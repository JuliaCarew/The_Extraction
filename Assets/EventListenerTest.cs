using UnityEngine;

public class EventListenerTest : MonoBehaviour
{
    [SerializeField] EventSystem_Void playerDiedEventChannel;

    private void Awake()
    {
        playerDiedEventChannel.OnEventRaised += PlayerDiedResponse;
    }

    private void OnDestroy()
    {
        playerDiedEventChannel.OnEventRaised -= PlayerDiedResponse;
    }

    private void PlayerDiedResponse()
    {
        Debug.Log("Player has died! Event received in EventListenerTest.");
    }
}
