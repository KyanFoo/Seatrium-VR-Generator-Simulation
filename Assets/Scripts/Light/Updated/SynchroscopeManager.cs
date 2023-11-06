using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeManager : MonoBehaviour
{
    [Header("Lamp GameObject")]
    //Represent the "Secondary Script to access variables.
    public SynchroscopeLamp lamp0Script;
    public SynchroscopeLamp lamp1Script;
    public SynchroscopeLamp lamp2Script;

    //Represent the bool used for if else statement to check which gameobject turn is to fade in.
    public bool nextLamp0 = false;
    public bool nextLamp1 = false;
    public bool nextLamp2 = false;

    [Header("Input Bool Settings")]
    //Represent bool to "on" the synchroscope.
    public bool synActive = true;

    //Represent the bool to reverse the rotation of fade in and out after a certain critia is made.
    public bool reverseLoop;

    //Represent the bool to check when the isolator has been switched on.
    public bool isolatorSwitch;

    [Header("Input Emissive Duration Setting")]
    //Represent the value of the check the variables of the lamps.
    public float lerpDuration;
    public float duration;

    [Header("Input Intensity & LerpTime Settings")]
    public float startIntensity;
    public float endIntensity;

    //Represne the special variabel that pauses the needle rotation constant updates when the isolator switch has been switched.
    public bool isNeedlePause;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Update variables to see in "Inspector".
        duration = lamp0Script.lerpTime;

        //Activate the "Synchroscope".
        if (synActive == true)
        {
            isNeedlePause = true;
            ActiveSync();
        }
        else
        {
            isNeedlePause= false;
        }

        if (isolatorSwitch == true)
        {
            isNeedlePause = false;
        }

        //Codes use to increase or decrease the duration of fading in and out of the lamps.
        if (Input.GetKeyDown("1"))
        {
            //lerpDuration = lerpDuration + 0.1f;
            isolatorSwitch = true;
        }
        if (Input.GetKeyDown("2"))
        {
            //lerpDuration = lerpDuration - 0.1f;
            isolatorSwitch = false;
        }
    }
    public void ActiveSynchro()
    {
        isolatorSwitch = true;
    }
    public void DectiveSynchro()
    {
        isolatorSwitch = false;
    }
    public void ActiveSync()
    {
        //The order of the lamps fading in and out oridinal order
        if (reverseLoop == false)
        {
            if (!nextLamp0)
            {
                CallLamp0Script();
            }
            if (nextLamp0 == true && !nextLamp1 && lamp0Script.onLight == false)
            {
                CallLamp1Script();
            }
            if (nextLamp0 == true && nextLamp1 == true && !nextLamp2 && lamp1Script.onLight == false)
            {
                CallLamp2Script();
            }
            if (nextLamp0 == true && nextLamp1 == true && nextLamp2 == true && lamp2Script.onLight == false)
            {
                ResetBool();
            }
        }
        //The order of the lamps fading in and out reverse order
        if (reverseLoop == true)
        {
            if (!nextLamp0 && reverseLoop == true)
            {
                CallLamp0Script();
            }
            if (nextLamp0 == true && !nextLamp2 && lamp0Script.onLight == false && reverseLoop == true)
            {
                CallLamp2Script();
            }
            if (nextLamp0 == true && nextLamp2 == true && !nextLamp1 && lamp2Script.onLight == false && reverseLoop == true)
            {
                CallLamp1Script();
            }
            if (nextLamp0 == true && nextLamp1 == true && nextLamp2 == true && lamp1Script.onLight == false && reverseLoop == true)
            {
                ResetBool();
            }
        }
    }
    //After calling their function to light up their lamps, bool is check "true" to tell the code that it has been light up already and await for the next one.
    public void CallLamp0Script()
    {
        lamp0Script.CallOnCoroutine();
        nextLamp0 = true;
    }
    public void CallLamp1Script()
    {
        lamp1Script.CallOnCoroutine();
        nextLamp1 = true;
    }
    public void CallLamp2Script()
    {
        lamp2Script.CallOnCoroutine();
        nextLamp2 = true;
    }
    //After a cycle has been complete, reset is needed to repeat the process again.
    public void ResetBool()
    {
        nextLamp0 = false;
        nextLamp1 = false;
        nextLamp2 = false;
    }
}
