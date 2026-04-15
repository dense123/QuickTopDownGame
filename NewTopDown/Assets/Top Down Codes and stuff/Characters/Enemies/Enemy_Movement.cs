using UnityEngine;  

public class Enemy_Movement : MonoBehaviour
{


    private Enemy enemyScript;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;


}