using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Value : MonoBehaviour
{
    //Represent the pivot point of the gameobject that is being rotated.
    public Transform needlePivotPoint;

    //Represent the minimum and maximum of the gameobject rotation.
    public float minValue;
    public float maxValue;

    //Represent the minimum and maximum value of the valuemeter.
    public float minValueMeter;
    public float maxValueMeter;

    //Represent the value of what the pivot point is at for further coding.
    public float value;

    // Update is called once per frame
    public void SetValueMeter(float value)
    {
        //Ensure that the value is within range.
        value = Mathf.Clamp(value, minValueMeter, maxValueMeter);

        //Calculate the rotation of the pivot based on the valuemeter's value.
        float normalizedValue = (value - minValueMeter) / (maxValueMeter - minValueMeter);
        float targetRotation = Mathf.Lerp(minValueMeter, maxValueMeter, normalizedValue);

        // Rotate the needle of the valuemeter to targeted rotation.
        needlePivotPoint.localRotation = Quaternion.Euler(0f, 0f, targetRotation);
    }
    void Update()
    {
        SetValueMeter(value);
    }
}
