using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Menu")]
public class MenuState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;
        GameStateEvents.RaiseGameStarted();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
