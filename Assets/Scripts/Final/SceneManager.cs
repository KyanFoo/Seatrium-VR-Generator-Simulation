using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int sceneno = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Increase()
    {
        sceneno += 1;
    }
    public void Decrease()
    {
        sceneno -= 1;
    }
}
