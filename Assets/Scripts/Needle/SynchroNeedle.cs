using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SynchroNeedle : MonoBehaviour
{
    [Header("Pivot Transform GameObject")]
    //Represent the GameObject that is being rotated.//
    public Transform needle;

    //Represent the transform Vector3 to check where the rotation starts and ends.//
    private Vector3 startRotation;
    private Vector3 endRotation;

    //Represent the Duration of "Synchroscope Needle" rotating 360.//
    private float lerpTime;
    public float lerpDuration; //**DO NOT WRITE ANYTHING INTO THIS INPUT**//

    [Header("Secondary Scripts")]
    //Represent the "Secondary Script to access variables.//
    public SynchroscopeManager synchromanager;
    public GameManager gameManager;

    //Represent the bool to check whether the syncroscope has been switched "on" and the isolator has been switched.//
    private bool pauseSwitch;
    private bool startSwitch;
    private bool reverseSwitch;

    //Represent the rotation of the Needle, either going Clockwise or Anti-Clockwise.//
    private float rotation;

    //Represent the rotation Vectors to lerp the Needle back to 12 "0" Clock.//
    private Vector3 currentRotation;
    private float currentRotationZ;
    private Vector3 centerRotation;

    [Header("ValueMeters Scripts & Settings")]
    //Represent the scripts from ValueMeters.//
    public ValueMeter valueMeterFrequency1;
    public ValueMeter valueMeterFrequency2;

    //Represent the variable value from the repesctive ValueMeters.//
    public float frequencyValue1;
    public float frequencyValue2;

    [Header("Blackout GameObject")]
    //reference to blackout canvas.//
    public GameObject blackOut;
    public GameObject failedSync;
    public GameObject tutorialUI;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject light4;

    public bool PhaseSeqMatch;


    private void Start()
    {
        //Set the current rotation of the Synchroscope Needle.//
        startRotation = needle.rotation.eulerAngles;

        //Set the rotation to clockwise (360f).//
        endRotation = startRotation + new Vector3(0, 0, rotation);
    }

    private void Update()
    {
        //Check to see if the Isolator Switch has been switched "ON" or "OFF".//
        pauseSwitch = synchromanager.isolatorSwitch;

        //Check to see if the Synchroscope has been switched "ON" or "OFF".//
        startSwitch = synchromanager.isNeedlePause;

        //Check to see if the Reverse Switch has been switched "ON" or "OFF".//
        reverseSwitch = synchromanager.reverseLoop;

        //Get the lerpDuration from the Manager script.//
        //Make sure that the needle value is "1.3" more than the lamps.//
        //The needle movement is just right that is not too slow and fast and can have two lamps light up when it hit 12 o' clock.//
        lerpDuration = synchromanager.lerpDuration + 1.3f;

        if (startSwitch == true)
        {
            //Rotate the Synchroscope Needle Clockwise.//
            //The reason why the supposed rotation is clockwise is -360 is because our game scene, everything is in reverse.//
            rotation = -360.0f;
            StartCoroutine(NeedleCoroutine());
        }
        if (startSwitch == true && reverseSwitch == true)
        {
            //Rotate the Synchroscope Needle Anti-Clockwise.//
            //The reason why the supposed rotation is Anti-clockwise is 360 is because our game scene, everything is in reverse.//
            rotation = 360.0f;
            StartCoroutine(NeedleCoroutine());

            //Find its current transform rotation.//
            startRotation = needle.rotation.eulerAngles;
            lerpTime = 0;
        }
        //Apply new rotation.//
        endRotation = startRotation + new Vector3(0, 0, rotation);

        //Check whether the needle is within the deviation range.//
        if (pauseSwitch == true)
        {
            currentRotation = needle.rotation.eulerAngles;
            if (currentRotation.z > 180)
            {
                //Constantly update to check if needle GameObject had past the 180 rotation.//
                //If it had when past already, it will rotate forward to the center.//
                currentRotationZ = currentRotation.z;
                centerRotation.z = 360;
                currentRotationZ = currentRotation.z - 360;
            }
            else
            {
                //Constantly update to check if needle GameObject had past the 180 rotation.//
                //If it haven't past, it will rotate back its direction to the center.//
                currentRotationZ = currentRotation.z;
                centerRotation.z = 0;
            }

            //Constantly update to check if variable value is within the deviation.//
            if (currentRotationZ >= -25 && currentRotationZ <= 25)
            {
                Debug.Log("Within 20");
                lerpTime = 0;
                PhaseSeqMatch = true;
                gameManager.ErrorChecker();
                StartCoroutine(CenterNeedleCoroutine());
            }
            else
            {
                PhaseSeqMatch = false;
                //Breaker tripping actions will be carried out in this function.
                gameManager.ErrorChecker();
                tutorialUI.SetActive(false);
            }
        }

        //Constantly update to check variables value of the ValueMeters.//
        frequencyValue1 = valueMeterFrequency1.inputValue;
        frequencyValue2 = valueMeterFrequency2.inputValue;

        //Check to see if Running Generator is bigger than Incoming Generators.//
        if (frequencyValue2 < frequencyValue1)
        {
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

    public void BreakerTrip()
    {
        blackOut.SetActive(true);
        failedSync.SetActive(true);
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        light4.SetActive(false);
    }
}
