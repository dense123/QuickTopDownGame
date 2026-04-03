using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private int maxInventorySlots = 32;
    public int MaxInventorySlots => maxInventorySlots;

    public System.Action InventorySizeChanged;

    GameEvents gameEvents;

    protected override void Start()
    {
        base.Start();
        //gameEvents = GameManager.instance.gameEvents;
        //gameEvents.OnKillingEvent += GainExp;
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
