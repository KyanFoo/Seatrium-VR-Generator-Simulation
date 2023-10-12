using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroscopeManager : MonoBehaviour
{
    public Fusion lamp0Script;
    public Fusion lamp1Script;
    public Fusion lamp2Script;

    public bool nextLamp0 = false;
    public bool nextLamp1 = false;
    public bool nextLamp2 = false;

    public bool SynActive = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A))
        {
        }
            if (SynActive == true)
        {
            if (!nextLamp0)
            {
                lamp0Script.CallOnCoroutine();
                nextLamp0 = true;
            }
            if (nextLamp0 == true && !nextLamp1 && lamp0Script.onLight == false)
            {
                lamp1Script.CallOnCoroutine();
                nextLamp1 = true;
            }
            if (nextLamp0 == true && nextLamp1 == true && !nextLamp2 && lamp1Script.onLight == false)
            {
                lamp2Script.CallOnCoroutine();
                nextLamp2 = true;
            }
            if (nextLamp1 == true && nextLamp2 == true && lamp2Script.onLight == false)
            {
                nextLamp0 = false;
                nextLamp1 = false;
                nextLamp2 = false;
            }
        }
    }
}
