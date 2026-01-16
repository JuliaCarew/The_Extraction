using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : EnemyBase
{
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    private int targetPoint = 0;

    private void Awake()
    {
        rotationStrategy = new FaceTargetPosition();
    }

    private void Start()
    {
        targetPoint = 0;
    }

    protected override void Update()
    {
        movementVector = new Vector3(patrolPoints[targetPoint].position.x, transform.position.y, patrolPoints[targetPoint].position.z);

        if (transform.position == movementVector)
        {
            IncreaseTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, movementVector, moveSpeed * Time.deltaTime);
        base.Update();
    }

    private void IncreaseTarget()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Count)
        {
            targetPoint = 0;
        }
    }
}
