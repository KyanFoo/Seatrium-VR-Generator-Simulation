using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float lerpStartTime;
    public float lerpTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime = Time.time - lerpStartTime;
        Debug.Log(lerpTime);
    }
}
