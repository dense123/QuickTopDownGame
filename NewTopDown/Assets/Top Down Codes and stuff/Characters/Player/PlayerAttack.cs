using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(Player), typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{



    [Header("For attack")]
    [SerializeField] private int damage = 10;
    public int Damage => damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private AttackPointScript attackPointScript;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isDetectingEnemy;
    [SerializeField] private bool isEnemyDetected;

    private ControlsHandler controlsHandler;
    private Player player;
    private GameEvents gameEvents;

    [Header("Prototype stuff, will remove when refactoring")]
    private PlayerMovement playerMovement;

    [Header("Player Attack Movement Disable")]
    [SerializeField] private float timeDelay;


    // This can only store unique values, so it cant store same game objects etc.

    public HashSet<GameObject> lastHitHashSet = new HashSet<GameObject>(); // Contain last hit colliders
    public HashSet<GameObject> currentHitHashSet = new HashSet<GameObject>();

    //public event Action<Character.LastInputFacingNow> OnPlayerAttack;
    //public event Action<Vector2> OnPlayerAttack;

    public event Action OnPlayerAttackHitStop;
    public event Action<Enemy> OnPlayerHitEnemy;

    public event Action OnPlayerIdleAttack;
    public event Action<Vector2> OnPlayerRunningAttack;
    public event EventHandler<playerAttack> OnPlayerAttackButtonPressed;

    public class playerAttack
    {
        public Character.LastInputFacingNow facingState;
        public Vector2 moveInput;
    }


    private void Awake()
    {
        controlsHandler = GameManager.Instance.controlsHandler;
        gameEvents = GameManager.Instance.GameEvents;
        player = GetComponent<Player>();
        player.SetPlayer_TimeDelay_ForAttackMovementDisable(timeDelay);

        if (attackPointScript.playerAttack == null)
        {
            attackPointScript.playerAttack = this;
            Debug.LogWarning("Attack Point Script not initialised under inspector");
        }



        // Needs player movement for move input mainly, but also to disable movement when attacking
        playerMovement = GetComponent<PlayerMovement>();
        attackPoint.GetComponent<Collider2D>().enabled = false;
    }
    private void OnEnable()
    {
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Attack.performed += Attack;
        OnPlayerAttackButtonPressed += Attack_RepositionAttackPoint;
        OnPlayerAttackButtonPressed += Disable_PlayerMovement_ForPlayerAttack;
    }


    private void OnDisable()
    {
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Attack.performed -= Attack;
        OnPlayerAttackButtonPressed -= Attack_RepositionAttackPoint;
        OnPlayerAttackButtonPressed -= Disable_PlayerMovement_ForPlayerAttack;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        OnPlayerAttackButtonPressed?.Invoke(this, new playerAttack { 
            facingState = player.GetFacingState(), 
            moveInput = playerMovement.GetMoveInput() 
        });
    }


    //private void Update()
    //{

    //    currentHitHashSet.Clear();

    //    // Taken from Player Raycast script
    //    if (isDetectingEnemy)
    //    {
    //        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

    //        // Detect first
    //        foreach (Collider2D enemy in hitEnemies)
    //        {
    //            // Cut animation and slow velocity when hit enemy, so in normal state
    //            //isEnemyDetected = true;
    //            currentHitHashSet.Add(enemy.gameObject);
    //            // Shake their health bar

    //        }
    //    }

    //    if (currentHitHashSet.Count > 0)
    //    {
    //        foreach (GameObject hit in currentHitHashSet)
    //        {
    //            if (!lastHitHashSet.Contains(hit)) // if does not contain
    //            {
    //                // This means it's a new hit
    //                lastHitHashSet.Add(hit);
    //                hit.GetComponent<Enemy>().TakeDamage(damage);
    //                // Will change state to idle when time passes, it's called Disable_PlayerMovement_ForPlayerAttack coroutine
    //                player.SetCharacterState(Character.CharacterState.Attacking);
    //                if (combostate == IdleComboState.HeavyPunch)
    //                {
    //                    Debug.Log($"{hit.name} got knocked back!");
    //                    hit.GetComponent<Enemy>().InvokeKnockback(transform, idleKnockbackForce);
    //                }
    //                if (runningComboState == RunningComboState.RunningAttack_1)
    //                {
    //                    hit.GetComponent<Enemy>().InvokeKnockback(transform, runAttackForce);
    //                }
    //            }
    //        }
    //    }
    //    if (lastHitHashSet.Count > 0)
    //    {
    //        foreach (var obj in lastHitHashSet.ToList())
    //        //Original list = [A, B, C]

    //        //ToList() → makes a copy:
    //        //Copy = [A, B, C]

    //        //You loop over Copy
    //        //But remove from Original
    //        {
    //            // if current hit doesnt have last hit anymore
    //            if (!currentHitHashSet.Contains(obj))
    //            {
    //                if (combostate == IdleComboState.HeavyPunch || runningComboState == RunningComboState.RunningAttack_1)
    //                {
    //                    OnPlayerAttackHitStop?.Invoke(hitStopDuration);
    //                }
    //                lastHitHashSet.Remove(obj);
    //            }
    //        }
    //    }

    //}

    public void InvokeOnPlayerHitEnemy(Enemy enemy)
    {
        OnPlayerHitEnemy?.Invoke(enemy);
    }

    public void InvokeOnPlayerHitStop()
    {
        OnPlayerAttackHitStop?.Invoke();
    }

    // This is for player Attack, so Player attack requires player movement, not vice versa,
    public void Disable_PlayerMovement_ForPlayerAttack(object sender, playerAttack e)
    {
        //playerMovement.SetCanMove(false);
        StartCoroutine(Enable_PlayerMovement_AfterDelay(timeDelay, e.moveInput));
    }

    private IEnumerator Enable_PlayerMovement_AfterDelay(float delay, Vector2 moveInput)
    {
        yield return new WaitForSecondsRealtime(delay);
        //playerMovement.SetCanMove(true);
        if (moveInput.magnitude > 0)
        {
            player.SetCharacterState(Character.CharacterState.Moving); // NEED DEBUG

        }else
        {
            player.SetCharacterState(Character.CharacterState.Idle); // NEED DEBUG
        }
        attackPoint.GetComponent<Collider2D>().enabled = false;
        isDetectingEnemy = false;
    }


    private void Attack_RepositionAttackPoint(object sender, playerAttack e)
    {
        

        Vector2 UpLeft = new Vector2(-1, 1);
        Vector2 UpRight = new Vector2(1, 1);
        Vector2 DownLeft = new Vector2(-1, -1);
        Vector2 DownRight = new Vector2(1, -1);
        
        if (e.moveInput == Vector2.up)
        {
            attackPoint.localPosition = new Vector2(0.4f, 0.4f);
        }
        if (e.moveInput == Vector2.down)
        {
            attackPoint.localPosition = new Vector2(-0.4f, 0.4f);
        }
        if (e.moveInput == Vector2.left)
        {
            attackPoint.localPosition = new Vector2(-0.4f, 0.4f);
        }
        if (e.moveInput == Vector2.right)
        {
            attackPoint.localPosition = new Vector2(0.4f, 0.4f);
        }
        if (e.moveInput == UpLeft)
        {
            attackPoint.localPosition = new Vector2(-0.4f, 0.4f);
        }
        if (e.moveInput == UpRight)
        {
            attackPoint.localPosition = new Vector2(0.4f, 0.4f);
        }
        if (e.moveInput == DownLeft)
        {
            attackPoint.localPosition = new Vector2(-0.4f, 0.4f);
        }
        if (e.moveInput == DownRight)
        {
            attackPoint.localPosition = new Vector2(0.4f, 0.4f);
        }
        if (e.moveInput == Vector2.zero)
        {
            // If no input, use facing state to determine attack point
            switch (e.facingState)
            {
                case Character.LastInputFacingNow.Up:
                    attackPoint.localPosition = new Vector2(0.4f, 0.4f);
                    break;
                case Character.LastInputFacingNow.Down:
                    attackPoint.localPosition = new Vector2(-0.4f, 0.4f);
                    break;
                case Character.LastInputFacingNow.Left:
                    attackPoint.localPosition = new Vector2(-0.4f, 0.4f);
                    break;
                case Character.LastInputFacingNow.Right:
                    attackPoint.localPosition = new Vector2(0.4f, 0.4f);
                    break;
            }
        }


        switch (player.GetCharacterState())
        {
            case Character.CharacterState.Idle:
                OnPlayerIdleAttack?.Invoke();
                Debug.Log("Idle");
                break;
            case Character.CharacterState.Moving: 
                OnPlayerRunningAttack?.Invoke(e.moveInput);
                break;
        }

        player.SetCharacterState(Character.CharacterState.Attacking);
        isDetectingEnemy = true;
        attackPoint.GetComponent<Collider2D>().enabled = true;

    }

   
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    


}
