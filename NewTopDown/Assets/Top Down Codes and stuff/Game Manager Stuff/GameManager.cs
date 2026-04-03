using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] PlayerPreferencesHandler playerPrefsHandler;
    [SerializeField] ControlsHandler controlsHandler;
    [SerializeField] ItemDictionary itemDictionary;
    [SerializeField] AudioManager audioManager;
    
    GameEvents gameEvents;

    [SerializeField] private Player player;
    public Player Player { get => player; private set => player = value; }



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        gameEvents = new GameEvents();

    }

    private void Start()
    {

    }

    public void RegisterPlayer(Player newPlayer)
    {
        player = newPlayer;
    }

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
