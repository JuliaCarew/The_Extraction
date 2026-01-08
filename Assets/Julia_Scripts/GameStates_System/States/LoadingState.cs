using UnityEngine;

[CreateAssetMenu(menuName = "Game States/Loading")]
public class LoadingState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;
        Debug.Log("Entered Loading State");
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}