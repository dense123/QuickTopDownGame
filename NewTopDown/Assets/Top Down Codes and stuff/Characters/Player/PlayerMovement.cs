using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Character;
using static PlayerAttack;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{

    private float walkingMoveSpeed = 200f;
    private float sprintingMoveSpeed = 500f;
    private Vector2 moveInput;
    public Vector2 GetMoveInput() => moveInput;
    private float moveSpeed;

    [SerializeField] private Player player;
    private ControlsHandler controlsHandler; // Not serialised as only get from game manager, not the component
    private Rigidbody2D rb;
    private GameEvents gameEvents;

    public event Action<Vector2> OnAnimationMoving;
    public event Action<Vector2> OnAnimationStoppedMoving;



    //[SerializeField] 
    private bool isDefaultWalking = true; // Will be a setting for the player
    private bool previousIsDefaultWalking; 
    public bool IsDefaultWalking
    {
        get => isDefaultWalking;
        set => isDefaultWalking = value;
    }

    private bool isShiftPressed;


    // ==========================================================================================================================
    
    // This is for Player Attack
    private bool canMove = true; // Default is true, used for maybe when player dies? or frozen/petrified
    public bool GetCanMove()
    {
        return canMove;
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    // ==========================================================================================================================

    private void Awake()
    {
        player = GetComponent<Player>();
        controlsHandler = GameManager.Instance.controlsHandler;
        gameEvents = GameManager.Instance.GameEvents;
    }

    private void Start()
    {
        rb = player.GetRigidbody();
        moveSpeed = isDefaultWalking ? walkingMoveSpeed : sprintingMoveSpeed;
        previousIsDefaultWalking = isDefaultWalking;
    }

    private void OnEnable()
    {
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Enable();
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Move.performed += Move;
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Move.canceled += Move;
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Sprint.performed += Sprint_Walk;
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Sprint.canceled += Sprint_Walk;
        //gameEvents.OnPlayerAttack += Disable_PlayerMovement_ForPlayerAttack;
        player.OnCharacterDeath += Disable_PlayerMovement;
        player.OnCharacterDeath += Disable_PlayerVelocity;
    }

    private void OnDisable()
    {
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Move.performed -= Move;
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Move.canceled -= Move;
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Sprint.performed -= Sprint_Walk;
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Sprint.canceled -= Sprint_Walk;
        //gameEvents.OnPlayerAttack -= Disable_PlayerMovement_ForPlayerAttack;
        player.OnCharacterDeath -= Disable_PlayerMovement;
        player.OnCharacterDeath -= Disable_PlayerVelocity;
    }
    private void Update()
    {
        // Only for prototype, will remove this and refactor when improving the code
        if (isDefaultWalking != previousIsDefaultWalking)
        {
            previousIsDefaultWalking = isDefaultWalking;
            moveSpeed = isDefaultWalking ? walkingMoveSpeed : sprintingMoveSpeed;
        }
    }
    private void FixedUpdate()
    {

        CharacterState currentState = player.GetCharacterState();
        switch (currentState)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Moving:
                break;
            case CharacterState.Attacking:
                return;
            case CharacterState.TakingDamage:
                return;
            case CharacterState.Dead:
                return;
        }


        rb.linearVelocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }


    // Separate animation from logic, so this is only for movement, 
    // and the animation script will subscribe to the move input and animate based on that, 
    //  which is cleaner and less buggy than trying to do both in one script
    private void Move(InputAction.CallbackContext context)
    {
        CharacterState currentState = player.GetCharacterState();
        switch (currentState)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Moving:
                break;
            case CharacterState.Attacking:
                return;
            case CharacterState.TakingDamage:
                return;
            case CharacterState.Dead:
                return;
        }
        // Place on top to save the last input
        if (context.canceled)
        {
            player.SetCharacterState(CharacterState.Idle);
            OnAnimationStoppedMoving?.Invoke(moveInput);
            moveInput = Vector2.zero;
            return;
        }
            
        moveInput = context.ReadValue<Vector2>();
        player.SetCharacterState(CharacterState.Moving);
        OnAnimationMoving?.Invoke(moveInput);

        Vector2 UpLeft = new Vector2(-1, 1);
        Vector2 UpRight = new Vector2(1, 1);
        Vector2 DownLeft = new Vector2(-1, -1);
        Vector2 DownRight = new Vector2(1, -1);

        if (moveInput == Vector2.up)
        {
            player.SetFacingState(Character.LastInputFacingNow.Up);
        }
        if (moveInput == Vector2.down)
        {
            player.SetFacingState(Character.LastInputFacingNow.Down);
        }
        if (moveInput == Vector2.left)
        {
            player.SetFacingState(Character.LastInputFacingNow.Left);
        }
        if (moveInput == Vector2.right)
        {
            player.SetFacingState(Character.LastInputFacingNow.Right);
        }
        if (moveInput == UpRight)
        {
            player.SetFacingState(Character.LastInputFacingNow.Right);
        }
        if (moveInput == DownRight)
        {
            player.SetFacingState(Character.LastInputFacingNow.Right);
        }
        if (moveInput == UpLeft)
        {
            player.SetFacingState(Character.LastInputFacingNow.Left);
        }
        if (moveInput == DownLeft)
        {
            player.SetFacingState(Character.LastInputFacingNow.Left);
        }

    }

    // Role is to set isSprinting variable to true, which will run in update
    private void Sprint_Walk(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // My main man chatgpt wrote this, im still confused but to paraphrase
            // if walking is set to default, sprinting is when shift is pressed, which is the left of the operator
            // if walking isnt default, sprinting is when shift is NOT pressed, which is why !isShiftPressed
            //bool isSprinting = isDefaultWalking ? isShiftPressed : !isShiftPressed;
            //float moveSpeed = isSprinting ? sprintingMoveSpeed : walkingMoveSpeed;
            // Not readable code for now,
            // will clean up later, basically if default is walking, then sprinting is when shift is pressed, else sprinting is when shift is not pressed
            moveSpeed = isDefaultWalking ? sprintingMoveSpeed : walkingMoveSpeed;
        }
        if (context.canceled)
        {
            moveSpeed = isDefaultWalking ? walkingMoveSpeed : sprintingMoveSpeed;
        }
    }

    

    private void Disable_PlayerMovement()
    {
        canMove = false;
    }

    private void Disable_PlayerVelocity()
    {
        rb.linearVelocity = Vector2.zero;
    
    }
}
