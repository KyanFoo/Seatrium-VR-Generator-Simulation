using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroNeedle : MonoBehaviour
{
    public Transform objectToRotate;
    private Vector3 startRotation;
    private Vector3 endRotation;
    private float lerpTime;
    public float lerpDuration = 3.7f;

    public SynchroscopeManager synchromanager;
    public bool pauseSwitch;
    public bool startSwitch;
    private void Start()
    {
        startRotation = objectToRotate.rotation.eulerAngles;
        endRotation = startRotation + new Vector3(0, 0, 360.0f);
    }

    private void Update()
    {
        pauseSwitch = synchromanager.isolatorSwitch;
        startSwitch = synchromanager.synActive;

        if (startSwitch == true)
        {
            StartCoroutine(LampCoroutine());
        }
    }
    IEnumerator LampCoroutine()
    {
        if (pauseSwitch== false)
        {
            lerpTime += Time.deltaTime;
        }
        objectToRotate.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, lerpTime / lerpDuration));
        if (lerpTime >= lerpDuration)
        {
            lerpTime = 0.0f;
        }
        yield return null;
    }
}
