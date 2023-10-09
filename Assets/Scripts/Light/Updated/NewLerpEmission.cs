using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLerpEmission : MonoBehaviour
{
    public Material emissiveMaterial;
    private Renderer _RenderCube;
    public GameObject objCube;

    public Color color;
    private float startIntensity;
    public float endIntensity;
    private float intensity;
    private float setIntensity;

    private float lerpStartTime;
    public float lerpDuration = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Getting access to the Renderer component of a GameObject.//
        _RenderCube = objCube.GetComponent<Renderer>();
        //Changing the material of a GameObject from basic material to EmissiveMaterial.//
        _RenderCube.material = emissiveMaterial;
        //Enable Emission in Material.//
        //Set Intensity Value to 0.//
        //Setting the Material that the emission is "On" but there is no light.//
        emissiveMaterial.EnableKeyword("_EMISSION");
        intensity = 0;
        setIntensity = 0;
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);

        StartCoroutine(LightCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            // Calculate the current time in the lerp.
            float lerpTime = Time.time - lerpStartTime;

            emissiveMaterial.EnableKeyword("_EMISSION");
            startIntensity = setIntensity;
            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        }
        if (Input.GetKeyDown("down"))
        {
            emissiveMaterial.EnableKeyword("_EMISSION");
            intensity = 10;
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        }
    }
    IEnumerator LightCoroutine()
    {
        while (true)
        {
            // Calculate the current time in the lerp.
            float lerpTime = Time.time - lerpStartTime;

            emissiveMaterial.EnableKeyword("_EMISSION");
            startIntensity = setIntensity;
            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
    }
}
