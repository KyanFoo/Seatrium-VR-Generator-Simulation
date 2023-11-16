using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueMeter : MonoBehaviour
{
    [Header("Input Value Settings")]
    //Represent the value of the inputValue.//
    public float inputValue;
    //Represent the value that will increase the inputValue.//
    public float flipValue;

    [Header("Lerp Speed Settings")]
    //Represent the speed in which the Synchroscope Needle would move.//
    public float lerpSpeed = 5.0f;

    [Header("Pivot Transform GameObject")]
    //Represent the pivot point of the GameObject that is being rotated.//
    public Transform pivotPoint;

    //Represent the Maximum and Minimum of the GameObject rotation.//
    //Write in Maximum & Minimum value of the GameObject rotation.//
    //Manually rotate the "Needle_Pivot" to the Maximum and Minimum point of the meters' image.//
    [Header("Value Meter Settings")]
    public float minValueMeter;
    public float maxValueMeter;

    //Represent the minimum and maximum values of the "ValueMeter".//
    //Write in Maximum & Minimum value of the "ValueMeter"'s image canvas.//
    //Current(A) = 0 - 1000, Power(Kw) = 300 - 600.//
    //Frequency(Hz) = 55 - 65, Voltage(V) 0 - 600.//
    [Header("Game Object Settings")]
    public float minValue;
    public float maxValue;

    //Represent the variable of the Synchroscope Needle's current Rotation.//
    private float currentRotation;

    [Header("Secondary Script")]
    public GameManager GameManager;
    public bool nowSetting1Toggle;
    public bool nowSetting2Toggle;

    //Represent the Bools to check ValueMeter to see if they are Setting 1 or 2.//
    //Setting 1 is Frequency.//
    //Setting 2 is Power & Current.//
    public bool areYouSetting1;
    public bool areYouSetting2;

    [Header("Generator Toggle Settings")]
    //Represent the Bools to check whether Generator 1 and 2 is switch "ON".//
    public bool isGenerator1On;
    public bool isGenerator2On;

    //Represent the Bools to check ValueMeter is on Generator 1 or 2.//
    public bool areYouGenerator1;
    public bool areYouGenerator2;

    public SynchroscopeManager SynchroscopeManager;
    public bool areYouFrequency2;

    // Update the pointer rotation based on the input value
    public void SetPointerRotation(float value)
    {
        //Ensure that the value is within range.//
        value = Mathf.Clamp(value, minValueMeter, maxValueMeter);

        //Calculate the rotation of the pivot based on the "ValueMeter"'s value.//
        float normalizedValue = (value - minValueMeter) / (maxValueMeter - minValueMeter);
        float targetRotation = Mathf.Lerp(minValue, maxValue, normalizedValue);

        // Smoothly interpolate rotation when inputValue changes.//
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * lerpSpeed);

        // Rotate the needle of the valuemeter to targeted rotation.//
        pivotPoint.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    // Example usage in Update or wherever you need to update the pointer:
    void Update()
    {
        //Constantly update to check whether Generator 1 or 2 has been switched "ON" respectively.//
        isGenerator1On = GameManager.Generator1Toggle;
        isGenerator2On = GameManager.Generator2Toggle;

        //If Else Statement to check if the Generator the ValueMeter is build in, is the same as Generator itself has been switched "ON".//
        //e.g. ValueMeter on Generator 1 is checking whether Generator 1 is switched "ON".//
        if (isGenerator1On == true && areYouGenerator1 == true || isGenerator2On == true && areYouGenerator2 == true)
        {
            //Constantly update to check whether Generators is on Setting 1 or 2.//
            nowSetting1Toggle = GameManager.setting1;
            nowSetting2Toggle = GameManager.setting2;

            SetPointerRotation(inputValue);
        }
    }
    public void FlipIncrease()
    {
        //Function called to, first check whether Generator and the ValueMeter is on the same setting.//
        //Frequency ValueMeter is in Setting 1 & Power and Current ValueMeter is in Setting 2.//
        //They must check if the Generators is on the same setting so that they can increase its respective ValueMeter's output.//
        if (areYouSetting1 == true && nowSetting1Toggle == true || areYouSetting2 == true && nowSetting2Toggle == true)
        {
            //Check whether the ValueMeter is a Frequency ValueMeter in Generator 2.//
            if (areYouFrequency2 == true)
            {
                //If Yes, allow to call a function in Synchroscope Manager.//
                SynchroscopeManager.FlipIncrease();
            }
            inputValue = inputValue + flipValue;
        }
    }
    public void FlipDecrease()
    {
        //Function called to, first check whether Generator and the ValueMeter is on the same setting.//
        //Frequency ValueMeter is in Setting 1 & Power and Current ValueMeter is in Setting 2.//
        //They must check if the Generators is on the same setting so that they can decrease its respective ValueMeter's output.//
        if (areYouSetting1 == true && nowSetting1Toggle == true || areYouSetting2 == true && nowSetting2Toggle == true)
        {
            //Check whether the ValueMeter is a Frequency ValueMeter in Generator 2.//
            if (areYouFrequency2 == true)
            {
                //If Yes, allow to call a function in Synchroscope Manager.//
                SynchroscopeManager.FlipDecrease();
            }
            inputValue = inputValue - flipValue;
        }
    }
}
