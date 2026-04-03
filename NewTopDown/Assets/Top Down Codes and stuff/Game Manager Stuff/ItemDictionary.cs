using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemData;
    Dictionary<int, Item> itemDictionary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = new Dictionary<int, Item>();

        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemData[i] != null)
            {
                itemData[i].ID = i + 1;
            }
        }
        foreach (Item item in itemData)
        {
            itemDictionary[item.ID] = item;
        }
    }

    public Item GetItemData(int ItemID)
    {
        itemDictionary.TryGetValue(ItemID, out Item item);
        if (item == null)
            Debug.LogWarning($"{ItemID} has nothing !!!!!");
        return item;
    }
}
