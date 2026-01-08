using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Gameplay")]
public class GameplayState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 1f;
        GameStateEvents.RaiseGameResumed();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
