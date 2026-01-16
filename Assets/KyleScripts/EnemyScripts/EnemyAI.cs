using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceTraveledSoFar;
    [SerializeField] private float distanceToTravel;
    private Vector3 direction;
    float rotationSpeed = 2f;
    [SerializeField] private LayerMask obstructionLayer;

    private void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        DetectWalls();
        RotateTowardsDirection();
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            distanceTraveledSoFar = 0f;

            while (distanceTraveledSoFar < distanceToTravel)
            {
                float step = speed * Time.deltaTime;
                transform.position += direction * step;
                distanceTraveledSoFar += step;

                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void RotateTowardsDirection()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
    private void DetectWalls()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 1f, obstructionLayer))
        {
            direction = Vector3.Reflect(direction, hit.normal);
        }
    }
}
