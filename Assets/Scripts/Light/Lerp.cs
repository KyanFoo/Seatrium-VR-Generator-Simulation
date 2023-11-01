using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    private Renderer _renderCube;
    public GameObject objCube;
    public Color color;

    public Material targetMaterial;
    public float startIntensity = 0.0f;
    public float targetIntensity = 1.0f;
    public float lerpDuration = 2.0f;

    private float currentIntensity;
    private float lerpStartTime;
    public float lerpTime;

    public bool isOn;

    private void Start()
    {
        _renderCube = objCube.GetComponent<Renderer>();
        _renderCube.material = targetMaterial;
        targetMaterial.EnableKeyword("_EMISSION");
        targetMaterial.SetColor("_EmissionColor", color * targetIntensity);
        // Initialize the current intensity and lerp start time.
        currentIntensity = startIntensity;
        lerpTime = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOn = true;
        }

        if (isOn == true)
        {
            LightUp();
        }
    }
    public void LightUp()
    {
        // Calculate the current time in the lerp.
        lerpTime += Time.deltaTime;

        // Calculate the new intensity using Mathf.Lerp.
        currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, lerpTime / lerpDuration);

        // Apply the new emission intensity to the material.
        targetMaterial.SetColor("_EmissionColor", color * currentIntensity);

        // You may need to enable emission if it's not already.
        targetMaterial.EnableKeyword("_EMISSION");

        // Optionally, you can stop lerping when the target intensity is reached.
        if (lerpTime >= lerpDuration)
        {
            enabled = false;
        }
    }
}
