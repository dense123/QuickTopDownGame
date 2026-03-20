using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{

    [SerializeField] Transform PauseMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
    }

    public void PauseFunction(InputAction.CallbackContext context)
    {
        PauseMenu.gameObject.SetActive(!PauseMenu.gameObject.activeSelf);
    }
}
