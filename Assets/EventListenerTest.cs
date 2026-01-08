using UnityEngine;

public class EventListenerTest : MonoBehaviour
{
    [SerializeField] EventSystem_Void eventChannel;
    [SerializeField] private string debugMessage = "Event received!";

    private void OnEnable()
    {
        eventChannel.OnEventRaised += OnEventRaised;
    }

    private void OnDisable()
    {
        eventChannel.OnEventRaised -= OnEventRaised;
    }

    private void OnEventRaised()
    {
        Debug.Log(debugMessage);
    }
}
