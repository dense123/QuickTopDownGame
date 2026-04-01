using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.Image;

[RequireComponent (typeof(Player))]
public class PlayerRaycasting : MonoBehaviour
{
    bool isInteractableInRange = false; // Will be set to true with on trigger collision

    [SerializeField] int numberOfRays;
    [SerializeField] float coneAngle;
    [SerializeField] private float distance;
    public LayerMask raycastLayer;

    Vector2 forward = Vector2.down;
    float startAngle;
    float angleSpace;

    // Hashset isnt ordered, doesnt have indexes. Only able to store unique values
    public HashSet<GameObject> lastHitHashSet = new HashSet<GameObject>(); // Contain last hit colliders
    public HashSet<GameObject> currentHitHashSet = new HashSet<GameObject>();

    private Vector2 direction;
    //Collider2D LastHitCollider;

    [SerializeField] private Character character;
    private bool isKeyPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (character == null)
            character = GetComponent<Character>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (!isInteractableInRange)
        {
            //return;
        }

        currentHitHashSet.Clear();

        switch (character.lastInputFacing)
        {
            case Character.LastInputFacingNow.Up:
                forward = Vector2.up;
                break;
            case Character.LastInputFacingNow.Down:
                forward = Vector2.down;
                break;
            case Character.LastInputFacingNow.Left:
                forward = Vector2.left;
                break;
            case Character.LastInputFacingNow.Right:
                forward = Vector2.right;
                break;
        }

        startAngle = -coneAngle / 2;
        angleSpace = coneAngle / (numberOfRays - 1);

        for (int i = 0; i < numberOfRays; i++)
        {
            
            float angle = startAngle + angleSpace * i;
            direction = Quaternion.Euler(0, 0, angle) * forward;

            RaycastHit2D hitRay = Physics2D.Raycast(transform.position, direction, distance, raycastLayer);

            if (hitRay.collider != null)
            {
                // Draw only up to the hit point
                Debug.DrawLine(transform.position, hitRay.point, Color.blue);
                // Hash set will only add if it isnt inside already
                currentHitHashSet.Add(hitRay.collider.gameObject); 
                
            }
            else if (hitRay.collider == null)
            {
                Debug.DrawRay(transform.position, direction * distance, Color.red);
            }
        }

        foreach(var obj in currentHitHashSet)
        {
            // Add current collided gameobject to last hit collder
            if (!lastHitHashSet.Contains(obj))
            {
                lastHitHashSet.Add(obj);
                obj.GetComponent<Interactable>().isHovering = true;
            }
        }

        foreach (var obj in lastHitHashSet.ToList())
        //Original list = [A, B, C]

        //ToList() → makes a copy:
        //Copy = [A, B, C]

        //You loop over Copy
        //But remove from Original
        {
            // if current hit doesnt have last hit anymore
            if (!currentHitHashSet.Contains(obj))
            {
                lastHitHashSet.Remove(obj);
                obj.GetComponent<Interactable>().isHovering = false;
            }
        }
    }

    public void DisplayDebugForRaycast(InputAction.CallbackContext context)
    {
        if (context.started)
            isKeyPressed = true;
        else if (context.canceled)
            isKeyPressed = false;
        Debug.Log(isKeyPressed);
    }

    public GameObject GetClosestHitCollider()
    {
        float minDistance = Mathf.Infinity;
        GameObject closest = null;
        foreach (var obj in currentHitHashSet)
        {
            float objDistance = Vector2.Distance(obj.transform.position, transform.position);
            if (objDistance < minDistance)
            {
                minDistance = objDistance;
                closest = obj;
                Debug.Log(minDistance);
            }
        }
        return closest;
    }

}
