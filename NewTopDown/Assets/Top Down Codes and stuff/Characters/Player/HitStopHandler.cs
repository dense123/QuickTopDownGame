using System;
using System.Collections;
using UnityEngine;
using static PlayerCombo;

public class HitStopHandler : MonoBehaviour
{


    [Header("Set the time for hitStop")]
    [SerializeField] private float hitStopDuration = 0.1f;

    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerCombo playerCombo;

    private void OnEnable()
    {
        playerAttack.OnPlayerAttackHitStop += HitStop;
    }
    private void OnDisable()
    {
        playerAttack.OnPlayerAttackHitStop -= HitStop;
    }

    private void HitStop()
    {
        Time.timeScale = 0.0f;
        //Application.targetFrameRate = 10;
        StartCoroutine(WaitHitStop(hitStopDuration));
    }

    private IEnumerator WaitHitStop(float duration)
    {
        //Debug.Log("hit stop started");
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        //Application.targetFrameRate = -1;
        //playerCombo.combostate = IdleComboState.IdleState;
        playerCombo.runningComboState = RunningComboState.Idle;
        //Debug.Log("hit stop stopped");
    }
}
