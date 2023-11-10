using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public Transform topOfJoystick;

    [SerializeField]
    private float forwardBackwardTilt = 0;
    [SerializeField]
    private float sideToSideTilt = 0;

    // Update is called once per frame
    void Update()
    {
        forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
        if (forwardBackwardTilt < 355 && forwardBackwardTilt > 290)
        {
            forwardBackwardTilt = Mathf.Abs(forwardBackwardTilt - 360);
            Debug.Log("Backward" + forwardBackwardTilt);
        }
        else if (forwardBackwardTilt > 5 && forwardBackwardTilt < 74)
        {
            Debug.Log("Forward" + forwardBackwardTilt);
        }

        sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
        if (sideToSideTilt < 355 && sideToSideTilt > 290)
        {
            sideToSideTilt = Mathf.Abs(sideToSideTilt - 360);
            Debug.Log("Right" + sideToSideTilt);
        }
        else if (sideToSideTilt > 5 && sideToSideTilt < 74)
        {
            Debug.Log("Left" + sideToSideTilt);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            // Get the Transform component of the object with the collider.
            Transform otherTransform = other.transform;

            // Look at the position of the otherTransform using topOfJoystick's up vector.
            transform.LookAt(otherTransform.position, topOfJoystick.up);
        }
    }
}