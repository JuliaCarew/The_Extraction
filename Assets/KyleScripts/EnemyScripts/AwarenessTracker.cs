using UnityEngine;

public class AwarenessTracker : MonoBehaviour
{
    float awarenessLevel = 0f;

    private void Awake()
    {
        EnemyEvents.SpottedPlayer += IncreaseAwareness;
    }

    private void OnDestroy()
    {
        EnemyEvents.SpottedPlayer -= IncreaseAwareness;
    }
    public void IncreaseAwareness()
    {
        awarenessLevel += 1f;
        EnemyEvents.EnemyDetectionChanged(awarenessLevel);
        Debug.Log($"Awareness Level: {awarenessLevel}");
    }

    public float GetAwarenessLevel()
    {
        return awarenessLevel;
    }
}
