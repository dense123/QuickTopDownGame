using UnityEngine;
using static PlayerAttack;

[RequireComponent(typeof(PlayerAttack))]
public class PlayerAttackAnimation : MonoBehaviour
{
    private Animator animator;
    private Character character;
    private PlayerAttack playerAttack;
    private GameEvents gameEvents;

    private void Awake()
    {
        character = GetComponent<Character>();
        playerAttack = GetComponent<PlayerAttack>();
        gameEvents = GameManager.Instance.GameEvents;
    }
    private void OnEnable()
    {
        playerAttack.OnPlayerAttackButtonPressed += PlayAttackAnimation;
    }
    private void OnDisable()
    {
        playerAttack.OnPlayerAttackButtonPressed -= PlayAttackAnimation;
    }
    private void Start()
    {
        animator = character.GetAnimator();
    }

    private void PlayAttackAnimation(object sender, playerAttack e)
    {
        animator.SetTrigger("Attack");

        Vector2 UpLeft = new Vector2(-1, 1);
        Vector2 UpRight = new Vector2(1, 1);
        Vector2 DownLeft = new Vector2(-1, -1);
        Vector2 DownRight = new Vector2(1, -1);

        if (e.moveInput == Vector2.up)
        {
            animator.SetFloat("Last Input X", 1);
        }
        if (e.moveInput == Vector2.down)
        {
            animator.SetFloat("Last Input X", -1);
        }
        if (e.moveInput == Vector2.left)
        {
            animator.SetFloat("Last Input X", -1);
        }
        if (e.moveInput == Vector2.right)
        {
            animator.SetFloat("Last Input X", 1);
        }
        if (e.moveInput == UpLeft)
        {
            animator.SetFloat("Last Input X", -1);
        }
        if (e.moveInput == UpRight)
        {
            animator.SetFloat("Last Input X", 1);
        }
        if (e.moveInput == DownLeft)
        {
            animator.SetFloat("Last Input X", -1);
        }
        if (e.moveInput == DownRight)
        {
            animator.SetFloat("Last Input X", 1);
        }
        if (e.moveInput == Vector2.zero)
        {
            // If no input, use facing state to determine attack point
            switch (e.facingState)
            {
                case Character.LastInputFacingNow.Up:
                    animator.SetFloat("Last Input X", 1);
                    break;
                case Character.LastInputFacingNow.Down:
                    animator.SetFloat("Last Input X", -1);
                    break;
                case Character.LastInputFacingNow.Left:
                    animator.SetFloat("Last Input X", -1);
                    break;
                case Character.LastInputFacingNow.Right:
                    animator.SetFloat("Last Input X", 1);
                    break;
            }
        }
    }


}