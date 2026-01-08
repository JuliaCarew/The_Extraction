using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Paused")]
public class PausedState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;
        GameStateEvents.RaiseGamePaused();
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
    }

    public override void Update() {}
}
