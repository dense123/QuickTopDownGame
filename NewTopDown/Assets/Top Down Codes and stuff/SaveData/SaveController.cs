using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    private string SaveLocation;
    private string SettingsSaveLocation;
    Player player;
    InventoryController inventoryController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.instance.Player;
        if (GetComponent<InventoryController>() != null)
            inventoryController = GetComponent<InventoryController>();
        else
            Debug.LogWarning($"{this.name} doesn't have reference to Inventory Controller");
        SaveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        SettingsSaveLocation = Path.Combine(Application.persistentDataPath, "settingsData.json");

        LoadGame();
    }


    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(SaveLocation, JsonUtility.ToJson(saveData));
        GameManager.instance.DisplayDebugText(JsonUtility.ToJson(saveData).ToString());
    }

    public void SaveSettings()
    {
        SettingsData settingsData = new SettingsData
        {
            isDefaultWalking = GameManager.instance.GetDefaultWalk_Sprint()
        };

        File.WriteAllText(SettingsSaveLocation, JsonUtility.ToJson(settingsData));
        GameManager.instance.DisplayDebugText(JsonUtility.ToJson(settingsData).ToString());
    }

    public void LoadGame()
    {
        if (File.Exists(SaveLocation)) 
        { 
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(SaveLocation));

            player.transform.position = saveData.playerPosition; 
            inventoryController.SetInventoryItems(saveData.inventorySaveData);
        }
        else
            SaveGame();

        if (File.Exists(SettingsSaveLocation))
        {
            SettingsData settingsData = JsonUtility.FromJson<SettingsData>(File.ReadAllText(SettingsSaveLocation));

            GameManager.instance.SetDefaultWalk_Sprint(settingsData.isDefaultWalking);
        }

        else
            SaveSettings();
    }

}
