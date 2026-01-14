using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Menu")]
public class MenuState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;
        Debug.Log($"MenuState.Enter() called - Time.timeScale set to: {Time.timeScale}");
        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.RaiseGameStarted();
        }
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
