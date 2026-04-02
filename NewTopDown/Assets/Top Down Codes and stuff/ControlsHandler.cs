using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsHandler : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;


    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GameManager.instance.Player.GetComponent<PlayerInput>();
        }
    }

    public void OnControlsChanged(PlayerInput input)
    {
        Debug.Log("New scheme: " + input.currentControlScheme);
        Debug.Log($"{GetBindingName("Interact")} to interact");
    }
    string GetBindingName(string actionName)
    {
        var action = playerInput.actions[actionName];
        return action.GetBindingDisplayString();
    }

}
