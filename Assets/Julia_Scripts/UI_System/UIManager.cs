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

public class UIManager : MonoBehaviour
{
    [System.Serializable]
    public class UIScreen
    {
        public UIState state;
        public GameObject screenObject;
    }

    [SerializeField] private List<UIScreen> screens = new List<UIScreen>();
    [SerializeField] private UIState currentState = UIState.None;
    
    private Dictionary<UIState, GameObject> screenDictionary = new Dictionary<UIState, GameObject>();

    private void Awake()
    {
        // Initialize dictionary
        screenDictionary.Clear();
        
        foreach (var screen in screens)
        {
            if (screen.screenObject != null)
            {
                screenDictionary[screen.state] = screen.screenObject;
                // Hide all screens initially
                screen.screenObject.SetActive(false);
            }
        }

        // Show the initial state 
        if (currentState != UIState.None)
        {
            SetState(currentState);
        }
    }

    // put pause input logic here for now
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPause();
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

    
    public void ShowMainMenu()
    {
        SetState(UIState.MainMenu);
        GameStateMachine.Instance.ChangeState(GameState.Menu);
    }

    public void ShowGameplay()
    {
        SetState(UIState.Gameplay);
        GameStateMachine.Instance.ChangeState(GameState.Gameplay);
    }

    public void ShowPause()
    {
        SetState(UIState.Pause);
        GameStateMachine.Instance.ChangeState(GameState.Paused);
    }

    public void ShowGameOver()
    {
        SetState(UIState.GameOver);
        
    }

    public void ShowSettings()
    {
        SetState(UIState.Settings);
        GameStateMachine.Instance.ChangeState(GameState.Settings);
    }

    public void ShowResults()
    {
        SetState(UIState.Results);
        GameStateMachine.Instance.ChangeState(GameState.Results);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
