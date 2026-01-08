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

        currentState?.Exit();
        currentState = states[newState];
        currentState.Enter();

        GameStateEvents.RaiseStateChanged(newState);
        if (debugMode) Debug.Log($"Game State changed to: {newState}");
    }
}