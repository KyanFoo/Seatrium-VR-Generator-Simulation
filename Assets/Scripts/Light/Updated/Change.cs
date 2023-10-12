using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    public Material newMaterial;
    public GameObject[] gameObjects;
    public string[] existingNames;

    private void Start()
    {
        ApplyMaterialAndSetNames();
        LogNames();
        Invoke("ChangeMaterialForSpecificObject", 3f);
    }

    private void ApplyMaterialAndSetNames()
    {
        if (gameObjects.Length != existingNames.Length)
        {
            Debug.LogError("The number of GameObjects and existing names does not match.");
            return;
        }

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Renderer renderer = gameObjects[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
            }
            else
            {
                Debug.LogWarning("Renderer component not found on GameObject " + gameObjects[i].name);
            }

            gameObjects[i].name = existingNames[i];
        }
    }

    private void LogNames()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Debug.Log("Unique name of GameObject " + i + ": " + gameObjects[i].name);
        }
    }

    public void ChangeMaterialByName(string uniqueName, Material newMaterial)
    {
        GameObject foundObject = GameObject.Find(uniqueName);

        if (foundObject != null)
        {
            Renderer renderer = foundObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = newMaterial;
                Debug.Log("Material changed for " + uniqueName);
            }
            else
            {
                Debug.LogWarning("Renderer component not found on GameObject " + uniqueName);
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the name: " + uniqueName);
        }
    }

    // Example usage to change material of a specific GameObject by name
    // Call this function from elsewhere in your code, e.g., from a button click.
    public void ChangeMaterialForSpecificObject()
    {
        ChangeMaterialByName("Lamp1", newMaterial);
    }
}
