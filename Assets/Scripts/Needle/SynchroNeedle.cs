using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroNeedle : MonoBehaviour
{
    //Represent the gameobject that is being rotated.
    public Transform objectToRotate;

    //Represent the transform Vector3 to check where the rotation starts and ends.
    private Vector3 startRotation;
    private Vector3 endRotation;

    //Represent the Duration of "Synchroscope Needle" rotating 360.
    //Make sure that the needle value is "0.3" lesser than the lamps.
    //The needle movement is just right that is not too slow and fast and can have two lamps light up when it hit 12 o' clock.
    private float lerpTime;
    public float lerpDuration = 3.7f;

    //Represent the "Secondary Script to access variables.
    public SynchroscopeManager synchromanager;

    //Represent the bool to check whether the syncroscope has been switched "on" and the isolator has been switched.
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
        //"pauseSwitch" is used to check whether the player has switch on the Isolator.
        //If switched, the rotation out of "needle" will stop and it will not affect the entire game.
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
