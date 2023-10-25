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

    public float speedValue;
    public float duration;
    public bool isolatorSwitch;

    public GameObject needle;
    public float startRotation = 0.0f; // Starting rotation
    public float endRotation = 360.0f; // Ending rotation
    public float rotationDuration = 5.0f; // Duration of the rotation in seconds

    private float currentRotation = 0.0f;
    private float lerpTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LampCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        speedValue = lamp0Script.lerpDuration;
        duration = lamp0Script.lerpTime;
        if (synActive == true)
        {
            ActiveSync();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lamp0Script.lerpDuration = lamp0Script.lerpDuration + 0.1f;
            lamp1Script.lerpDuration = lamp1Script.lerpDuration + 0.1f;
            lamp2Script.lerpDuration = lamp2Script.lerpDuration + 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            lamp0Script.lerpDuration = lamp0Script.lerpDuration - 0.1f;
            lamp1Script.lerpDuration = lamp1Script.lerpDuration - 0.1f;
            lamp2Script.lerpDuration = lamp2Script.lerpDuration - 0.1f;
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
    IEnumerator LampCoroutine()
    {
        while (isolatorSwitch == false)
        {
            lerpTime += Time.deltaTime;
            // Calculate the rotation angle using Mathf.Lerp
            currentRotation = Mathf.Lerp(startRotation, endRotation, lerpTime / rotationDuration);

            // Apply the rotation to the GameObject's Z-axis
            needle.transform.rotation = Quaternion.Euler(0, 0, currentRotation);

            // Reset the time elapsed and repeat the rotation
            if (lerpTime >= rotationDuration)
            {
                lerpTime = 0.0f;
            }
            yield return null;
        }
    }
}
