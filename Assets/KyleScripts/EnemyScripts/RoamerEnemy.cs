using System.Collections;
using UnityEngine;

public class RoamerEnemy : EnemyBase
{
    [Header("Distance Settings")]
    [SerializeField] private float distanceTraveledSoFar;
    [SerializeField] private float distanceToTravel;

    private void Awake()
    {
        rotationStrategy = new FaceMovementDirection();
    }

    private void Start()
    {
        movementVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        StartCoroutine(Patrol());
    }

    protected override void Update()
    {
        DetectWalls();
        base.Update();
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            movementVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            distanceTraveledSoFar = 0f;

            while (distanceTraveledSoFar < distanceToTravel)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position += movementVector * step;
                distanceTraveledSoFar += step;

                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void DetectWalls()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, movementVector, out hit, 1f, obstructionLayer))
        {
            movementVector = Vector3.Reflect(movementVector, hit.normal);
            movementVector.y = 0;
            movementVector.Normalize();
        }
    }
}
