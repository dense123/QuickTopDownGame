using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private int maxInventorySlots = 32;
    public int MaxInventorySlots => maxInventorySlots;

    public System.Action InventorySizeChanged;


    protected override void Start()
    {
        base.Start();
        healthEvent.OnHealthEvent += GainExp;
    }

    public void GainExp()
    {
        currentExp += 10;
        LevelUp();
    }

    protected override void LevelUpAdditionalLogic()
    {
        base.LevelUpAdditionalLogic();
        maxInventorySlots++;
        InventorySizeChanged?.Invoke();
    }
}
