using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EventSystem_Void", menuName = "Scriptable Objects/EventSystem_Void")]
public class EventSystem_Void : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }

}
