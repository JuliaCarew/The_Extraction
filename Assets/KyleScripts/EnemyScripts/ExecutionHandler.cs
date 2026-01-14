using UnityEngine;

public class ExecutionHandler : MonoBehaviour, IKillable
{
    float awarenessLevel = 0f;
    public GameObject toothPickup;

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
        Instantiate(toothPickup, transform.position + (transform.forward * 1.2f), Quaternion.identity);
        Destroy(gameObject);
    }
}
