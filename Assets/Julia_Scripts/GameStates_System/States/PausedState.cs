using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Paused")]
public class PausedState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;
        Debug.Log($"PausedState.Enter() called - Time.timeScale set to: {Time.timeScale}");
        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.RaiseGamePaused();
        }
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
    }

    public override void Update() {}
}
