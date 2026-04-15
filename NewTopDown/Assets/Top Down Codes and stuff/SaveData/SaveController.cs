using System.Collections;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    private string SaveLocation;
    private string SettingsSaveLocation;
    Player player;
    InventoryController inventoryController;
    PlayerPreferencesHandler playerPreferencesHandler;


    private IEnumerator Start()
    {
        SaveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        SettingsSaveLocation = Path.Combine(Application.persistentDataPath, "settingsData.json");
        
        yield return new WaitUntil(() => GameManager.Instance != null);
        player = GameManager.Instance.Player;
        if (player == null)
            yield return null;

        playerPreferencesHandler = GameManager.Instance.playerPrefsHandler;

        inventoryController = GameManager.Instance.InventoryController;
        if (GetComponent<InventoryController>() == null)
            Debug.LogWarning($"{this.name} doesn't have reference to Inventory Controller");

    }

    private void OnDisable()
    {
    }


    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position,
            inventorySaveData = inventoryController.GetInventoryItems()
        };

        File.WriteAllText(SaveLocation, JsonUtility.ToJson(saveData));
        playerPreferencesHandler.DisplayDebugText(JsonUtility.ToJson(saveData).ToString());
    }

    public void SaveSettings()
    {
        SettingsData settingsData = new SettingsData
        {
            isDefaultWalking = playerPreferencesHandler.GetDefaultWalk_Sprint()
        };

        File.WriteAllText(SettingsSaveLocation, JsonUtility.ToJson(settingsData));
        playerPreferencesHandler.DisplayDebugText(JsonUtility.ToJson(settingsData).ToString());
    }

    // Will be called when play button event is triggered
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

            playerPreferencesHandler.SetDefaultWalk_Sprint(settingsData.isDefaultWalking);
        }

        else
            SaveSettings();
    }

}
