using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Material newMaterial;
    public GameObject lights;

    public void ChangeMaterial()
    {
        // Get the Renderer component of the object to change material
        Renderer renderer = lights.GetComponent<Renderer>();

        // Check if the Renderer component exists
        if (renderer != null)
        {
            // Assign the new material to the object
            renderer.material = newMaterial;
        }
        else
        {
            // Log a warning if the Renderer component is not found
            Debug.LogWarning("No Renderer on object");
        }
    }
}
