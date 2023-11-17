using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeLamp: MonoBehaviour
{
    [Header("Emissive Material & GameObject")]
    //Drag & Drop the Emissive Material & GameObject model.//
    public Material emissiveMaterial;
    private Renderer _renderCube;
    public GameObject objCube;

    [Header("Emissive Color Material")]
    //Represent Color of Emissive Material.//
    public Color color;

    [Header("Intensity & LerpTime Settings")]
    //Represent the level of intensity of "EmissiveMaterial".//
    public float startIntensity;  //**DO NOT WRITE ANYTHING INTO THIS INPUT**//
    public float endIntensity;  //**DO NOT WRITE ANYTHING INTO THIS INPUT**//
    public float intensity;

    //Represent the Duration of Emissive Material fading IN and OUT.//
    private float lerpStartTime;
    public float lerpTime; //**DO NOT WRITE ANYTHING INTO THIS INPUT**//
    private float lerpDuration;

    [Header("ON/OFF Boolean")]
    //Represent the Bools to check when each GameObject is fading IN or OUT.//
    public bool onLight;
    public bool offLight;

    [Header("Secondary Scripts")]
    //Represent the "Secondary Script" to access variables.//
    public SynchroscopeManager synchromanager;

    //Represent the Bool to check when the Isolator has been switched "ON".//
    public bool pauseSwitch;

    // Start is called before the first frame update
    void Start()
    {
        //Apply Renderer on GameObject.//
        _renderCube = objCube.GetComponent<Renderer>();

        //Apply Emissive Material onto GameObject.//
        _renderCube.material = emissiveMaterial;

        //Enable the Emissive Map and set the Color and Intensity of the Emissive Material.//
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        lerpTime = 0f;
    }
    public void CallOnCoroutine()
    {
        //Function is called to, start the Coroutine of the Emissive Material to fade IN.//
        onLight = true;
        lerpTime = 0;
        StartCoroutine(OnCoroutine());
    }
    public void CallOffCoroutine()
    {
        //Function is called to, start the Coroutine of the Emissive Material to fade OUT.//
        offLight = true;
        lerpTime = 0;
        StartCoroutine(OffCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if the Isolator Switch has been switched "ON" or "OFF".//
        pauseSwitch = synchromanager.isolatorSwitch;

        //Constantly update to check variables value of total duration of the Lerp Duration.//
        lerpDuration = synchromanager.lerpDuration;

        //Constantly update to check variables value of StartIntensity & EndIntensity of lamps.
        startIntensity = synchromanager.startIntensity;
        endIntensity = synchromanager.endIntensity;

        //Constantly update to check the duration left whether the GameObject is fading IN or OUT is about to be completed.//
        if (lerpTime / lerpDuration >= 1.0f && onLight == true)
        {
            //Disable "LightOnCoroutine" Coroutine.//
            StopOnCoroutineAndExitLoop();
        }
        if (lerpTime / lerpDuration >= 1.0f && offLight == true)
        {
            //Disable "LightOnCoroutine" Coroutine.//
            StopOffCoroutineAndExitLoop();
        }
    }
    IEnumerator OnCoroutine()
    {
        //Bool to check to tell the script to "ON" the lamp.//
        while (onLight == true)
        {
            //"PauseSwitch" is used to check whether the player has switch "ON" the Isolator.//
            //If switched, the fading in of "EmissiveMaterial" will stop and it will not affect the entire game.//
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "EmissiveMaterial".//
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        //Debug.Log("Finish Lighting Up");
    }
    private void StopOnCoroutineAndExitLoop()
    {
        //When the GameObject is fading "IN" is almost complete it stop the fade "IN" function and call a function to fade "OUT".//
        onLight = false;
        StopCoroutine(OnCoroutine());
        CallOffCoroutine();
    }
    IEnumerator OffCoroutine()
    {
        //Bool to check to tell the script to "OFF" the lamp.//
        while (offLight == true)
        {
            //"PauseSwitch" is used to check whether the player has switch "ON" the Isolator.//
            //If switched, the fading in of "EmissiveMaterial" will stop and it will not affect the entire game.//
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "EmissiveMaterial".
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(endIntensity, startIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        //Debug.Log("Finish Lighting Down");
    }
    private void StopOffCoroutineAndExitLoop()
    {
        //When the GameObject is fading "OUT" is almost complete it stop the fade "OUT" function.
        offLight = false;
        StopCoroutine(OffCoroutine());
    }
}
