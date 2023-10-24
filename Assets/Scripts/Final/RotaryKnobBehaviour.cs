using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class RotaryKnobBehaviour : MonoBehaviour
{
    [Header("Secondary Script")]
    //All variables' data are retreive from this script//
    public RotaryCircularDrive rotarycirculardrive;
    private float outAngleValue;

    [Header("Primary Script")]
    //Gameobject's Z Roation//
    private float currentZRotation;
    private float previousZRotation;

    //There can only be interval points from 1 to 10//
    //***Avoid using "7" as it does not give a whole number//
    [Range(1, 10)]
    public int intervalPoint;
    //An Array that would be filled with interval points//
    public int[] myArray;
    private int num;

    //public Vector3 currentAngle;
    public int targetRotation;

    //Allow the knobs to be how close to the next interval to snap to the cloest position//
    private float allowedDeviation = 0;

    //Variables to check how many intervals are there//
    public int numberOfVariables;
    //Variables to check which interval the knob is at//
    public int numberPlace = 0;
    private float rotationDifference;

    //Variables to check whether the knob is increasing or decreasig//
    private bool isIncrease;
    private bool isDecrease;

    //Bool to ensure code was used once only in the Update Function//
    private bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        //Automatic notify the "Circular Drive" Script to enable limited.//
        rotarycirculardrive.limited = true;

        //Creating the interval points of the knobs//
        //If there are "4" interval points, the angles of the points are 90, 180, 270, 360//
        myArray = new int[intervalPoint + 1];
        num = 360 / intervalPoint;
        //Within the array, the first value would be "0"//
        myArray[0] = 0;
        for (int i = 0; i < intervalPoint; i++)
        {
            //Create an Array to check how many points there are and what is the rotation angle//
            myArray[i + 1] = (num * (i + 1));
        }

        //Checking on how many variables are there in the gameobject//
        numberOfVariables = myArray.Length;
        numberPlace = 0;

        //Set your Maximum & Minium Angle//
        rotarycirculardrive.minAngle = -3.0f;

        //Maximum Angles can varied based on the gameobject's interval points//
        rotarycirculardrive.maxAngle = myArray[numberOfVariables - 2] + 3;

        //Rotation value before reaching the interval points//
        //For example, if the interval points are (0, 90, 180, 270)//
        //The allowedDeviation would be "45", allow no space for the gameobject to hang between interval. Either snapping to "0" or "90"//
        allowedDeviation = myArray[1] / 2;
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
            //Check to see if the gameobject's rotation increasing//
            if (currentZRotation > previousZRotation)
            {
                //Debug.Log("Z rotation is increasing.");
                isIncrease = true;
            }

            //Check to see if the gameobject's rotation decreasing//
            else if (currentZRotation < previousZRotation)
            {
                //Debug.Log("Z rotation is decreasing.");
                isDecrease = true;
            }
        }
        //When the user releae the knob//
        if (rotarycirculardrive.handHold == false && rotarycirculardrive.handRelease == true)
        {
            //Ensure that the outAngle is similar to the gameobject Z Rotation//
            rotarycirculardrive.outAngle = currentZRotation;
            previousZRotation = transform.eulerAngles.z;

            if (isIncrease == true && isDecrease == false)
            {
                //Code is used one time when "oneTime" == false)
                if (oneTime == false)
                {
                    //Increase the gameobject's interval numberplace//
                    numberPlace = numberPlace + 1;
                }
                oneTime = true;
            }
            isIncrease = false;

            if (isDecrease == true && isIncrease == false)
            {
                //Code is used one time when "oneTime" == false)
                if (oneTime == false)
                {
                    //Decrease the gameobject's interval numberplace//
                    numberPlace = numberPlace - 1;
                }
                oneTime = true;
            }
            isDecrease= false;
        }
        //Allow the OneTime Code to be used again//
        oneTime = false;

        //Ensure that gameobject does not rotate too much//
        //For example,//
        //If the gameobject has intervals of (0, 90, 180, 270, 360)//
        //This script prevent the gameobject to rotate to 360 interval as knobs does not turn 360 to off but rotate it back to 0 anti-clockwise//
        numberPlace = Mathf.Clamp(numberPlace, 0, numberOfVariables - 2);

        //Checking the rotation difference, checking to see if the GameObject is getting closer to the interval//
        rotationDifference = Mathf.Abs(currentZRotation - myArray[numberPlace]);
        if (rotationDifference <= allowedDeviation && rotationDifference > 0)
        {
            //Fill in "targetRotation" with the closest interval//
            targetRotation = myArray[numberPlace];
            SnapRotate();
        }
    }
    void SnapRotate()
    {
        //Snap the GameObject to the rotation position of the "targetRotaion"//
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    }
}