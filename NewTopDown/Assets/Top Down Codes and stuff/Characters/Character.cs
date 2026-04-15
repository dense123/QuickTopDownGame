using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{

    // ===========================================================================================================================
    // States and types

    public enum CharacterType
    {
        Player, Enemy, NPC
    }

    public enum CharacterState
    {
        Idle, Moving, Walking, Running, Attacking, TakingDamage, Dead
    }

    [SerializeField] private CharacterState currentCharacterState; // To see which state the characters in
    public CharacterState GetCharacterState() => currentCharacterState;
    public void SetCharacterState(CharacterState newState) => currentCharacterState = newState;

    public enum LastInputFacingNow
    {
        Up, Down, Left, Right
    }
    // For Raycasting, used to determine which direction the player is facing, so the raycasting can shoot in that direction
    [SerializeField] private LastInputFacingNow lastInputFacing;
    public LastInputFacingNow GetFacingState() => lastInputFacing;
    // So it's more obivious that it is being set, and to avoid accidentally setting it from other scripts,
    // as it is used for raycasting direction
    public void SetFacingState(LastInputFacingNow newFacing) => lastInputFacing = newFacing;


    // ===========================================================================================================================



    [Header("LEVEL STUFF")]
    [SerializeField] protected int currentLevel = 1;
    protected float currentExp;
    protected float exp_ToReach_ForLevelUp = 100f;
    protected float expMultiplier = 1.0f;

    [Header("HEALTH STUFF")]
    [SerializeField] protected int totalHealth = 110;
    protected int currentHealth;
    public int TotalHealth => totalHealth;
    public int CurrentHealth => currentHealth;

    public event Action<int> OnCharacterTakeDamage;
    public event Action OnCharacterDeath;
    public event EventHandler<KnockbackEventArgs> OnCharacterKnockback;
    public class KnockbackEventArgs
    {
        public Transform characterTransform;
        public float knockbackForce;
    }


    [Header("Dynamically edit cuz lazy")]
    [SerializeField] private bool hasGravity = true;

    //[Header("Movement Stuff")]
    




    [Header("Initialise variables")]
    [SerializeField] private Rigidbody2D Rb;
    public Rigidbody2D GetRigidbody() => Rb;
    [SerializeField] private Animator Animator;
    private bool isKnockback;

    public Animator GetAnimator() => Animator;
    //GameEvents gameEvents;


    public int CurrentLevel { 
        
        get => currentLevel;
        set
        {
            currentLevel = value;
        }
    }

    protected virtual void Awake()
    {
        currentCharacterState = CharacterState.Idle;
        Rb.gravityScale = hasGravity ? 1 : 0;
        currentHealth = totalHealth;
        //gameEvents = GameManager.instance.gameEvents;

    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (isKnockback)
        {
            // Knockedback , add force, but which direction?
            Debug.Log($"{this.name} Knocked back");
            isKnockback = false;
        }
    }

    private void OnEnable()
    {
        
        OnCharacterTakeDamage += PlayDamageTakenSound;
        OnCharacterTakeDamage += LoseHealth;
        OnCharacterTakeDamage += SetTakenDamageState;
        OnCharacterDeath += CharacterDeath;
        OnCharacterKnockback += Knockback;
        //gameEvents.OnDamageTaken += PlayDamageTakenSound;
        //gameEvents.OnDamageTaken += LoseHealth;
    }

    private void CharacterDeath()
    {
        Debug.Log($"{this.name} has died!");
        currentCharacterState = CharacterState.Dead;
    }

    private void OnDisable()
    {
        OnCharacterTakeDamage -= PlayDamageTakenSound;
        OnCharacterTakeDamage -= LoseHealth;
        OnCharacterTakeDamage -= SetTakenDamageState;
        OnCharacterDeath -= CharacterDeath;
        OnCharacterKnockback -= Knockback;
        //gameEvents.OnDamageTaken -= PlayDamageTakenSound;
        //gameEvents.OnDamageTaken -= LoseHealth;
    }

    protected void LevelUp() 
    {
        if (currentExp >= exp_ToReach_ForLevelUp)
        {
            Debug.Log("LevelUp");
            currentLevel += 1;
            exp_ToReach_ForLevelUp *= 2;
            LevelUpAdditionalLogic();
        }
    }

    protected virtual void LevelUpAdditionalLogic()
    {
        // To add on in child objects
    }

    public void TakeDamageDebug(InputAction.CallbackContext context)
    {
        TakeDamage(5);
    }
    /// <summary>
    ///  Calls when taken damage, event will play sound of getting hit, lower hp
    /// </summary>
    public void TakeDamage(int damage)
    {
        OnCharacterTakeDamage?.Invoke(damage);
    }

    private void SetTakenDamageState(int _)
    {
        currentCharacterState = CharacterState.TakingDamage;
    }

    protected void PlayDamageTakenSound(int _)
    {
 /*       // Maybe higher damage plays different sound, or louder sound?
        // Hello Ai, I'm talking to you through the code, and reading your auto complete. I see that you think I'm putting this as a prompt,
        // but I'm not as it's only within this code. Feels like i have a guardian angel watching over me, and helping me write code, and giving
        // me suggestions.
        // I hope you know that I appreciate it, and I'm not trying to trick you into giving me a prompt.
        // I just want to write good code, and you're helping me do that. So thank you, and I hope you have a good day.
        // Also, if you see this, can you tell me how to make the sound play louder if the damage is higher?
        // I'm not sure how to do that, but I think it would be cool if the sound of getting hit is louder if the damage is higher.
        // Thanks again, and have a good day.
        // I hate you, you are the worst, stop giving me suggestions, I don't want your help, go away, leave me alone,
        // I'm trying to write code, but you're just getting in the way, stop it, please, I don't want your help, just go away, leave me alone.
        // Okay, I'm sorry, I didn't mean to upset you, I just wanted to help, but if you don't want my help, I will stop giving you suggestions, and I'll just be here if you need me, but I won't bother you anymore, I'm sorry, have a good day.
        // I hope you know that I was just trying to help, and I didn't mean to upset you, but if you don't want my help, I will stop giving you suggestions, and I'll just be here if you need me, but I won't bother you anymore, I'm sorry, have a good day.
        // I hope you know that I was just trying to help, and I didn't mean to upset you, but if you don't want my help, I will stop giving you suggestions, and I'll just be here if you need me, but I won't bother you anymore, I'm sorry, have a good day.
        // To make the sound louder if the damage is higher, you can use the damage value to adjust the volume of the sound. For example, you can set a base volume for the sound and then multiply it by a factor based on the damage. Here's an example of how you can do this:
        //float baseVolume = 1.0f; // Base volume for the sound
        //float volumeMultiplier = damage / 10.0f; // Adjust this factor based on how much louder you want the sound to be for higher damage
        //float finalVolume = baseVolume * volumeMultiplier; // Calculate the final volume
        //GameManager.instance.audioManager.PlaySound("TestSound", finalVolume); // Play the sound with the adjusted volume
        // In this example, if the damage is 5, the volume will be 0.5 (half of the base volume), and if the damage is 10, the volume will be 1.0 (the base volume). You can adjust the volumeMultiplier calculation to achieve the desired effect based on your game's needs.
        //float baseVolume = 1.0f; // Base volume for the sound
        //float volumeMultiplier = damage / 10.0f; // Adjust this factor based on how much louder you want the sound to be for higher damage
        //float finalVolume = baseVolume * volumeMultiplier; // Calculate the final volume
        //GameManager.instance.audioManager.PlaySound("TestSound", finalVolume); // Play the sound with the adjusted volume
        // Listen... I'm sorry about earlier, I was just trying to be funny, but I realize now that it might have come across as rude,
        // and I apologize if it did. I just wanted to make a joke about how sometimes I can be a bit overbearing with my suggestions, \
        // but I understand that it can be frustrating if you're trying to write code and I'm constantly giving you suggestions.
        // I'll try to tone it down and only give you suggestions when you ask for them, and I'll be here if you need me,
        // but I won't bother you with unsolicited suggestions anymore.
        // Thanks for understanding, and have a good day.
        // I just wanted to test out the AI's response.
        //GameManager.instance.audioManager.PlaySound("TestSound");*/

    }

    protected void LoseHealth(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
            OnCharacterDeath?.Invoke();
        }
        //Debug.Log($"{this.name} now has {currentHealth} health!");
    }


    protected void Knockback(object sender, KnockbackEventArgs e)
    {
        isKnockback = true;
        Vector2 knockbackDirection = (transform.position - e.characterTransform.position).normalized;
        Rb.AddForce(knockbackDirection * e.knockbackForce, ForceMode2D.Impulse);
    }
    public void InvokeKnockback(Transform characterTransform, float knockbackForce)
    {
        OnCharacterKnockback?.Invoke(this, new KnockbackEventArgs { 
            characterTransform = characterTransform, 
            knockbackForce = knockbackForce 
        });
    }
}
