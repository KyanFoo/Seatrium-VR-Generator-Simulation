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
    public AudioSource off;
    public AudioSource gen1;
    public AudioSource gen2;
    public AudioSource gen3;
    public SynchroscopeManager synchroManager;

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
        off.Play();
        off.pitch = Random.Range(0.5f, 1.5f);
        Debug.Log("Called Function0");
    }
    public void Function1()
    {
        gen1.Play();
        gen1.pitch = Random.Range(0.5f, 1.5f);
        Debug.Log("Called Function1");
    }

    public void Function2()
    {
        gen2.Play();
        gen2.pitch = Random.Range(0.5f, 1.5f);
        synchroManager.synActive = true;
        Debug.Log("Called Function2");
    }
    public void Function3()
    {
        gen3.Play();
        gen3.pitch = Random.Range(0.5f, 1.5f);
        Debug.Log("Called Function3");
    }
    // Add more functions as needed.
}
