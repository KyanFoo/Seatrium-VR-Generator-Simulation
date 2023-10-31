using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
