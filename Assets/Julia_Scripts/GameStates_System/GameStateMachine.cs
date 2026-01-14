using UnityEngine;
using System.Collections.Generic;

public class GameStateMachine : SingletonBase<GameStateMachine>
{
    [Header("States")]
    [SerializeField] private List<GameStateSO> availableStates;

    private Dictionary<GameState, IState> states;
    private IState currentState;
    public bool debugMode = false;

    private void Awake()
    {
        base.Awake();

        states = new Dictionary<GameState, IState>();

        foreach (var state in availableStates)
        {
            if (!states.ContainsKey(state.Id))
                states.Add(state.Id, state);
        }
    }

    private void Start()
    {
        ChangeState(GameState.Menu);
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(GameState newState)
    {
        if (currentState?.Id == newState)
            return;

        if (!states.ContainsKey(newState))
        {
            return;
        }

        Debug.Log($"GameStateMachine: Changing from {currentState?.Id} to {newState}. Time.timeScale before: {Time.timeScale}");
        
        currentState?.Exit();
        
        currentState = states[newState];
        if (currentState == null)
        {
            Debug.LogError($"GameStateMachine: State object for {newState} is null!");
            return;
        }
        
        try
        {
            currentState.Enter();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"GameStateMachine: Exception in Enter() method: {e.Message}\n{e.StackTrace}");
        }

        // Ensure timeScale is set correctly for pause / gameplay states
        if (newState == GameState.Paused || newState == GameState.Menu || newState == GameState.GameOver)
        {
            if (Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
            }
        }
        else if (newState == GameState.Gameplay)
        {
            if (Time.timeScale != 1f)
            {
                Time.timeScale = 1f;
            }
        }

        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.RaiseStateChanged(newState);
        }
    }

    public GameState GetCurrentState()
    {
        return currentState?.Id ?? GameState.Menu;
    }
}