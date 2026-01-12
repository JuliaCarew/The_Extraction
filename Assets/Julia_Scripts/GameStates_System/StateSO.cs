using UnityEngine;

[CreateAssetMenu(fileName = "StateSO", menuName = "Scriptable Objects/GameStateSO")]
public class StateSO : GameStateSO
{
    public override void Enter()
    {
        Debug.Log("Enter state");
    }

    public override void Exit()
    {
        Debug.Log("Exit state");
    }

    public override void Update()
    {
        //Debug.Log("Update state");
    }
}
