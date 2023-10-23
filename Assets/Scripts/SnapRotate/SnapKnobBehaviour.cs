using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SnapKnobBehaviour : MonoBehaviour
{
    public CircularDriveSnap circulardrivesnapScript;

    private float linearvalue;
    public float floatlinearvalue;
    public float outAngle;
    private int targetRotation;
    public bool snapToPoint;

    public float zRotation;

    // Start is called before the first frame update
    void Start()
    {
        //snapToPoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        //float zRotation = transform.rotation.eulerAngles.z;
        //linearvalue = circulardrivesnapScript.linearMapping.value;
        //floatlinearvalue = Mathf.Round(linearvalue * 10) / 10.0f;
        outAngle = circulardrivesnapScript.outAngle;
        if (outAngle >= 60)
        {
            targetRotation= 60;
            //Debug.Log("FirstPoint");
            SnapRotate();
        }
        if (outAngle >= 120)
        {
            targetRotation = 120;
            //Debug.Log("FirstPoint");
            SnapRotate();
        }
        if (outAngle >= 180)
        {
            targetRotation = 180;
            //Debug.Log("FirstPoint");
            SnapRotate();
        }
    }
    void SnapRotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, targetRotation);
    }
}
