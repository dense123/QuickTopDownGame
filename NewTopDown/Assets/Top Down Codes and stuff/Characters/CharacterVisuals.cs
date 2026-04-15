using UnityEngine;
using UnityEngine.UI;

public class CharacterVisuals : MonoBehaviour
{

    private GameObject interactionIndicator;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Animator healthBarAnimator;
    private float totalHealth;
    private float currentHealth;
    [SerializeField] private float lowHealthPercentage = .3f;

    private enum HealthBarState
    {
        Normal,
        LowHealth
    }

    private HealthBarState currentHealthState;

    [Header("Initialisation stuff")]
    private Character character;

    private void Awake()
    {
        TryGetComponent(out Character character);
        if (character == null)
        {
            Debug.Log("NO CHARACTER COMPONENT FOUND ON " + gameObject.name);
            enabled = false;
            return;
        }
        else
        {
            this.character = character;
        }


        // Move to separate script as its UI
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<Slider>();
            if (healthBar == null)
            {
                Debug.Log("NOT FOUND HEALTHBAR");
                enabled = false;
                return;
            }
        }
    }

    private void Start()
    {
        totalHealth = character.TotalHealth;
        currentHealth = character.CurrentHealth;
        healthBar.maxValue = totalHealth;
        healthBar.value = currentHealth;
        Debug.Log($"{this.name} has total health of {totalHealth} and current health of {currentHealth}");
    }

    private void OnEnable()
    {
        character.OnCharacterTakeDamage += HealthBarUI;
        character.OnCharacterDeath += CharacterDeathChangeColor;
    }

    private void CharacterDeathChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.gray; // Change color to gray when character dies, can change to something else if needed
    }

    private void OnDisable()
    {
        character.OnCharacterTakeDamage -= HealthBarUI;
        character.OnCharacterDeath -= CharacterDeathChangeColor;
    }

    void HealthBarUI(int healthValue) // Used to add or deduct health, make the ui go up or down. current health is handled elsewhere
    {
        healthBar.value -= healthValue;
        currentHealth -= healthValue;
        if (healthBar.value != currentHealth)
        {
            Debug.Log("Health bar value is not equal to current health. Health bar value: " + healthBar.value + " Current health: " + currentHealth);
            return;
        }
        if (healthBar.value <= totalHealth * lowHealthPercentage)
        {
            Debug.Log("WARNING LOW HEALTH");
            healthBarAnimator.SetBool("IsHealthLow", true);
            // Maybe make the bar shake, or change color to red, or something to indicate low health
        }
    }
}