using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Gameplay")]
public class GameplayState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 1f;
        GameStateEvents.Instance.RaiseGameResumed();
        Debug.Log("entered Gameplay state");
        AudioManager.Instance.SetMusicIntensity(0.75f, 0.75f);
        AudioManager.Instance.PlayMusic("GameplayMusic");
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
