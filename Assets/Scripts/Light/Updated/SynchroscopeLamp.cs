using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeLamp: MonoBehaviour
{
    [Header("Emissive Material & GameObject")]
    //Represent the "EmissiveMaterial" & "GameObject Model".
    public Material emissiveMaterial;
    private Renderer _renderCube;
    public GameObject objCube;

    [Header("Emissive Color Material")]
    //Represent Color of "EmissiveMaterial".
    public Color color;

    [Header("Intensity & LerpTime Settings")]
    //Represent the level of intensity of "EmissiveMaterial".
    public float startIntensity; //**DO NOT WRITE ANYTHING INTO THIS INPUT**//
    public float endIntensity; //**DO NOT WRITE ANYTHING INTO THIS INPUT**//
    private float intensity;

    //Represent the Duration of "EmissiveMaterial" fading in and out.
    private float lerpStartTime;
    public float lerpTime; //**DO NOT WRITE ANYTHING INTO THIS INPUT**//
    private float lerpDuration;

    [Header("ON/OFF Boolean")]
    //Represent the bool to check when each GameObject is fading in or fading out.
    public bool onLight;
    public bool offLight;

    [Header("Secondary Scripts")]
    //Represent the "Secondary Script to access variables.
    public SynchroscopeManager synchromanager;
    public bool pauseSwitch;

    // Start is called before the first frame update
    void Start()
    {
        //Retrieve access of GameObject renderer,
        //Apply "EmissiveMaterial" onto GameObject,
        //Set the color of the "EmissiveMaterial".
        _renderCube = objCube.GetComponent<Renderer>();
        _renderCube.material = emissiveMaterial;
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        lerpTime = 0f;

        //CallOnCoroutine();
    }

    //A function called from the "SynchroscopeManager" to start the coroutine to fade in the emissive material.
    public void CallOnCoroutine()
    {
        onLight = true;
        lerpTime = 0;
        StartCoroutine(OnCoroutine());
    }
    //A function called to start the coroutine to fade out the emissive material.
    public void CallOffCoroutine()
    {
        
        offLight = true;
        lerpTime = 0;
        StartCoroutine(OffCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        //Retrieve variable bool vlaue to check whether isolator has been switched.
        pauseSwitch = synchromanager.isolatorSwitch;

        //Retrieve variable lerp duration from Manager Script.
        lerpDuration = synchromanager.lerpDuration;

        //Retrieve variable of Start and End Intensity from Manager Script.
        startIntensity = synchromanager.startIntensity;
        endIntensity = synchromanager.endIntensity;

        //Check the duration left whether the  GameObject is fading in or out is also complete.
        if (lerpTime / lerpDuration >= 1.0f && onLight == true)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopOnCoroutineAndExitLoop();
        }
        if (lerpTime / lerpDuration >= 1.0f && offLight == true)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopOffCoroutineAndExitLoop();
        }
    }
    IEnumerator OnCoroutine()
    {
        //Bool to check to tell the script to "on" the light.
        while (onLight == true)
        {
            //"pauseSwitch" is used to check whether the player has switch on the Isolator.
            //If switched, the fading in of "EmissiveMaterial" will stop and it will not affect the entire game.
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "EmissiveMaterial".
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        //Debug.Log("Finish Lighting Up");
    }
    private void StopOnCoroutineAndExitLoop()
    {
        //When the GameObject is fading in is almost complete it stop the fade in function and call a function to fade out.
        onLight = false;
        StopCoroutine(OnCoroutine());
        CallOffCoroutine();
    }
    IEnumerator OffCoroutine()
    {
        //Bool to check to tell the script to "off" the light.
        while (offLight == true)
        {
            //"pauseSwitch" is used to check whether the player has switch on the Isolator.
            //If switched, the fading out of "EmissiveMaterial" will stop and it will not affect the entire game.
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
        //When the GameObject is fading in is almost complete it stop the fade in function.
        offLight = false;
        StopCoroutine(OffCoroutine());
    }
}
