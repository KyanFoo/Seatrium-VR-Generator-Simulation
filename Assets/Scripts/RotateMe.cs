using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public Vector3 targetAngle = new Vector3(0f, 345f, 0f);

    private Vector3 currentAngle;

    public bool atRest = false;

    public void Start()
    {
        currentAngle = transform.eulerAngles;
    }

    public void Update()
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
}
