using System.Collections;
using UnityEngine;

public class TestSceneInitialiser : MonoBehaviour
{
    [Header("So I dont have to start at bootstrap scene")]
    public CanvasMenu MenuCanvas;
    public Player player;

    public GameObject PauseMenu;
    public GameObject InventoryPage;
    public GameObject SettingsPage;

    public InventoryController InventoryController;

    //public Character character; //For both player and enemy

    //public void SetCharacter(Character character)
    //{
    //    this.character = character;
    //}

    IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.RegisterPlayer(player);
        GameManager.Instance.RegisterMenuCanvas(MenuCanvas);
        GameManager.Instance.RegisterInventoryController(InventoryController);
    }

}
