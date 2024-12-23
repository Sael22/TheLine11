using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed in degrees per second
    public float rotationSpeed = 100f;

    void Update()
    {
        // Rotate the object around its own Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

}
