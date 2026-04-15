using UnityEngine;

public class AttackPointScript : MonoBehaviour
{
    // Detects that enemy is collided with attack


    [Header("Will mainly be initialised from player attack script, cuz maybe I'll forget to do it in inspector")]
    public PlayerAttack playerAttack;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        collision.TryGetComponent(out Enemy enemy);
        if (enemy != null)
        {
            enemy.TakeDamage(playerAttack.Damage);
            // Invoke OnPlayerHitEnemy;
            playerAttack.InvokeOnPlayerHitEnemy(enemy);
            //Debug.Log($"Hit {enemy.name} for {playerAttack.Damage} damage!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null) return;
        collision.TryGetComponent(out Enemy enemy);
        if (enemy != null)
        {
            playerAttack.InvokeOnPlayerHitStop();
        }
    }




}