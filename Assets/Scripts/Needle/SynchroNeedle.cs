using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroNeedle : MonoBehaviour
{
    [Header("Pivot Transform GameObject")]
    //Represent the GameObject that is being rotated.
    public Transform needle;

    //Represent the transform Vector3 to check where the rotation starts and ends.
    private Vector3 startRotation;
    private Vector3 endRotation;

    //Represent the Duration of "Synchroscope Needle" rotating 360.
    private float lerpTime;
    public float lerpDuration; //**DO NOT WRITE ANYTHING INTO THIS INPUT**//

    [Header("Secondary Scripts")]
    //Represent the "Secondary Script to access variables.
    public SynchroscopeManager synchromanager;

    //Represent the bool to check whether the syncroscope has been switched "on" and the isolator has been switched.
    private bool pauseSwitch;
    private bool startSwitch;
    private bool reverseSwitch;

    //Represent the rotation of the Needle, either going Clockwise or Anti-Clockwise.
    private float rotation;

    //Represent the rotation Vectors to lerp the Needle back to 12 "0" Clock.
    private Vector3 currentRotation;
    private float currentRotationZ;
    private Vector3 centerRotation;

    public ValueMeter valueMeterFrequency1;
    public ValueMeter valueMeterFrequency2;
    public float frequencyValue1;
    public float frequencyValue2;

    //reference to blackout canvas
    public GameObject blackOut;


    private void Start()
    {
        startRotation = needle.rotation.eulerAngles;
        endRotation = startRotation + new Vector3(0, 0, rotation);
    }

    private void Update()
    {
        //Retreive variable bool of respective bool switches.
        pauseSwitch = synchromanager.isolatorSwitch;
        startSwitch = synchromanager.isNeedlePause;
        reverseSwitch = synchromanager.reverseLoop;

        //Get the lerpDuration from the Manager script
        //Make sure that the needle value is "1.3" more than the lamps.
        //The needle movement is just right that is not too slow and fast and can have two lamps light up when it hit 12 o' clock.
        lerpDuration = synchromanager.lerpDuration + 1.3f;

        if (startSwitch == true)
        {
            //Rotate the Synchroscope Needle Clockwise.
            //The reason why the supposed rotation is clockwise is -360 is because our game scene, everything is in reverse.
            rotation = -360.0f;
            StartCoroutine(NeedleCoroutine());
        }
        if (startSwitch == true && reverseSwitch == true)
        {
            //Rotate the Synchroscope Needle Anti-Clockwise.
            rotation = 360.0f;
            StartCoroutine(NeedleCoroutine());
            startRotation = needle.rotation.eulerAngles;
            lerpTime = 0;
        }
        //Apply new rotation.
        endRotation = startRotation + new Vector3(0, 0, rotation);

        //Check whether the needle is within the deviation range.
        if (pauseSwitch == true)
        {
            currentRotation = needle.rotation.eulerAngles;
            if (currentRotation.z > 180)
            {
                currentRotationZ = currentRotation.z;
                centerRotation.z = 360;
                currentRotationZ = currentRotation.z - 360;
            }
            else
            {
                currentRotationZ = currentRotation.z;
                centerRotation.z = 0;
            }

            if (currentRotationZ >= -25 && currentRotationZ <= 25)
            {
                Debug.Log("Within 20");
                lerpTime = 0;
                StartCoroutine(CenterNeedleCoroutine());
            }
            else
            {
                blackOut.SetActive(true);
            }
        }
        frequencyValue1 = valueMeterFrequency1.inputValue;
        frequencyValue2 = valueMeterFrequency2.inputValue;
        if (frequencyValue2 < frequencyValue1)
        {
            //Debug.Log("Anti");
            synchromanager.reverseLoop = true;
        }
        else
        {
            synchromanager.reverseLoop = false;
        }
    }
    IEnumerator NeedleCoroutine()
    {
        //"pauseSwitch" is used to check whether the player has switch on the Isolator.
        //If switched, the rotation out of "needle" will stop and it will not affect the entire game.
        if (pauseSwitch == false)
        {
            lerpTime += Time.deltaTime;
        }
        needle.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, endRotation, lerpTime / lerpDuration));

        if (lerpTime >= lerpDuration)
        {
            lerpTime = 0.0f;
        }
        yield return null;
    }
    IEnumerator CenterNeedleCoroutine()
    {
        Debug.Log("Center Needle");
        lerpTime += Time.deltaTime;
        needle.rotation = Quaternion.Euler(Vector3.Lerp(currentRotation, centerRotation, lerpTime / 1));
        yield return null;
    }
}
