using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLerpEmission : MonoBehaviour
{
    //Variable of Emissive Material//
    [Header("Emissive Material")]
    public Material emissiveMaterial;

    //Renderer of Gameobjects and GameObject themselves//
    private Renderer _RenderCube;
    [Header("GameObject")]
    public GameObject objCube;

    //Emissive Colot//
    [Header("Emissive Color")]
    public Color color;

    private float startIntensity;
    public float endIntensity;
    private float intensity;
    private float setIntensity;

    private float originalStartIntensity;
    private float originalEndIntensity;

    //Duration of Lerping from start to end//
    private float lerpStartTime;
    private float lerpTime;
    public float lerpDuration = 2.0f;

    [Header("Bool Testing")]
    public bool StartOnLight = true;
    public bool StartOffLight = false;

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        //Getting access to the Renderer component of a GameObject.//
        _RenderCube = objCube.GetComponent<Renderer>();
        //Changing the material of a GameObject from basic material to EmissiveMaterial.//
        _RenderCube.material = emissiveMaterial;
        //Enable Emission in Material.//
        emissiveMaterial.EnableKeyword("_EMISSION");
        //Set Intensity Value to 0.//
        intensity = 0;
        setIntensity = 0;
        //Setting the Material that the emission is "On" but there is no light.//
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);

        StartCoroutine(LightOnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down"))
        {
            //emissiveMaterial.EnableKeyword("_EMISSION");
            //intensity = 10;
            //emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        }
        if (lerpTime / lerpDuration >= 1.0f)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopCoroutineAndExitLoop();
        }
        if (StartOnLight == false)
        {
            StartCoroutine(LightOffCoroutine());
        }
    }
    IEnumerator LightOnCoroutine()
    {
        while (StartOnLight == true)
        {
            //Calculate the current time in the lerp.//
            lerpTime = Time.time - lerpStartTime;
            //Enable Emissive Material.//
            emissiveMaterial.EnableKeyword("_EMISSION");
            startIntensity = setIntensity;
            //Smooth Lerp of Emissive Intensity of the material.//
            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            //Setting the emissive material with its color and intensity.//
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);

            //Retreive Varaible for later//
            //Start > 0, End = 0//
            originalStartIntensity = endIntensity;
            originalEndIntensity = startIntensity;
            yield return null;
        }
        Debug.Log("Finish Lighting Up");
    }
    private void StopCoroutineAndExitLoop()
    {
        StartOnLight = false;
        StopCoroutine(LightOnCoroutine());
    }
    IEnumerator LightOffCoroutine()
    {
        while (StartOffLight == true)
        {
            Debug.Log("Hello");
            yield return null;
        }
        Debug.Log("Finish Lighting Down");
    }
}
