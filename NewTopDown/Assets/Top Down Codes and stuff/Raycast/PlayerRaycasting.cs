using UnityEngine;

[RequireComponent (typeof(Player))]
public class PlayerRaycasting : MonoBehaviour
{

    RaycastHit2D hit;
    private Vector2 direction;
    [SerializeField] private float distance;
    Collider2D LastHitCollider;

    [SerializeField] private Character character;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (character == null)
            character = GetComponent<Character>();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (character.lastInputFacing)
        {
            case Character.LastInputFacingNow.Up:
                direction = Vector2.up;
                break;
            case Character.LastInputFacingNow.Down:
                direction = Vector2.down;
                break;
            case Character.LastInputFacingNow.Left:
                direction = Vector2.left;
                break;
            case Character.LastInputFacingNow.Right:
                direction = Vector2.right;
                break;
        }
        hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Interactable"));
        Debug.DrawRay(transform.position, direction * distance, Color.red);

        if (hit.collider == null)
        {
            if (LastHitCollider != null)
            {
                LastHitCollider.GetComponent<Interactable>().isHovering = false;
                //if (Physics2D.Distance(LastHitCollider, gameObject.GetComponent<Collider2D>()) > )
                { 
                    LastHitCollider = null; 
                }
            }

            return;
        }
        if (LastHitCollider != hit.collider.gameObject)
        {
            LastHitCollider = hit.collider;
            LastHitCollider.GetComponent<Interactable>().isHovering = true;
            Debug.Log(LastHitCollider);
        }
    }

    public GameObject GetHitCollider()
    {

        return null;
    }
}
