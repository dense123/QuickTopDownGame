using System;
using UnityEngine;

public class ControlsHandler : MonoBehaviour
{
    InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    public Vector2 UpPressed()
    {
        // Handle walking logic here using movementInput
        Debug.Log("Walking with input: " + inputActions.Player.Move);
        return inputActions.Player.Move.ReadValue<Vector2>();
    }

}
