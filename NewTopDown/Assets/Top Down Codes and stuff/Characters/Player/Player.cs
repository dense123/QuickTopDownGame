using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private int maxInventorySlots = 32;
    public int MaxInventorySlots => maxInventorySlots;

    public System.Action InventorySizeChanged;



    public void GainExp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentExp += 10;
            LevelUp();
        }
    }

    protected override void LevelUpAdditionalLogic()
    {
        base.LevelUpAdditionalLogic();
        maxInventorySlots++;
        InventorySizeChanged?.Invoke();
    }
}
