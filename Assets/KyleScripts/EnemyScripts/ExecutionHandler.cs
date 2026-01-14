using UnityEngine;

public class ExecutionHandler : MonoBehaviour, IKillable
{
    float awarenessLevel = 0f;

    private void Awake()
    {
        EnemyEvents.Instance.OnEnemyDetectionChanged += UpdateAwarenessLevel;
    }

    private void OnDestroy()
    {
        EnemyEvents.Instance.OnEnemyDetectionChanged -= UpdateAwarenessLevel;
    }

    private void UpdateAwarenessLevel(float newAwareness)
    {
        awarenessLevel = newAwareness;
    }
    public void Kill()
    {
        // Kill logic here.
        EnemyEvents.Instance.EnemyDiedWithDetectionLevel(awarenessLevel);
        Debug.Log("Enemy executed. Awareness Level: " + awarenessLevel);
        EnemyEvents.Instance.EnemyKilled();
        Destroy(gameObject);
    }
}
