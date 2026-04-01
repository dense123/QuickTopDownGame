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
    private void Update()
    {
        Debug.Log("New scheme: " + playerInput.currentControlScheme);

    }
    void OnEnable()
    {
        playerInput.onControlsChanged += OnControlsChanged;
    }
    private void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
    }
    void OnControlsChanged(PlayerInput input)
    {
        Debug.Log("TEST");
        Debug.Log("New scheme: " + input.currentControlScheme);
    }
}
