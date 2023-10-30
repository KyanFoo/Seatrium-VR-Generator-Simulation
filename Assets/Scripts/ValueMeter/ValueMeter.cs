using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueMeter : MonoBehaviour
{
    //Represent the value of what the pivot point is at for further coding.
    public float inputValue;

    //Represent the pivot point of the gameobject that is being rotated.
    public Transform pivotPoint;

    //Represent the minimum and maximum of the gameobject rotation.
    [Header("ValueMeter Settings")]
    public float minValueMeter = -68f;
    public float maxValueMeter = 68f;

    //Represent the minimum and maximum value of the valuemeter.
    [Header("Game Object Settings")]
    public float minValue;
    public float maxValue;

    // Update the pointer rotation based on the input value
    public void SetPointerRotation(float value)
    {
        //Ensure that the value is within range.
        value = Mathf.Clamp(value, minValueMeter, maxValueMeter);

        //Calculate the rotation of the pivot based on the valuemeter's value.
        float normalizedValue = (value - minValueMeter) / (maxValueMeter - minValueMeter);
        float targetRotation = Mathf.Lerp(minValue, maxValue, normalizedValue);

        // Rotate the needle of the valuemeter to targeted rotation.
        pivotPoint.localRotation = Quaternion.Euler(0f, 0f, targetRotation);
    }

    // Example usage in Update or wherever you need to update the pointer:
    void Update()
    {
        SetPointerRotation(inputValue);
    }
}
