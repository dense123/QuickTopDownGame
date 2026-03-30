using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [Header("LEVEL STUFF")]
    [SerializeField] protected int currentLevel = 1;
    protected float currentExp;
    protected float exp_ToReach_ForLevelUp = 100f;
    protected float expMultiplier = 1.0f;

    //[Header("Misc")]
    public enum LastInputFacingNow
    {
        Up, Down, Left, Right
    }

    public LastInputFacingNow lastInputFacing;

    [Header("Initialise variables")]
    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }


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

    }
}
