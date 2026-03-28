using NUnit.Framework;
using System.Collections.Generic;
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
    public GameObject genericItemUIPrefab;
    ItemDictionary itemDictionary;

    void Start()
    {
        player = GameManager.instance.Player;
        slotCount = player.MaxInventorySlots;

        transform.GetChild(0).TryGetComponent(out ItemDictionary itemDic);
        if (itemDic != null)
            itemDictionary = itemDic;
        else
            GameManager.instance.nullReference_debugLogWarning("itemDictionary", this.name);

            /*        //for (int i = 0; i < player.MaxInventorySlots; i++)
                    //{
                    //    GameObject obj = Instantiate(slotPrefab, inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform);
                    //    obj.name = $"Slot {i}";
                    //    if(i < itemPrefab.Length)
                    //    {
                    //        Instantiate(itemPrefab[i], obj.transform);
                    //    }
                    //}
            */
        player.InventorySizeChanged += UpdateSlots;
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

    // For saving game for inventory
    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();

        // Getting the slots gameobjects, if using too much computing power
        // set a serialized field and put in the slots there.
        foreach(Transform slotTransform in inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot.CurrentItemInSlot != null)
            {
                Item item = slot.CurrentItemInSlot.GetComponent<Item>();
                invData.Add(new InventorySaveData { ItemID = item.ID, SlotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return invData;
    }

    // For loading inside inventory
    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        // Destroy duplicates apparently
        foreach (Transform slotTransform in inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform)
        {
            Destroy(slotTransform.gameObject);
        }
        for (int i = 0; i < slotCount; i++)
        {
            GameObject obj = Instantiate(slotPrefab, inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform);
            obj.name = $"Slot {i}";
        }

        foreach (InventorySaveData loadedData in inventorySaveData)
        {
            // Get allSlots gameobject
            Slot slot = inventoryPage.GetComponentInChildren<GridLayoutGroup>().transform.GetChild(loadedData.SlotIndex).GetComponent<Slot>();
            if (slot != null)
            {
                Debug.Log(slot);
            }
            if (itemDictionary != null)
            {
                if (loadedData.SlotIndex < slotCount)
                {
                    Item ItemData = itemDictionary.GetItemData(loadedData.ItemID);
                    if (ItemData != null)
                    {
                        GameObject obj = Instantiate(genericItemUIPrefab, slot.transform);

                        // send item data into script object handler to display whatever
                        obj.GetComponent<ItemScriptObjectHandler>().itemData = ItemData;
                        // have a script inside the item prefab to store and display all these


                        slot.CurrentItemInSlot = genericItemUIPrefab;
                    }
                    else
                        Debug.Log($"Under {this.name}, no itemPrefab");
                }
            }
            else
                GameManager.instance.nullReference_debugLogWarning("itemDictionary", this.name, "This is under SetInventoryItems");
        }
    }
}
