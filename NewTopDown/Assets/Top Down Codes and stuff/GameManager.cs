using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    [SerializeField] private Player player;
    public Player Player => player;
    
    [SerializeField] GameObject DEBUG_TEXT_GENERAL;
    public GameObject SettingsPage;

    private void Awake()
    {
        instance = this;
        SettingsPage.GetComponentInChildren<Toggle>().onValueChanged.AddListener(SetDefaultWalk_Sprint);// returns bool whenever changes
    }

    private void Update()
    {
        
    }


    public void SetDefaultWalk_Sprint(bool isDefaultWalking)
    {
        player.GetComponent<PlayerMovement>().IsDefaultWalking = isDefaultWalking;
        SettingsPage.GetComponentInChildren<Toggle>().isOn = isDefaultWalking;
    }

    public bool GetDefaultWalk_Sprint()
    {
        return player.GetComponent<PlayerMovement>().IsDefaultWalking;
    }

    public void ResetGameDEBUG()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayDebugText(string text)
    {
        // Need to be under canvas gameobject to display
        GameObject obj = Instantiate(DEBUG_TEXT_GENERAL, FindFirstObjectByType<Canvas>().transform); // Might have errors in future, but lazy now
        obj.GetComponent<TextMeshProUGUI>().SetText(text);
        //obj.transform.position = 
        Destroy(obj, 5f);
    }

    public void nullReference_debugLogWarning(string var_Name, string script_Name)
    {
        Debug.LogWarning($"{var_Name} is null under {script_Name}");
    }
    public void nullReference_debugLogWarning(string var_Name, string script_Name, string additional_Text)
    {
        Debug.LogWarning($"{var_Name} is null under {script_Name}. {additional_Text}");
    }
}
