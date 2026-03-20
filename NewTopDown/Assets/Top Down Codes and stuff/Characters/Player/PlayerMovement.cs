using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    private float walkingMoveSpeed = 200f;
    private float sprintingMoveSpeed = 500f;
    Vector2 moveInput;



    [SerializeField] private bool isDefaultWalking; // Will be a setting for the player
    public bool IsDefaultWalking
    {
        get => isDefaultWalking;
        set => isDefaultWalking = value;
    }

    bool isShiftPressed;
    bool isMoving;
    bool canMove = true; // Default is true, used for maybe when player dies? or frozen/petrified



    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        // My main man chatgpt wrote this, im still confused but to paraphrase
        // if walking is set to default, sprinting is when shift is pressed, which is the left of the operator
        // if walking isnt default, sprinting is when shift is NOT pressed, which is why !isShiftPressed
        bool isSprinting = isDefaultWalking ? isShiftPressed : !isShiftPressed;
        float moveSpeed = isSprinting ? sprintingMoveSpeed : walkingMoveSpeed;
        rb.linearVelocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }


    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            animator.SetBool("IsWalking", true);
            if (context.canceled)
            {
                animator.SetBool("IsWalking", false);
                animator.SetFloat("Last Input X", moveInput.x);
                animator.SetFloat("Last Input Y", moveInput.y);
            }
            moveInput = context.ReadValue<Vector2>();
            isMoving = moveInput != Vector2.zero;
            animator.SetFloat("Current Input X", moveInput.x);
            animator.SetFloat("Current Input Y", moveInput.y);
        }
    }

    // Role is to set isSprinting variable to true, which will run in update
    public void Sprint_Walk(InputAction.CallbackContext context)
    {
        //isSprinting = context.performed;

        if (context.performed)
        {
            isShiftPressed = true;
        }
        if (context.canceled)
        {
            isShiftPressed = false;
        }
    }
}
