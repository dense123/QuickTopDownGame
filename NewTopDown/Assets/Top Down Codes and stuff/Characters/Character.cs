using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Character : MonoBehaviour
{

    [SerializeField] protected int currentLevel = 1;
    protected float currentExp;
    protected float exp_ToReach_ForLevelUp = 100f;
    protected float expMultiplier = 1.0f;

    public int CurrentLevel { 
        
        get => currentLevel;
        set
        {
            currentLevel = value;
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
