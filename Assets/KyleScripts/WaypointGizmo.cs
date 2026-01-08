using UnityEngine;

/// <summary>
/// Waypoint Gizmo class for visualizing waypoint in scene.
/// </summary>
public class WaypointGizmo : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
