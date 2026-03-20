using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    // NOT USED
    //private int slotCount = 32; // max 32, able to change depending on the game, upgrade?
    
    private Player player;
    private int slotCount;

    public GameObject inventoryPage;
    public GameObject slotPrefab;
    public GameObject[] itemPrefab;

    void Start()
    {
        player = GameManager.instance.Player;
        slotCount = player.MaxInventorySlots;
        for (int i = 0; i < player.MaxInventorySlots; i++)
        {
            GameObject obj = Instantiate(slotPrefab, inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform);
            obj.name = $"Slot {i}";
            if(i < itemPrefab.Length)
            {
                Instantiate(itemPrefab[i], obj.transform);
            }
        }

        player.InventorySizeChanged += UpdateSlots;
    }

    void Update()
    {
        
    }

    void UpdateSlots()
    {
        if (slotCount < player.MaxInventorySlots)
        {
            slotCount = player.MaxInventorySlots;
            GameObject obj = Instantiate(slotPrefab, inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform);
            obj.name = "NEW SLOT";
        }
    }
}
