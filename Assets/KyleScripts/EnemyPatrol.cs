using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    private int targetPoint = 0;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 5f;
    private Vector3 movementVector;

    private void Start()
    {
        targetPoint = 0;
    }

    private void Update()
    {
        movementVector = new Vector3(patrolPoints[targetPoint].position.x, transform.position.y, patrolPoints[targetPoint].position.z);
        if (transform.position == movementVector)
        {
            IncreaseTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, movementVector, speed * Time.deltaTime);


        RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = patrolPoints[targetPoint].position - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void IncreaseTarget()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Count) targetPoint = 0;
    }
}
