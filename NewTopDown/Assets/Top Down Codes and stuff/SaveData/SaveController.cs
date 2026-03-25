using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    private string SaveLocation;
    Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameManager.instance.Player;
        SaveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");

        LoadGame();
    }


    public void SaveGame()
    {
        SaveData saveData = new SaveData{
            playerPosition = player.transform.position
        };


        File.WriteAllText(SaveLocation, JsonUtility.ToJson(saveData));
        GameManager.instance.DisplayDebugText(JsonUtility.ToJson(saveData).ToString());
    }


    public void LoadGame()
    {
        if (File.Exists(SaveLocation)) 
        { 
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(SaveLocation));

            player.transform.position = saveData.playerPosition; 
        }
        else
            SaveGame();
    }

}
