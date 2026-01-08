using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    private int targetPoint = 0;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        targetPoint = 0;
    }

    private void Update()
    {
        if (transform.position == patrolPoints[targetPoint].position)
        {
            IncreaseTarget();
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime);
        transform.LookAt(patrolPoints[targetPoint].position);
    }

    private void IncreaseTarget()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Count) targetPoint = 0;
    }
}
