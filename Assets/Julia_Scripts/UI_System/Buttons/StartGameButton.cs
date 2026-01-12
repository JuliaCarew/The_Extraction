using UnityEngine;

public class StartGameButton : MonoBehaviour, IUIButtonAction
{
    public void OnButtonPressed()
    {
        // Check if UIManager exists in the scene
        // if (UIManager.Instance == null)
        // {
        //     Debug.LogError("StartGameButton: UIManager.Instance is null");
        //     return;
        // }

        // Check if GameStateMachine exists in the scene
        if (GameStateMachine.Instance == null)
        {
            Debug.LogError("StartGameButton: GameStateMachine.Instance is null");
            return;
        }

        //UIManager.Instance.HideAllScreens();
        //UIManager.Instance.ShowScreen("GameplayHUD");
        GameStateMachine.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnButtonHovered()
    {
    }
}
