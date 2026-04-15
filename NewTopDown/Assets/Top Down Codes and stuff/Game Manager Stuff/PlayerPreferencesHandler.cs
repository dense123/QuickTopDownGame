using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreferencesHandler : MonoBehaviour
{

    [Header("DEBUG STUFF")]
    [SerializeField] GameObject DEBUG_TEXT_GENERAL;
    [SerializeField] GameObject menuCanvas;
    public GameObject SettingsPage;
    Player player;

    private void Awake()
    {
        //SettingsPage.GetComponentInChildren<Toggle>().onValueChanged.AddListener(SetDefaultWalk_Sprint);// returns bool whenever changes
    }

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        player = GameManager.Instance.Player;
        menuCanvas = GameManager.Instance.MenuCanvas;
        SettingsPage = GameManager.Instance.SettingsPage;
        if (menuCanvas == null)
        {
            GameManager.Instance.nullReference_debugLogWarning("Menu Canvas", this.name);
        }
        if (SettingsPage == null)
        {
            GameManager.Instance.nullReference_debugLogWarning("Settings Page", this.name);
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
        GameObject obj = Instantiate(DEBUG_TEXT_GENERAL, menuCanvas.transform); // Might have errors in future, but lazy now
        obj.GetComponent<TextMeshProUGUI>().SetText(text);
        //obj.transform.position = 
        Destroy(obj, 5f);
    }
}
