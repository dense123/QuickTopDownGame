using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] private Player player;
    public Player Player => player;

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

}
