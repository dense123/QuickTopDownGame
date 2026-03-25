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

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void SetDefaultWalk_Sprint(Toggle toggle)
    {
        player.GetComponent<PlayerMovement>().IsDefaultWalking = toggle.isOn;
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

}
