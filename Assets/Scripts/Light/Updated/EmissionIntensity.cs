using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionIntensity : MonoBehaviour
{
    Renderer renderer; // Reference to the Renderer component of the GameObject

    private void Start()
    {
        // Get the Renderer component attached to this GameObject
        renderer = GetComponent<Renderer>();

        // Check if the material has an emission property
        if (renderer.material.HasProperty("_EmissionColor"))
        {
            // Get the current emission color of the material
            Color emissionColor = renderer.material.GetColor("_EmissionColor");

            // Get the intensity value from the emission color
            float emissionIntensity = emissionColor.maxColorComponent;

            // Log the emission intensity value to the console
            Debug.Log("Emission Intensity: " + emissionIntensity);
        }
        else
        {
            Debug.LogWarning("The material does not have an emission property.");
        }
    }
}
