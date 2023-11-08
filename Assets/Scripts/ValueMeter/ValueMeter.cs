using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueMeter : MonoBehaviour
{
    [Header("Input Value Settings")]
    //Represent the value of what the pivot point is at for further coding.
    public float inputValue;
    public float flipValue;

    [Header("Lerp Speed Settings")]
    public float lerpSpeed = 5.0f;

    [Header("Pivot Transform GameObject")]
    //Represent the pivot point of the GameObject that is being rotated.
    public Transform pivotPoint;

    //Represent the Maximum and Minimum of the GameObject rotation.
    //Write in Maximum & Minimum value of the GameObject rotation.
    //Manually rotate the "Needle_Pivot" to the Maximum and Minimum point of the meters' image.
    [Header("Value Meter Settings")]
    public float minValueMeter;
    public float maxValueMeter;

    //Represent the minimum and maximum values of the "ValueMeter".
    //Write in Maximum & Minimum value of the "ValueMeter"'s image canvas.
    //Current(A) = 0 - 1000, Power(Kw) = 300 - 600.
    //Frequency(Hz) = 55 - 65, Voltage(V) 0 - 600.
    [Header("Game Object Settings")]
    public float minValue;
    public float maxValue;

    private float currentRotation;

    public GameManager GameManager;
    public bool nowSetting1Toggle;
    public bool nowSetting2Toggle;

    public bool areYouSetting1;
    public bool areYouSetting2;

    // Update the pointer rotation based on the input value
    public void SetPointerRotation(float value)
    {
        //Ensure that the value is within range.
        value = Mathf.Clamp(value, minValueMeter, maxValueMeter);

        //Calculate the rotation of the pivot based on the "ValueMeter"'s value.
        float normalizedValue = (value - minValueMeter) / (maxValueMeter - minValueMeter);
        float targetRotation = Mathf.Lerp(minValue, maxValue, normalizedValue);

        // Smoothly interpolate rotation when inputValue changes.
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * lerpSpeed);

        // Rotate the needle of the valuemeter to targeted rotation.
        pivotPoint.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    // Example usage in Update or wherever you need to update the pointer:
    void Update()
    {
        nowSetting1Toggle = GameManager.setting1;
        nowSetting2Toggle = GameManager.setting2;
        
        SetPointerRotation(inputValue);
    }
    public void FlipIncrease()
    {
        if (areYouSetting1 == true && nowSetting1Toggle == true || areYouSetting2 == true && nowSetting2Toggle == true)
        {
            Debug.Log("Hello1");
            inputValue = inputValue + flipValue;
        }
    }
    public void FlipDecrease()
    {
        if (areYouSetting1 == true && nowSetting1Toggle == true || areYouSetting2 == true && nowSetting2Toggle == true)
        {
            Debug.Log("Hello2");
            inputValue = inputValue - flipValue;
        }
    }
}
