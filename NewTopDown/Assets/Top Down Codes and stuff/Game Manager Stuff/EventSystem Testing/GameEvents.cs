using System;
using UnityEngine;

public class GameEvents
{
    // FOR HEALTH
    public event Action<int> OnDamageTaken;
    public event Action OnKillingEvent;

    // FOR RAYCASTING
    public event Action OnHovering;

    // FOR MENU
    public event Action<SaveData> OnPlayButtonPressed;
    public event Action OnLoadButtonPressed;
    public event Action OnSettingsButtonPressed;
    public event Action OnQuitButtonPressed;

    // FOR PLAYER ATTACK AND MOVEMENT
    //public event Action<Vector2> OnPlayerAttack;

    //public void InvokePlayerAttackEvent(Vector2 moveInput) 
    //{ OnPlayerAttack?.Invoke(moveInput); }













    public void InvokeTakeDamageEvent(int damage)
    { OnDamageTaken?.Invoke(damage); }
    public void InvokeKillingEvent() 
    { OnKillingEvent?.Invoke(); }
    public void InvokeHoveringEvent() 
    { OnHovering?.Invoke(); }
    public void InvokePlayButtonPressedEvent(SaveData saveData) 
    { OnPlayButtonPressed?.Invoke(saveData); }
    public void InvokeLoadButtonPressedEvent() 
    { OnLoadButtonPressed?.Invoke(); }
    public void InvokeSettingsButtonPressedEvent() 
    { OnSettingsButtonPressed?.Invoke(); }
    public void InvokeQuitButtonPressedEvent() 
    { OnQuitButtonPressed?.Invoke(); }
}
