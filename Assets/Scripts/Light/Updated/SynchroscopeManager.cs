using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeManager : MonoBehaviour
{
    public Fusion lamp0Script;
    public Fusion lamp1Script;
    public Fusion lamp2Script;

    public bool nextLamp0 = false;
    public bool nextLamp1 = false;
    public bool nextLamp2 = false;

    public bool synActive = true;
    public bool reverseLoop = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (synActive == true)
        {
            ActiveSync();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lamp0Script.lerpDuration = lamp0Script.lerpDuration + 1;
            lamp1Script.lerpDuration = lamp1Script.lerpDuration + 1;
            lamp2Script.lerpDuration = lamp2Script.lerpDuration + 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lamp0Script.lerpDuration = lamp0Script.lerpDuration - 1;
            lamp1Script.lerpDuration = lamp1Script.lerpDuration - 1;
            lamp2Script.lerpDuration = lamp2Script.lerpDuration - 1;
        }
    }
    public void ActiveSync()
    {
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
        nextLamp0 = false;
        nextLamp1 = false;
        nextLamp2 = false;
    }
}
