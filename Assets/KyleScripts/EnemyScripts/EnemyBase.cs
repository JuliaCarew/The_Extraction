using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected LayerMask obstructionLayer;
    [SerializeField] protected Vector3 movementVector;

    protected IRotationStrategy rotationStrategy;

    protected virtual void Update()
    {
        rotationStrategy?.Rotate(transform, movementVector, rotationSpeed);
    }
}
