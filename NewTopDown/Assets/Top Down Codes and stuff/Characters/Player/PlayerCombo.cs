using System;
using UnityEngine;
using System.Collections;
using static PlayerAttack;

[RequireComponent(typeof(Player))]
public class PlayerCombo : MonoBehaviour
{



    [Header("For DEBUGGING")]
    [SerializeField] private SpriteRenderer sr;
    public enum RunningComboState
    {
        Idle, RunningAttack_1
    }
    [Header("For running attack")]
    public RunningComboState runningComboState;
    public enum IdleComboState
    {
        IdleState, Punch_1, Punch_2, HeavyPunch
    }

    [Header("For combo")]
    public IdleComboState combostate;

    [SerializeField] private float comboTimer = .2f;
    [SerializeField] private float idleKnockbackForce = 10f;
    [SerializeField] private float runAttackForce = 30f;

    public event Action<IdleComboState> OnComboStarted;


    [Header("Initialisation")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerAttack playerAttack;


    private void Awake()
    {
        combostate = IdleComboState.IdleState;
    }


    private void OnEnable()
    {
        OnComboStarted += invokeComboTimerCoroutine;
        playerAttack.OnPlayerIdleAttack += IdleCombo;
        playerAttack.OnPlayerIdleAttack += ChangePlayerColour_ForDebugIdleCombo;
        playerAttack.OnPlayerRunningAttack += RunAttack;
        playerAttack.OnPlayerHitEnemy += KnockbackEnemy;
    }
    private void OnDisable()
    {
        OnComboStarted -= invokeComboTimerCoroutine;
        playerAttack.OnPlayerIdleAttack -= IdleCombo;
        playerAttack.OnPlayerIdleAttack -= ChangePlayerColour_ForDebugIdleCombo;
        playerAttack.OnPlayerRunningAttack -= RunAttack;
    }

    private void KnockbackEnemy(Enemy hit)
    {
        if (combostate == IdleComboState.HeavyPunch)
        {
            Debug.Log($"{hit.name} got knocked back!");
            hit.GetComponent<Enemy>().InvokeKnockback(transform, idleKnockbackForce);
        }
        if (runningComboState == RunningComboState.RunningAttack_1)
        {
            Debug.Log($"{hit.name} got knocked back!");
            hit.GetComponent<Enemy>().InvokeKnockback(transform, runAttackForce);
        }
    }

    private void RunAttack(Vector2 moveInput)
    {
        runningComboState = RunningComboState.RunningAttack_1;
        player.SetCharacterState(Character.CharacterState.Attacking);
        player.GetRigidbody().AddForce(moveInput * runAttackForce, ForceMode2D.Impulse);
        Debug.Log("Running Attack!");
    }

    private void IdleCombo()
    {
        switch (combostate)
        {
            case IdleComboState.IdleState:
                combostate = IdleComboState.Punch_1;
                break;
            case IdleComboState.Punch_1:
                combostate = IdleComboState.Punch_2;
                break;
            case IdleComboState.Punch_2:
                combostate = IdleComboState.HeavyPunch;
                Debug.Log($"{combostate}");
                break;

        }
        Debug.Log("Ran idle combo");
        OnComboStarted?.Invoke(combostate);
    }


    private void invokeComboTimerCoroutine(IdleComboState state)
    {
        StartCoroutine(ComboTimer(state));
    }
    private IEnumerator ComboTimer(IdleComboState state)
    {
        yield return new WaitForSecondsRealtime(comboTimer);
        if (state == combostate)
        {
            combostate = IdleComboState.IdleState;// reset
        }
        if (state == IdleComboState.HeavyPunch)
        {
            combostate = IdleComboState.IdleState;// reset
        }
    }

    private void ChangePlayerColour_ForDebugIdleCombo() // Will delete when have combo animation
    {
        switch (combostate)
        {
            case IdleComboState.IdleState:
                sr.color = Color.white;
                break;
            case IdleComboState.Punch_1:
                sr.color = Color.blue;
                break;
            case IdleComboState.Punch_2:
                sr.color = Color.green;
                break;
            case IdleComboState.HeavyPunch:
                sr.color = Color.red;
                break;
        }
    }

}
