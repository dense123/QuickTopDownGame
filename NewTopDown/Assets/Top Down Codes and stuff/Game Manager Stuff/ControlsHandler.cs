using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControlsHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    public event Action<PlayerInput> ControlsChanged;


    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GameManager.instance.Player.GetComponent<PlayerInput>();
        }
    }

    private void Start()
    {
        ControlsChanged += DebugChangedControls;
    }

    public void OnControlsChanged(PlayerInput input)
    {
        ControlsChanged?.Invoke(input);
    }

    void DebugChangedControls(PlayerInput input)
    {
        Debug.Log("New scheme: " + input.currentControlScheme);
    }

    string GetBindingName(string actionName)
    {
        var action = playerInput.actions[actionName];
        return action.GetBindingDisplayString();
    }

}
