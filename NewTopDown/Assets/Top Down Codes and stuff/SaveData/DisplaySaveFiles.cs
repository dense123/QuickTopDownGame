using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySaveFiles : MonoBehaviour
{
    [SerializeField] SaveFilesHandler saveFilesHandler;
    [SerializeField] List<Button> loadFileButtons;

    private void OnEnable()
    {
        saveFilesHandler = GameManager.Instance.saveFilesHandler;
        if (saveFilesHandler == null)
        {
            Debug.LogWarning($"{this.name} doesn't have reference to SaveFilesHandler");
            return;
        }
        DisplaySaveData();
    }

    public void DisplaySaveData()
    {
        if (saveFilesHandler.GetSaveFiles().Count > 0)
        {
            for(int i = 0; i < saveFilesHandler.GetSaveFiles().Count; i++)
            {
                SaveData save = saveFilesHandler.GetSaveFiles()[i];
                Debug.Log($"Player Position: {save.playerPosition}");
                Debug.Log($"Inventory Save Data: {save.inventorySaveData}");
            }
        }
        else
        {
            Debug.Log("No save data to display");
        }
    }
}
