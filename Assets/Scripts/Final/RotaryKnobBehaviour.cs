using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;
using static UnityEngine.GraphicsBuffer;

public class RotaryKnobBehaviour : MonoBehaviour
{
    [Header("Secondary Script")]
    //All variables' data are retreive from this script//
    public RotaryCircularDrive rotarycirculardrive;

    public float outAngleValue;

    //There can only be interval points from 1 to 10//
    //***Avoid using "7" as it does not give a whole number//
    [Range(1, 10)]
    public int intervalPoint;
    //An Array that would be filled with interval points//
    public int[] myArray;
    public int num;

    //public Vector3 currentAngle;
    private int targetRotation;

    public int numberOfVariables;
    public int numberPlace = 0;

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
        myArray = new int[intervalPoint+1];
        num = 360 / intervalPoint;
        myArray[0] = 0;
        for (int i = 0; i < intervalPoint; i++)
        {
            //Create an Array to check how many points there are and what is the rotation angle//
            myArray[i+1] = (num * (i + 1));
        }

        numberOfVariables = myArray.Length;
        numberPlace = 0;
    }
    // Update is called once per frame
    void Update()
    {
        outAngleValue = rotarycirculardrive.outAngle;
        //currentAngle = transform.eulerAngles;
        float zRotation = transform.rotation.eulerAngles.z;
        for (int i = 0; i < intervalPoint; i++)
        {
            if (outAngleValue >= myArray[i])
            {
                int num = myArray[i];
                targetRotation = num;
                SnapRotate();
            }
        }
    }
    void SnapRotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    }
}
