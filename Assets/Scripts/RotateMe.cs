using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    public Vector3 targetAngle;
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
           Mathf.Lerp(currentAngle.x, targetAngle.x, Time.deltaTime * 4),
           Mathf.Lerp(currentAngle.y, targetAngle.y, Time.deltaTime * 4),
           Mathf.Lerp(currentAngle.z, targetAngle.z, Time.deltaTime * 4));

            transform.eulerAngles = currentAngle;
        }
    }
}
