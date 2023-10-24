using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rotary : MonoBehaviour
{
    [Header("Secondary Script")]
    //All variables' data are retreive from this script//
    public RotaryCircularDrive rotarycirculardrive;
    public float outAngleValue;

    [Header("Primary Script")]
    //Gameobject's Z Roation//
    public float currentZRotation;
    public float previousZRotation;

    //There can only be interval points from 1 to 10//
    //***Avoid using "7" as it does not give a whole number//
    [Range(1, 10)]
    public int intervalPoint;
    //An Array that would be filled with interval points//
    public int[] myArray;
    public int num;

    //public Vector3 currentAngle;
    private int targetRotation;

    private float allowedDeviation = 3.0f;

    public int numberOfVariables;
    public int numberPlace = 0;

    public bool isIncrease;
    public bool isDecrease;
    public bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        //Automatic notify the "Circular Drive" Script to enable limited.//
        rotarycirculardrive.limited = true;

        //Set your Maximum & Minium Angle 
        rotarycirculardrive.minAngle = -1.0f;
        rotarycirculardrive.maxAngle = 361.0f;

        //Creating the interval points of the knobs//
        //If there are "4" interval points, the angles of the points are 90, 180, 270, 360//
        myArray = new int[intervalPoint + 1];
        num = 360 / intervalPoint;
        myArray[0] = 0;
        for (int i = 0; i < intervalPoint; i++)
        {
            //Create an Array to check how many points there are and what is the rotation angle//
            myArray[i + 1] = (num * (i + 1));
        }
        numberOfVariables = myArray.Length;
        numberPlace = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Retreive data of "outAngle" from "RotatyCircularDrive"//
        outAngleValue = rotarycirculardrive.outAngle;

        //Retreive data of the gameobject Z Rotation//
        currentZRotation = transform.rotation.eulerAngles.z;

        //When the user holds the knob//
        if (rotarycirculardrive.handHold == true && rotarycirculardrive.handRelease == false)
        {
            if (currentZRotation > previousZRotation)
            {
                Debug.Log("Z rotation is increasing.");
                isIncrease = true;
            }
            // Check if the Z rotation is decreasing.
            else if (currentZRotation < previousZRotation)
            {
                Debug.Log("Z rotation is decreasing.");
                isDecrease = true;
            }
        }
        //When the user releae the knob//
        if (rotarycirculardrive.handHold == false && rotarycirculardrive.handRelease == true)
        {
            //Ensure that the outAngle is similar to the gameobject Z Rotation//
            rotarycirculardrive.outAngle = currentZRotation;
            previousZRotation = transform.eulerAngles.z;

            if (isIncrease == true)
            {
                if (oneTime == false)
                {
                    numberPlace = numberPlace + 1;
                }
                oneTime = true;
            }
            isIncrease = false;

            if (isDecrease == false)
            {
                if (oneTime == false)
                {
                    numberPlace = numberPlace - 1;
                }
                oneTime = true;
            }
            isDecrease= false;
        }
    }
    void SnapRotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    }
}
