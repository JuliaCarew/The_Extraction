using UnityEngine;

public class TutorialState : GameStateSO
{
    public override void Enter()
    {
        Time.timeScale = 0f;

        if(GameStateEvents.Instance != null) 
        {
            GameStateEvents.Instance.RaiseTutorial(); 
        }
    }

    public override void Update()
    {
        
    }

    public override void Exit() 
    {

    }
}
