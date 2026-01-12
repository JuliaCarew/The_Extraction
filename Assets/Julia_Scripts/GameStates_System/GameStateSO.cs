using UnityEngine;


public abstract class GameStateSO : ScriptableObject, IState
{
    [SerializeField] private GameState id;
    public GameState Id => id;

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
