using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class KnobBehaviour : MonoBehaviour
{
    public CircularDrive circulardrive;
    public KnobData knobdata;

    private float linearvalue;
    public int intlinearvalue;
    public bool atRest = false;

    public Vector3 targetAngle;
    private Vector3 currentAngle;


    // Vessel Engine Control Details//
    //Right = Raise = 1//
    //Middle = Rest = 0.5//
    //Left = Decrease = 0//
    void Start()
    {
        //Automatic notify the "Circular Drive" Script to enable limited.//
        circulardrive.limited = true;
        currentAngle = transform.eulerAngles;
    }
    void Update()
    {
        if (atRest == true)
        {
            currentAngle = new Vector3(
                        Mathf.LerpAngle(currentAngle.x, targetAngle.x, Time.deltaTime),
                        Mathf.LerpAngle(currentAngle.y, targetAngle.y, Time.deltaTime),
                        Mathf.LerpAngle(currentAngle.z, targetAngle.z, Time.deltaTime));
            transform.eulerAngles = currentAngle;
        }
    }
    public void IncreaseValue()
    {
        //Retreive value from Main Script "CircularDrive".//
        linearvalue = circulardrive.linearMapping.value;
        //Round up from float value to int.//
        intlinearvalue = (Mathf.RoundToInt(linearvalue));
        //Actions whether the player got properly interact the knob.//
        Debug.Log(intlinearvalue);
        Debug.Log("Increase");
    }
    public void DecreaseValue()
    {
        //Retreive value from Main Script "CircularDrive".//
        linearvalue = circulardrive.linearMapping.value;
        //Round up from float value to int.//
        intlinearvalue = (Mathf.RoundToInt(linearvalue));
        //Actions whether the player got properly interact the knob.//
        Debug.Log(intlinearvalue);
        Debug.Log("Decrease");
    }
}
