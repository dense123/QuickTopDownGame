using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Header("LEVEL STUFF")]
    [SerializeField] protected int currentLevel = 1;
    protected float currentExp;
    protected float exp_ToReach_ForLevelUp = 100f;
    protected float expMultiplier = 1.0f;

    [Header("HEALTH STUFF")]
    protected int totalHealth = 110;
    protected int currentHealth;
    [SerializeField] protected Slider healthBar;


    //[Header("Misc")]
    public enum LastInputFacingNow
    {
        Up, Down, Left, Right
    }

    public LastInputFacingNow lastInputFacing;

    [Header("Initialise variables")]
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }
    GameEvents gameEvents;


    public int CurrentLevel { 
        
        get => currentLevel;
        set
        {
            currentLevel = value;
        }
    }

    protected virtual void Awake()
    {
        if (Rb == null)
            Rb = GetComponent<Rigidbody2D>();
        if (Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
            if (Animator == null)
                GameManager.instance.nullReference_debugLogWarning("animator", this.name, "Getting from child, I assume this doesnt have child with sprite?");
        }
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<Slider>();
            if (healthBar == null)
            {
                Debug.Log("NOT FOUND HEALTHBAR");
            }
        }

        //gameEvents = GameManager.instance.gameEvents;

    }

    protected virtual void Start()
    {        
        currentHealth = totalHealth;
        healthBar.maxValue = totalHealth;
        healthBar.value = currentHealth;
    }

    private void OnEnable()
    {
        //gameEvents.OnDamageTaken += PlayDamageTakenSound;
        //gameEvents.OnDamageTaken += LoseHealth;
        //gameEvents.OnDamageTaken += HealthBarUI;
    }

    private void OnDisable()
    {
        //gameEvents.OnDamageTaken -= PlayDamageTakenSound;
        //gameEvents.OnDamageTaken -= LoseHealth;
        //gameEvents.OnDamageTaken -= HealthBarUI;
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
        if(context.started) 
        {
            TakeDamageEvent(5);
        }
    }
    /// <summary>
    ///  Calls when taken damage, event will play sound of getting hit, lower hp
    /// </summary>
    public void TakeDamageEvent(int damage)
    {
        //gameEvents.InvokeTakeDamageEvent(damage);
    }

    void PlayDamageTakenSound(int _)
    {
        //GameManager.instance.audioManager.PlaySound("TestSound");
    }

    void LoseHealth(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
        }
        else
        {
            currentHealth = 0;
            gameEvents.InvokeKillingEvent();
        }
    }

    void HealthBarUI(int healthValue) // Used to add or deduct health, make the ui go up or down. current health is handled elsewhere
    {
        healthBar.value = currentHealth;
    }
}
