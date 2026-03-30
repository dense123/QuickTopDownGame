using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Player))]
public class PlayerInteract : MonoBehaviour
{

    public GameObject interactionIndicator;

    private void Start()
    {
        interactionIndicator.SetActive(false);
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;
        if (collision.gameObject.GetComponent<Interactable>())
        {
            interactionIndicator.SetActive(true);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null)
            return;
        if (collision.gameObject.GetComponent<Interactable>())
        {
            interactionIndicator.SetActive(false);
        }
    }
}
