using UnityEngine;

public class AwarenessTracker : MonoBehaviour
{
    float awarenessLevel = 0f;

    private void Awake()
    {
        EnemyEvents.Instance.OnEnemyDetectionChanged += IncreaseAwareness;
    }

    private void OnDestroy()
    {
        EnemyEvents.Instance.OnEnemyDetectionChanged -= IncreaseAwareness;
    }
    public void IncreaseAwareness(float awareness)
    {
        awarenessLevel += awareness;
        EnemyEvents.Instance.EnemyDetectionChanged(awarenessLevel);
        Debug.Log($"Awareness Level: {awarenessLevel}");
    }

    public float GetAwarenessLevel()
    {
        return awarenessLevel;
    }
}
