using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMovementAnimation : MonoBehaviour
{

    private Animator animator;
    private Character character;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        character = GetComponent<Character>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        playerMovement.OnAnimationMoving += PlayMovingAnimation;
        playerMovement.OnAnimationStoppedMoving += PlayStoppingAnimation;
    }
    private void Start()
    {
        animator = character.GetAnimator();
    }

    private void PlayMovingAnimation(Vector2 currentlyMoving)
    {
        animator.SetBool("IsWalking", true);
        animator.SetFloat("Current Input X", currentlyMoving.x);
        animator.SetFloat("Current Input Y", currentlyMoving.y);
    }

    private void PlayStoppingAnimation(Vector2 lastFace)
    {
        animator.SetBool("IsWalking", false);
        animator.SetFloat("Last Input X", lastFace.x);
        animator.SetFloat("Last Input Y", lastFace.y);
        //Debug.Log("Stopped");
    }

    private void OnDisable()
    {
        playerMovement.OnAnimationMoving -= PlayMovingAnimation;
        playerMovement.OnAnimationStoppedMoving -= PlayStoppingAnimation;
    }
}
