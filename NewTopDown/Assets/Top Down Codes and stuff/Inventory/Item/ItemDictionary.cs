using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{

    public List<Item> ItemPrefabs;
    Dictionary<int, GameObject> itemDictionary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = new Dictionary<int, GameObject>();

        for (int i = 0; i < ItemPrefabs.Count; i++)
        {
            if (ItemPrefabs[i] != null)
            {
                ItemPrefabs[i].ID = i + 1;
            }
        }
        foreach (Item item in ItemPrefabs)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int ItemID)
    {
        itemDictionary.TryGetValue(ItemID, out GameObject item);
        if (item == null)
            Debug.LogWarning($"{ItemID} has nothing !!!!!");
        return item;
    }
}
