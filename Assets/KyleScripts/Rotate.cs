using UnityEngine;

public class Rotate : MonoBehaviour
{
    float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
