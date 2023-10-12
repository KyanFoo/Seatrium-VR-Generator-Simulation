using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Array : MonoBehaviour
{
    public Material newMaterial;
    public GameObject[] gameObjects;
    public string[] existingNames;

    private void Start()
    {
        ApplyMaterialAndSetNames();
        LogNames();
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
    void Update()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == "Lamp0")
            {
                Debug.Log("Yes");
            }
        }
    }
}
