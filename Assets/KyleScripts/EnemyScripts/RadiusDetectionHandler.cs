using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// Detects if player is in field of view.
/// </summary>
public class RadiusDetectionHandler : MonoBehaviour
{
    [SerializeField] private float proximityDistance = 5f; // Distance for sphere cast to check if enemy can hear player.
    [SerializeField] private float lookDistance = 15f; // Distance for dot product, to check if player is in view.
    [SerializeField] private float viewThreshold = 0.6f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstructionLayer;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material spottedMat;
    [SerializeField] private MeshRenderer mr;

    private Collider[] results = new Collider[1];
    [SerializeField] private PatrolEnemy enemyPatrol;

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, lookDistance, results, targetLayer) > 0)
        {
            Transform target = results[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float distance = directionToTarget.magnitude;
            directionToTarget.y = 0;
            directionToTarget.Normalize();

            if (distance <= proximityDistance)
            {
                HandlePlayerHeard();
            }

            float dot = Vector3.Dot(transform.forward, directionToTarget);

            if (dot >= viewThreshold)
            {
                HandlePlayerSeen(true, target);
            }
            else
            {
                HandlePlayerSeen(false, target);
            }
        }
    }

    private void HandlePlayerHeard()
    {
        // TODO: Move enemy towards sound source slightly.
        Debug.Log("Hearing player.");

    }

    private void HandlePlayerSeen(bool isSeen, Transform target)
    {
        if (!isSeen)
        {
            EnemyEvents.Instance.EnemyLostPlayer();
            mr.material = normalMat;
            return;
        }
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        Debug.Log("Detecting player.");
        if (GameStateMachine.Instance.GetCurrentState() == GameState.Gameplay)
        {
            EnemyEvents.Instance.EnemyDetectionChanged(0.1f);
            EnemyEvents.Instance.EnemySpottedPlayer();
        }
        mr.material = spottedMat;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, proximityDistance);

        Gizmos.color = Color.yellow;
        float angle = Mathf.Acos(Mathf.Clamp(viewThreshold, -1f, 1f)) * Mathf.Rad2Deg;

        Vector3 leftRay = Quaternion.Euler(0, -angle, 0) * transform.forward;
        Vector3 rightRay = Quaternion.Euler(0, angle, 0) * transform.forward;

        Gizmos.DrawRay(transform.position, leftRay * lookDistance);
        Gizmos.DrawRay(transform.position, rightRay * lookDistance);
        Gizmos.DrawLine(transform.position + leftRay * lookDistance, transform.position + rightRay * lookDistance);
    }
}
