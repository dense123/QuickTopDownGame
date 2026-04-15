using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Player))]
public class PlayerInteract : MonoBehaviour
{

    PlayerRaycasting playerRaycasting;
    GameObject interactionTarget;

    private void Start()
    {

        playerRaycasting = GetComponent<PlayerRaycasting>();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (interactionTarget != null)
            {
                //interactionTarget = playerRaycasting.GetClosestHitCollider();
                //interactionTarget.GetComponent<Interactable>().isInteractedWith = true;
            }        
        }
    }

}
