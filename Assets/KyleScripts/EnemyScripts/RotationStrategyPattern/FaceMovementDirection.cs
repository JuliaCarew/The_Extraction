using UnityEngine;

public class FaceMovementDirection : IRotationStrategy
{
    public void Rotate(Transform transform, Vector3 movementVector, float rotationSpeed)
    {
        if (movementVector == Vector3.zero) return;

        movementVector.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(movementVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
