using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveFilesHandler : MonoBehaviour
{
    private string SaveLocation;
    private List<SaveData> saveData;

    private void Awake()
    {
        //yield return new WaitUntil(() => GameManager.instance != null);
        SaveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public List<SaveData> GetSaveFiles()
    {
        if (File.Exists(SaveLocation))
        {
            // Send file over to next scene where it will then be read and loaded and applied to the player and inventory
            Debug.Log("Save file exists");
            saveData.Add(JsonUtility.FromJson<SaveData>(File.ReadAllText(SaveLocation)));
            Debug.Log(saveData);
            return saveData;
        }
        else
        {
            Debug.Log("No save file found");
            return null;
        }
    }
}
