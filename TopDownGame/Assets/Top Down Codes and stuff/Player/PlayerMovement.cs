using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    [SerializeField] float moveSpeed = 10f;
    bool isWalking = false;
    Vector2 moveInput;



    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveInput.x * moveSpeed * Time.deltaTime, moveInput.y * moveSpeed * Time.deltaTime, 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        isWalking = true;
        animator.SetBool("IsWalking", true);
        if (context.canceled)
        {
            isWalking = true;
            animator.SetBool("IsWalking", false);
            animator.SetFloat("Last Input X", moveInput.x);
            animator.SetFloat("Last Input Y", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("Current Input X", moveInput.x);
        animator.SetFloat("Current Input Y", moveInput.y);
        }
}
