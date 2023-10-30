using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeLamp: MonoBehaviour
{
    //Represent the "Emissive Material" & "Gameobject Model".
    public Material emissiveMaterial;
    private Renderer _renderCube;
    public GameObject objCube;

    //Represent Color of "EmissiveMaterial".
    public Color color;

    //Represent the level of intensity of "EmissiveMaterial".
    public float startIntensity;
    public float endIntensity;
    private float intensity;

    public float savedStartIntensity;
    public float savedEndIntensity;

    //Represent the Duration of "Emissive Material" fading in and out.
    private float lerpStartTime;
    public float lerpTime;
    public float lerpDuration;

    //Represent the bool to check when each Gameobject is fading in or fading out.
    public bool onLight;
    public bool offLight;

    //Represent the "Secondary Script to access variables.
    public SynchroscopeManager synchromanager;
    public bool pauseSwitch;

    // Start is called before the first frame update
    void Start()
    {
        //Retrieve access of Gameobject renderer,
        //Apply "emissiveMaterial" onto Gameobject,
        //Set the color of the "emissiveMaterial".
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
        //Retrieve variable bool vlaue to check whether isolator has been switch on.
        pauseSwitch = synchromanager.isolatorSwitch;

        //Retrieve variable lerp duration from Manager.
        lerpDuration = synchromanager.lerpDuration;

        //Check the duration left whether the  gameobject is fading in or out is also complete.
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
            //If switched, the fading in of "emissiveMaterial" will stop and it will not affect the entire game.
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "emissiveMaterial".
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        Debug.Log("Finish Lighting Up");
    }
    private void StopOnCoroutineAndExitLoop()
    {
        //When the gameobject is fading in is almost complete it stop the fade in function and call a function to fade out.
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
            //If switched, the fading out of "emissiveMaterial" will stop and it will not affect the entire game.
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "emissiveMaterial".
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(endIntensity, startIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        Debug.Log("Finish Lighting Down");
    }
    private void StopOffCoroutineAndExitLoop()
    {
        //When the gameobject is fading in is almost complete it stop the fade in function.
        offLight = false;
        StopCoroutine(OffCoroutine());
    }
}
