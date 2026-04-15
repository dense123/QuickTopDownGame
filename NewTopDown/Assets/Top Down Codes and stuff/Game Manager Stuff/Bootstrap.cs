using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string nextScene = "MainMenu";
    [SerializeField] private GameObject gameManagerPrefab;

    private void Awake()
    {   
        //// Prevent duplicates if scene reloads
        //if (FindObjectsOfType<Bootstrap>().Length > 1)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        //DontDestroyOnLoad(gameObject);

        //InitializeSystems();
    }

    private void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void InitializeSystems()
    {
        // Example:
        // Instantiate managers or ensure they exist

        if (GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }

        // Repeat for other managers if needed
    }

    // Game manager initialise variables
    /*
     Load Player
    Load Menu
    Load Save data, preferences, inventory
    (move player to position)


        All Scripts

    GameManager
    AudioManager    Sound
    GameEvents
    ControlsHandler
    PlayerPreferencesHandler
    Settings
    SettingsData
    InventoryController InventorySaveData
    MenuController
    TabController
    ItemDictionary
    SaveController  SaveData

    Character
        Player
    PlayerMovement
    PlayerInteract
    PlayerRaycasting
    
    Interactable
        InteractableBasicObjects
    Item
    ItemDragHandler
    Slot

     */

    //// On Start Up?----------------------------------------
    //GameManager                                    ;
    //    GameEvents                                     ;
    //    AudioManager                                   ;
    //    // where will player input be
    //    ControlsHandler                                ;
    //    // Will remember Preferences, run before savecontroller?
    //    PlayerPreferencesHandler                       ;
    //    // Need to separate save controller for inventory and settings(preferences)
    //    // SettingsSaveController.cs
    //    SaveController;  SaveData                       ;


   
                                                   
    //Character                                      ;
    //    Player                                     ;
    //    PlayerMovement                                 ;
    //    PlayerInteract                                 ;
    //    PlayerRaycasting                               ;

    //    // Load player first
                                                   
    //// Pause Menu ---------------------------------------
    //MenuController                                 ;
    //// Not using in menu, player hasnt chosen new game or load game
    //InventoryController; InventorySaveData          ; 
    //TabController                                  ;
    //ItemDictionary                                 ;
    //// GameSaveController.cs (inventory and position, soon to add other things??)
    
    //Interactable                                   ;
    //    InteractableBasicObjects                   ;
    //Item                                           ;
    //ItemDragHandler                                ;
    //Slot                                           ;

}
