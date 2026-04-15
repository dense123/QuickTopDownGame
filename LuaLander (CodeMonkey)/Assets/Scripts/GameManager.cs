using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }
    public GameEventsNested GameEvents { get; private set; }
    public ControlsHandler ControlsHandler { get; private set; }

    public class GameEventsNested
    {
        public event Action OnPlayerDeath;
        public event Action OnLevelComplete;
        public void PlayerDied()
        {
            OnPlayerDeath?.Invoke();
        }
        public void LevelCompleted()
        {
            OnLevelComplete?.Invoke();
        }

    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        /*
               // In Editor, the instance can be crated on the fly so we can play any scene without setup to do.
               // In a build, the first scene will Init all that so we are sure there will already be an instance.
                #if UNITY_EDITOR
               if (s_Instance == null && !s_IsShuttingDown)
               {
                   var newInstance = Instantiate(Resources.Load<GameManager>("GameManager"));
                   newInstance.Awake();
               }
                #endif
        */
        GameEvents = new GameEventsNested();
        ControlsHandler = GetComponentInChildren<ControlsHandler>();
    }

    private void OnEnable()
    {
    }

    void Start()
    {
    }

}
