using UnityEngine;

public class EventRaiserTest : MonoBehaviour
{
    [SerializeField] EventSystem_Void playerDiedEventChannel;

    private void Start()
    {
        playerDiedEventChannel.RaiseEvent();
    }
}
