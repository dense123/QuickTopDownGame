using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private int maxInventorySlots = 32;
    public int MaxInventorySlots => maxInventorySlots;

    public System.Action InventorySizeChanged;// Wtf is this

    private ControlsHandler controlsHandler;

    // This is used for the player attack movement disable, it's changed under PlayerAttack ,
    // and is used in PlayerMovement to determine how long to disable movement for when attacking
    private float Player_TimeDelay_ForAttackMovementDisable;
    public float GetPlayer_TimeDelay_ForAttackMovementDisable()
    {
        return Player_TimeDelay_ForAttackMovementDisable;
    }
    public void SetPlayer_TimeDelay_ForAttackMovementDisable(float timeDelay)
    {
        Player_TimeDelay_ForAttackMovementDisable = timeDelay;
    }


    protected override void Awake()
    {
        base.Awake();
        controlsHandler = GameManager.Instance.controlsHandler;
        Debug.Log("Player Awake");
    }



    protected override void Start()
    {
        base.Start();
        Debug.Log("Player Start");
        GameManager.Instance.RegisterPlayer(this);
    }
    private void OnEnable()
    {
        Debug.Log("Player OnEnable");
        controlsHandler.InputSystem_Actions_PlayerInput.Player.Enable();
        controlsHandler.InputSystem_Actions_PlayerInput.Player.TakeDamageDebug.performed += TakeDamageDebug;
    }

    private void OnDisable()
    {
        controlsHandler.InputSystem_Actions_PlayerInput.Player.TakeDamageDebug.performed -= TakeDamageDebug;
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
