using UnityEngine;

public interface IRotationStrategy
{
    public void Rotate(Transform transform, Vector3 targetPosition, float rotationSpeed);
}
