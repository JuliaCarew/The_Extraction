using UnityEngine;

/// <summary>
/// Detects if player is in field of view.
/// </summary>
public class RadiusDetectionHandler : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float viewThreshold = 0.7f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask obstructionLayer;
    [SerializeField] private Material normalMat;
    [SerializeField] private Material spottedMat;
    [SerializeField] private MeshRenderer mr;

    private Collider[] results = new Collider[1];

    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        if (Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, results, targetLayer) > 0)
        {
            Transform target = results[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Dot(transform.forward, directionToTarget) >= viewThreshold)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                Debug.Log("Detecting player.");
                mr.material = spottedMat;
            }
            else
            {
                mr.material = normalMat;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        float radAngle = Mathf.Acos(viewThreshold);
        float degAngle = radAngle * Mathf.Rad2Deg;

        Vector3 leftRayDirection = Quaternion.AngleAxis(-degAngle, Vector3.up) * transform.forward;
        Vector3 rightRayDirection = Quaternion.AngleAxis(degAngle, Vector3.up) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftRayDirection * detectionRadius);
        Gizmos.DrawRay(transform.position, rightRayDirection * detectionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * detectionRadius);
    }
}
