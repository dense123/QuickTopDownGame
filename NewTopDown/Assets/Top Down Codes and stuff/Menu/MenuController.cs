using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{

    [SerializeField] Transform PauseMenu;
    [SerializeField] PlayerInput playerInput;

    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        if (playerInput == null)
        {
            GameManager.instance.nullReference_debugLogWarning("Player Input", this.name, "Will now find player gameobject!");
            playerInput = FindFirstObjectByType<PlayerInput>();
        }
        playerInput.SwitchCurrentActionMap("Player");
            
    }

    public void PauseFunction(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        PauseMenuActivate(!PauseMenu.gameObject.activeSelf);
    }

    void PauseMenuActivate(bool pause)
    {
        PauseMenu.gameObject.SetActive(pause);

        if (pause)
        {
            SwitchToUi();
        }
        else
        {
            SwitchToPlayer();
        }
    }
    private void SwitchToPlayer()
    {
        playerInput.SwitchCurrentActionMap("Player");
    }

    private void SwitchToUi()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
}
