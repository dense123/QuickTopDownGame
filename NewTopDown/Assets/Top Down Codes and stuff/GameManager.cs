using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [SerializeField] private PlayerMovement playermovement;
    public PlayerMovement PlayerMovement => playermovement;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        
    }

    public void SetDefaultWalk_Sprint(Toggle toggle)
    {
        playermovement.IsDefaultWalking = toggle.isOn;
    }

}
