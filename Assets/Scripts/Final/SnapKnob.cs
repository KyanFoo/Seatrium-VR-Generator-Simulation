using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;
using static UnityEngine.GraphicsBuffer;

public class SnapKnob : MonoBehaviour
{
    public SnapCircularDrive snapcirculardrive;
    public float outAngleValue;

    //There can only be interval points from 1 to 10//
    //***Avoid using "7" as it does not give a whole number//
    [Range(1, 10)]
    public int intervalPoint;
    public float[] myArray;
    private float num;

    public Vector3 currentAngle;

    private int targetRotation;
    // Start is called before the first frame update
    void Start()
    {
        //Automatic notify the "Circular Drive" Script to enable limited.//
        snapcirculardrive.limited = true;

        //Set your Maximum & Minium Angle 
        snapcirculardrive.minAngle = -1.0f;
        snapcirculardrive.maxAngle = 361.0f;

        //Creating the interval points of the knobs//
        //If there are "4" interval points, the angles of the points are 90, 180, 270, 360//
        myArray = new float[intervalPoint];
        num = 360 / intervalPoint;
        for (int i = 0; i < intervalPoint; i++)
        {
            //Create an Array to check how many points there are and what is the rotation angle//
            myArray[i] = (num * (i+1)); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        outAngleValue = snapcirculardrive.outAngle;
        currentAngle = transform.eulerAngles;
        float zRotation = transform.rotation.eulerAngles.z;

        for (int i = 0; i < intervalPoint; i++)
        {
            float angle = myArray[i];
            int num = (int)myArray[i];
            float highAngle = (myArray[i] + 3f);
            float lowAngle = (myArray[i] - 3f);
            if (currentAngle.z > lowAngle && currentAngle.z < highAngle || outAngleValue == angle)
            {
                Debug.Log("Hello");
                SnapRotate();
            }
            if (outAngleValue >= myArray[i])
            {
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
