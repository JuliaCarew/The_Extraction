using UnityEngine;

public class FaceTargetPosition : IRotationStrategy
{
    public void Rotate(Transform transform, Vector3 targetPosition, float rotationSpeed)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;

        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
