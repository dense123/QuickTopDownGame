using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inventoryPage;
    [SerializeField] GameObject settingsPage;
    public GameObject PauseMenu => pauseMenu;
    public GameObject InventoryPage => inventoryPage;
    public GameObject SettingsPage => settingsPage;

    [SerializeField] Button playButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;


    private IEnumerator Start()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);
        GameManager.Instance.RegisterMenuCanvas(this);
        if (playButton != null)
        {
            playButton.onClick.AddListener(Play);
            loadButton.onClick.AddListener(Load);
            settingsButton.onClick.AddListener(OpenSettings);
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //GameManager.instance.GameEvents.InvokePlayButtonPressedEvent();
        Debug.Log("Play Button Pressed");
    }
    public void Load()
    {
        GameManager.Instance.GameEvents.InvokeLoadButtonPressedEvent();
        Debug.Log("Load Button Pressed");
    }
    public void OpenSettings()
    {
        GameManager.Instance.GameEvents.InvokeSettingsButtonPressedEvent();
        Debug.Log("Settings Button Pressed");
    }
    public void QuitGame()
    {
        // Maybe add a confirmation popup before quitting the game
        GameManager.Instance.GameEvents.InvokeQuitButtonPressedEvent();
        Debug.Log("Quit Button Pressed");
        Application.Quit();
    }
}
