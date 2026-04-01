using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Player))]
public class PlayerInteract : MonoBehaviour
{

    public GameObject interactionIndicator;
    PlayerRaycasting playerRaycasting;
    GameObject interactionTarget;

    private void Start()
    {
        interactionIndicator.SetActive(false);
        playerRaycasting = GetComponent<PlayerRaycasting>();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interactionTarget = playerRaycasting.GetClosestHitCollider();
            Debug.Log(interactionTarget.name);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision == null)
    //        return;
    //    if (collision.gameObject.GetComponent<Interactable>())
    //    {
    //        interactionIndicator.SetActive(true);
    //    } 
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision == null)
    //        return;
    //    if (collision.gameObject.GetComponent<Interactable>())
    //    {
    //        interactionIndicator.SetActive(false);
    //    }
    //}
}
