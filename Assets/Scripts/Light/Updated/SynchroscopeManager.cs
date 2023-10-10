using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeManager : MonoBehaviour
{
    public LampEmissiveOn onScript;
    public LampEmissiveOff offScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            Debug.Log("up");
            onScript.CallCoroutine();
        }
        if (Input.GetKeyDown("down"))
        {
            Debug.Log("down");
            offScript.CallCoroutine();
        }
    }
}
