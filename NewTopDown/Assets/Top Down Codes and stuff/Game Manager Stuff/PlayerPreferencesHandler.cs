using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferencesHandler : MonoBehaviour
{

    [Header("DEBUG STUFF")]
    [SerializeField] GameObject DEBUG_TEXT_GENERAL;
    [SerializeField] Transform menuCanvas;
    public GameObject SettingsPage;
    Player player;

    private void Awake()
    {
        SettingsPage.GetComponentInChildren<Toggle>().onValueChanged.AddListener(SetDefaultWalk_Sprint);// returns bool whenever changes
    }

    private void Start()
    {
        player = GameManager.instance.Player;
        if (menuCanvas == null)
        {
            GameManager.instance.nullReference_debugLogWarning("Menu Canvas", this.name);
        }
        if (SettingsPage == null)
        {
            GameManager.instance.nullReference_debugLogWarning("Settings Page", this.name);
        }
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

    public void DisplayDebugText(string text)
    {
        // Need to be under canvas gameobject to display
        GameObject obj = Instantiate(DEBUG_TEXT_GENERAL, menuCanvas); // Might have errors in future, but lazy now
        obj.GetComponent<TextMeshProUGUI>().SetText(text);
        //obj.transform.position = 
        Destroy(obj, 5f);
    }
}
