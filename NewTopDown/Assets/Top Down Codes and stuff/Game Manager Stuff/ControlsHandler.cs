using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ControlsHandler : MonoBehaviour
{


    public InputSystem_Actions InputSystem_Actions_PlayerInput { get; private set; }
    public event Action<InputSystem_Actions> ControlsChanged;


    private void Awake()
    {
        InputSystem_Actions_PlayerInput = new InputSystem_Actions();
        // Enable the input on the script itself
        InputSystem_Actions_PlayerInput.Disable();
    }

    private void OnEnable()
    {
        ControlsChanged += DebugChangedControls;
    }

    private void OnDisable()
    {
        ControlsChanged -= DebugChangedControls;
    }

    private void Start()
    {

    }



    //public void OnControlsChanged(PlayerInput input)
    //{
    //    ControlsChanged?.Invoke(input);
    //}

    void DebugChangedControls(InputSystem_Actions input)
    {
        Debug.Log("New scheme: " + input.controlSchemes);
    }

    string GetBindingName(string actionName)
    {
        var action = InputSystem_Actions_PlayerInput.FindAction(actionName);
        return action.GetBindingDisplayString();
    }

}
