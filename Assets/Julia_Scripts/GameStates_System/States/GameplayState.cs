using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Gameplay")]
public class GameplayState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 1f;
        GameStateEvents.Instance.RaiseGameResumed();
        Debug.Log("entered Gameplay state");
        // Good for now but in future will grab current track from level manager.
        AudioLibrary.Instance.PlayMusic("GameplayMusic");
    }

    public override void Exit()
    {
        AudioLibrary.Instance.StopMusic();
    }

    public override void Update()
    {
    }
}
