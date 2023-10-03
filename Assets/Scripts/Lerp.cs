using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Material targetMaterial;
    public float startIntensity = 0.0f;
    public float targetIntensity = 1.0f;
    public float lerpDuration = 2.0f;

    private float currentIntensity;
    private float lerpStartTime;

    private void Start()
    {
        // Initialize the current intensity and lerp start time.
        currentIntensity = startIntensity;
        lerpStartTime = Time.time;
    }

    private void Update()
    {
        // Calculate the current time in the lerp.
        float lerpTime = Time.time - lerpStartTime;

        // Calculate the new intensity using Mathf.Lerp.
        currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, lerpTime / lerpDuration);

        // Apply the new emission intensity to the material.
        targetMaterial.SetColor("_EmissionColor", Color.white * currentIntensity);

        // You may need to enable emission if it's not already.
        targetMaterial.EnableKeyword("_EMISSION");

        // Optionally, you can stop lerping when the target intensity is reached.
        if (lerpTime >= lerpDuration)
        {
            currentIntensity = targetIntensity;
            enabled = false; // Disable this script.
        }
    }
}
