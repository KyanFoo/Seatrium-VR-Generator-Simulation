using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeManager : MonoBehaviour
{
    [Header("Lamp GameObject")]
    //Represent the "Secondary Script" to access variables.//
    //Drag & Drop the Synchroscope Lamp's script to the respective slots.//
    public SynchroscopeLamp lamp0Script;
    public SynchroscopeLamp lamp1Script;
    public SynchroscopeLamp lamp2Script;

    //Represent the Bool used for if else statement to check which GameObject turn is to fade "In".//
    public bool nextLamp0 = false;
    public bool nextLamp1 = false;
    public bool nextLamp2 = false;

    [Header("Input Bool Settings")]
    //Represent Bool to "ON" the synchroscope.//
    public bool synActive = true;

    //Represent the Bool to reverse the rotation of fade "In" and "Out".//
    public bool reverseLoop;

    //Represent the Bool to check when the Isolator has been switched "ON".//
    public bool isolatorSwitch;

    [Header("Input Emissive Duration Setting")]
    //Represent the value of the check the variables of the lamps.//
    public float lerpDuration;
    public float duration;

    [Header("Input Intensity & LerpTime Settings")]
    //Represent the value of intensity of the lamps.//
    public float startIntensity;
    public float endIntensity;

    //Represne the special variable that pauses the needle rotation constant updates when the isolator switch has been switched.//
    public bool isNeedlePause;

    public GameManager GameManager;
    public RotaryKnobBehaviour RotaryKnob;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isStartLoadSharingScene == true && RotaryKnob.LoadSharingSceneStartSynchro == true)
        {
            isolatorSwitch = true;
        }
        //Constantly update to check variable duration value to be seen in "Inspector".//
        duration = lamp0Script.lerpTime;

        //When the Synchroscope has been switched "ON".//
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
        //Function is called to, call other fucntion to allow the lamps to fade "In" & "Out" in a speific order.//
        //It also check whether the lamps' speific order has to be Clockwise or Anti-Clockwise.//

        //Clockwise Motion.//
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
        //Anti-Clockwise Motion.//
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
    //Functions below are called to, call their lamp function to fade "In" & also Bool is check "True".
    //It is to tell the code that it has been fade "In" and await for the next lamp's turn.
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
    public void ResetBool()
    {
        //Function is called to, after a cycle has been complete, reset is needed to repear the process again.//
        nextLamp0 = false;
        nextLamp1 = false;
        nextLamp2 = false;
    }
    public void FlipIncrease()
    {
        //Function is called to, when the Governor Knob has been flipped.//
        //It would increase the speed of the Lamps fade "In" and "Out" sequence and Synchroscope Needle.//
        lerpDuration = lerpDuration + 0.1f;
    }
    public void FlipDecrease()
    {
        //Function is called to, when the Governor Knob has been flipped.//
        //It would decrease the speed of the Lamps fade "In" and "Out" sequence and Synchroscope Needle.//
        lerpDuration = lerpDuration - 0.1f;
    }
}
