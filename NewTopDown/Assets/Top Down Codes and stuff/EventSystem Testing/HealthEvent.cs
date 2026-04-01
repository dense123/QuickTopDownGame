using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthEvent : MonoBehaviour
{
    public event Action OnHealthEvent;

    public void EventTriggerNOW(InputAction.CallbackContext context)
    {
        OnHealthEvent?.Invoke();
    }
}
