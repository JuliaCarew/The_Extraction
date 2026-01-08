using UnityEngine;

public class StartGameButton : MonoBehaviour, IUIButtonAction
{
    public void OnButtonPressed()
    {
        UIManager.Instance.HideAllScreens();
        UIManager.Instance.ShowScreen("GameplayScreen");
        GameStateMachine.Instance.ChangeState(GameState.Gameplay);
    }

    public void OnButtonHovered()
    {
    }
}
