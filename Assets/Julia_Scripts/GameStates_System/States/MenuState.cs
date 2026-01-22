using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Menu")]
public class MenuState : StateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;
        Debug.Log($"MenuState.Enter() called - Time.timeScale set to: {Time.timeScale}");
        if (GameStateEvents.Instance != null)
        {
            GameStateEvents.Instance.RaiseGameStarted();
        }
        AudioManager.Instance.PlayMusic("MainMenu");
        Debug.Log("Attempting to play main menu music.");
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
