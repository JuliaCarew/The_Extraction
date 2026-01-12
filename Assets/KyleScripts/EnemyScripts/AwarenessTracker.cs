using UnityEngine;

public class AwarenessTracker : MonoBehaviour
{
    float awarenessLevel = 0f;

    private void Awake()
    {
        EnemyEvents.Instance.SpottedPlayer += IncreaseAwareness;
    }

    private void OnDestroy()
    {
        EnemyEvents.Instance.SpottedPlayer -= IncreaseAwareness;
    }
    public void IncreaseAwareness()
    {
        awarenessLevel += 1f;
        EnemyEvents.Instance.EnemyDetectionChanged(awarenessLevel);
        Debug.Log($"Awareness Level: {awarenessLevel}");
    }

    public float GetAwarenessLevel()
    {
        return awarenessLevel;
    }
}
