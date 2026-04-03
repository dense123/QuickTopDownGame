using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

// This is under 
public class MenuController : MonoBehaviour
{

    [SerializeField] Transform PauseMenu;
    [SerializeField] PlayerInput playerInput;
    private InputSystemUIInputModule uiModule;

    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;

        uiModule = EventSystem.current.GetComponent<InputSystemUIInputModule>();

        if (playerInput == null)
        {
            GameManager.instance.nullReference_debugLogWarning("Player Input", this.name, "Will now find player gameobject!");
            playerInput = FindFirstObjectByType<PlayerInput>();
        }
        EnterGameplay();
    }
    public void EnterGameplay()
    {
        playerInput.SwitchCurrentActionMap("Player");
        uiModule.enabled = false;
    }

    public void EnterMenu()
    {
        playerInput.SwitchCurrentActionMap("UI"); // optional
        uiModule.enabled = true;
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
            EnterMenu();
        }
        else
        {
            EnterGameplay();
        }
    }
}
