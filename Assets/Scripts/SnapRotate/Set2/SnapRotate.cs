using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SnapRotate : MonoBehaviour
{
    public TestingCircular testingCircular;
    public float currentAngle;
    public float negCurrentAngle;

    private float linearvalue;
    public float floatLinearvalue;
    // Start is called before the first frame update
    void Start()
    {
        testingCircular.minAngle = -90;
        testingCircular.maxAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        negCurrentAngle = transform.rotation.eulerAngles.z;
        currentAngle = 360 - negCurrentAngle;
        negCurrentAngle = currentAngle * -1;

        linearvalue = testingCircular.linearMapping.value;
        floatLinearvalue = Mathf.Round(linearvalue * 10.0f) / 10.0f;

        if (negCurrentAngle == testingCircular.minAngle && floatLinearvalue == 0)
        {
            Invoke("MaxChange", 1.0f); 
        }
    }
    void MaxChange()
    {
        testingCircular.minAngle = -180;
    }
}
