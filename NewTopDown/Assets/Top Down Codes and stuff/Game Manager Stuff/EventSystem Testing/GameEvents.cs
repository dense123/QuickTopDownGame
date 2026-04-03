using System;
using UnityEngine;

public class GameEvents
{
    // FOR HEALTH
    public event Action<int> OnDamageTaken;
    public event Action OnKillingEvent;

    // FOR RAYCASTING
    public event Action OnHovering;


    public void InvokeTakeDamageEvent(int damage)
    {
        OnDamageTaken?.Invoke(damage);
    }
    public void InvokeKillingEvent() 
    { OnKillingEvent?.Invoke(); }

    public void InvokeHoveringEvent() 
    { OnHovering?.Invoke(); }
}
