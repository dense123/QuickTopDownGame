using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

/// <summary>
/// The GameManager is the interface between all the system in the game. It is either instantiated by the Loading scene
/// which is the first at the start of the game, or Loaded from the Resource Folder dynamically at first access in editor
/// so we can press play from any point of the game without having to add it to every scene and test if it already exist
/// </summary>
[DefaultExecutionOrder(-9999)]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance
    {
        get
        {
            // In Editor, the instance can be crated on the fly so we can play any scene without setup to do.
            // In a build, the first scene will Init all that so we are sure there will already be an instance.
#if UNITY_EDITOR
            if (s_Instance == null)
            {
                var newInstance = Instantiate(Resources.Load<GameManager>("Game Manager"));
                newInstance.Awake();
            }
#endif
            return s_Instance;
        }

        private set => s_Instance = value;
    }

    private static GameManager s_Instance;
    public bool IsInitialised { get; private set; } = false;

    [SerializeField] public PlayerPreferencesHandler playerPrefsHandler;
    [SerializeField] public ControlsHandler controlsHandler;
    [SerializeField] public AudioManager audioManager;
    [SerializeField] public SaveFilesHandler saveFilesHandler;
    //[SerializeField] ItemDictionary itemDictionary;

    private GameEvents gameEvents;
    public GameEvents GameEvents { get => gameEvents; private set => gameEvents = value; }
    //[SerializeField] private Player player;
    //public Player Player { get => player; private set => player = value; }
    public Player Player { get; private set;  }
    public GameObject MenuCanvas { get; private set; }
    public GameObject PauseMenu { get; private set; }
    public GameObject InventoryPage { get; private set; }
    public GameObject SettingsPage { get; private set; }
    public InventoryController InventoryController { get; private set; }

    public List<Enemy> Enemies;



    private void Awake()
    {
        Debug.Log("Game Manager Awake" + SceneManager.GetActiveScene().name);
        if (s_Instance == this)
        {
            return;
        }
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);

            gameEvents = new GameEvents();
        }
    }
    private void OnEnable()
    {
        Debug.Log("Game Manager Enabled" + SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        IsInitialised = true;
        Debug.Log("Game Manager Started" + SceneManager.GetActiveScene().name);
    }


    public void RegisterPlayer(Player player)
    {
        this.Player = player;
    }

    // Maybe make the canvas a prefab and preset the reference
    public void RegisterMenuCanvas(CanvasMenu canvas)
    {
        if(canvas == null)
        {
            Debug.LogWarning("Not pause menu! Or wrong spelling");
            return;
        }
        this.MenuCanvas = canvas.gameObject;
        this.PauseMenu = canvas.PauseMenu;
        this.InventoryPage = canvas.InventoryPage;
        this.SettingsPage = canvas.SettingsPage;

    }

    public void RegisterInventoryController(InventoryController inventoryController)
    {
        this.InventoryController = inventoryController;
    }

    //public void RegisterMultipleEnemies(Enemy enemy) // not sure how to use, enemy will be all over no?
    //{
    //    Enemies.Add(enemy);
    //}

    public void ResetGameDEBUG()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void nullReference_debugLogWarning(string var_Name, string script_Name)
    {
        Debug.LogWarning($"{var_Name} is null under {script_Name}");
    }
    public void nullReference_debugLogWarning(string var_Name, string script_Name, string additional_Text)
    {
        Debug.LogWarning($"{var_Name} is null under {script_Name}. {additional_Text}");
    }

    Transform GetGameManagerChildObject(string objectTag)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(objectTag))
            {
                return child;
            }
        }
        Debug.Log($"Game Object with tag '{objectTag}' doesn't exist!");
        nullReference_debugLogWarning("Get Game Manager Child Object", this.name);
        return null;
    }
}
