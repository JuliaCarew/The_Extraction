using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
// changing menu state needs to change game state too
public enum UIState
{
    None,
    MainMenu,
    Gameplay,
    Pause,
    Results,
    GameOver,
    Settings
}

public class UIManager : SingletonBase<UIManager>
{
    [System.Serializable]
    public class UIScreen
    {
        public UIState state;
        public GameObject screenObject;
    }

    [System.Serializable]
    public class StateMapping
    {
        public GameState gameState;
        public UIState uiState;
    }

    [SerializeField] private List<UIScreen> screens = new List<UIScreen>();
    [SerializeField] private List<StateMapping> stateMappings = new List<StateMapping>();
    [SerializeField] private UIState currentState = UIState.None;
    
    private Dictionary<UIState, GameObject> screenDictionary = new Dictionary<UIState, GameObject>();
    private Dictionary<GameState, UIState> gameToUIMapping = new Dictionary<GameState, UIState>();
    public LevelManager levelManager;

    private void Awake()
    {
        base.Awake();
        // Initialize dictionary
        screenDictionary.Clear();
        
        foreach (var screen in screens)
        {
            if (screen.screenObject != null)
            {
                screenDictionary[screen.state] = screen.screenObject;
                screen.screenObject.SetActive(false);
            }
        }

        // Initialize state mapping
        gameToUIMapping.Clear();
        foreach (var mapping in stateMappings)
        {
            gameToUIMapping[mapping.gameState] = mapping.uiState;
        }
    }

    private void OnEnable()
    {
        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.StateChanged += HandleGameStateChanged;
        }
    }

    private void OnDisable()
    {
        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.StateChanged -= HandleGameStateChanged;
        }
    }

    private void Start()
    {
        // Sync with initial game state
        if (GameStateMachine.Instance != null)
        {
            HandleGameStateChanged(GameStateMachine.Instance.GetCurrentState());
        }
    }

    // put pause input logic here for now
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause
            if (GameStateMachine.Instance != null)
            {
                GameState current = GameStateMachine.Instance.GetCurrentState();

                if (current == GameState.Paused)
                {
                    GameStateMachine.Instance.ChangeState(GameState.Gameplay);
                }
                else if (current == GameState.Gameplay)
                {
                    GameStateMachine.Instance.ChangeState(GameState.Paused);
                }
            }
        }
    }

    public void SetState(UIState newState)
    {
        // Hide current state screen
        if (currentState != UIState.None && screenDictionary.ContainsKey(currentState))
        {
            screenDictionary[currentState].SetActive(false);
        }

        // Update current state
        currentState = newState;

        // Show new state screen
        if (currentState != UIState.None && screenDictionary.ContainsKey(currentState))
        {
            screenDictionary[currentState].SetActive(true);
        }
    }

    public UIState GetCurrentState()
    {
        return currentState;
    }

    public void ShowScreen(UIState state)
    {
        SetState(state);
    }

    public void HideAllScreens()
    {
        foreach (var screen in screenDictionary.Values)
        {
            if (screen != null)
            {
                screen.SetActive(false);
            }
        }
        currentState = UIState.None;
    }

    public void ShowGameOver() { ChangeGameState(GameState.GameOver); }
    public void ShowMainMenu(){ ChangeGameState(GameState.Menu); }
    public void ShowGameplay()
    { 
        // if not in pause state
        if(GameStateMachine.Instance != null && GameStateMachine.Instance.GetCurrentState() != GameState.Paused)
        {
            levelManager.RetryCurrentLevel();
        }
        ChangeGameState(GameState.Gameplay); 
    }
    public void ShowPause(){ ChangeGameState(GameState.Paused); }
    public void ShowSettings(){ ChangeGameState(GameState.Settings); }
    public void ShowResults(){ ChangeGameState(GameState.Results); }

    public void Retry()
    {
        levelManager.RetryCurrentLevel();
    }

    private void ChangeGameState(GameState newState)
    {
        if (GameStateMachine.Instance != null)
        {
            GameStateMachine.Instance.ChangeState(newState);
        }
    }

    private void HandleGameStateChanged(GameState newGameState)
    {
        if (gameToUIMapping.ContainsKey(newGameState))
        {
            SetState(gameToUIMapping[newGameState]);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
