using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Reflection;

public class RotaryCallFunction : MonoBehaviour
{
    [Header("Secondary Script")]
    //All variables' data are retreive from this script//
    public RotaryKnobBehaviour rotaryknobbehaviour;
    public int numInterval;
    public AudioSource audioSource;
    public AudioClip clip1, clip2, clip3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numInterval = rotaryknobbehaviour.numberPlace;

        string functionName = "Function" + numInterval;

        // Use reflection to get the method with the specified name.
        MethodInfo methodInfo = GetType().GetMethod(functionName);

        if (methodInfo != null)
        {
            // Call the method if it exists.
            methodInfo.Invoke(this, null);
        }
        else
        {
            Debug.LogWarning("Function not found: " + functionName);
        }
    }
    public void Function0()
    {
        Debug.Log("Called Function0");
    }
    public void Function1()
    {
        audioSource.clip = clip1;
        audioSource.Play();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        Debug.Log("Called Function1");
    }

    public void Function2()
    {
        audioSource.clip = clip2;  
        audioSource.Play();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        Debug.Log("Called Function2");
    }
    public void Function3()
    {
        audioSource.clip = clip3;
        audioSource.Play();
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        Debug.Log("Called Function3");
    }
    // Add more functions as needed.
}
