using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo90Degrees : MonoBehaviour
{
    public float rotationSpeed = 30.0f; // Adjust the rotation speed as needed
    private Quaternion targetRotation;

    void Start()
    {
        // Calculate the target rotation (90 degrees around the Y-axis)
        targetRotation = Quaternion.Euler(0, 90, 0);
    }

    void Update()
    {
        // Interpolate the current rotation towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the difference between the current rotation and the target rotation is small enough
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
        {
            // Snap to the exact target rotation to ensure it reaches exactly 90 degrees
            transform.rotation = targetRotation;
        }
    }
}
