using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FlipKnobBehaviour : MonoBehaviour
{
    [Header("Secondary Script")]
    //All variables' data are retreive from this script//
    public FlipCircularDrive flipcirculardrive;

    //Linear Value(Float) from "FlipCircularDrive"//
    private float linearvalue;

    //Linear Value(Int) will be used to convert from float to int//
    public int intlinearvalue;

    // Vessel Engine Control Details//
    //Right = Raise = 1//
    //Middle = Rest = 0.5//
    //Left = Decrease = 0//

    // Start is called before the first frame update
    void Start()
    {
        //Automatic notify the "Circular Drive" Script to enable limited.//
        flipcirculardrive.limited = true;

        //Set your Maximum & Minium Angle 
        flipcirculardrive.minAngle = -45.0f;
        flipcirculardrive.maxAngle = 45.0f;
    }
    public void IncreaseValue()
    {
        //Retreive value from Main Script "CircularDrive".//
        linearvalue = flipcirculardrive.linearMapping.value;

        //Round up from float value to int.//
        intlinearvalue = (Mathf.RoundToInt(linearvalue));

        //Actions whether the player got properly interact the knob.//
        Debug.Log(intlinearvalue);
        Debug.Log("Increase");
    }
    public void DecreaseValue()
    {
        //Retreive value from Main Script "CircularDrive".//
        linearvalue = flipcirculardrive.linearMapping.value;

        //Round up from float value to int.//
        intlinearvalue = (Mathf.RoundToInt(linearvalue));

        //Actions whether the player got properly interact the knob.//
        Debug.Log(intlinearvalue);
        Debug.Log("Decrease");
    }
}
