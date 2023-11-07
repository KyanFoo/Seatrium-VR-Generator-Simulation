using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Generator 1 Variables")]
    public float Gen1Power;
    public float Gen1Current;
    public float Gen1Voltage;
    public float Gen1Frequency;
    public ValueMeter ValueMeter1Power;
    public ValueMeter ValueMeter1Current;
    public ValueMeter ValueMeter1Voltage;
    public ValueMeter ValueMeter1Frequency;

    public SynchroscopeManager SynchroManage;
    public bool IsolatorToggle;

    // Start is called before the first frame update
    void Start()
    {
        //ValueMeter1Power.inputValue = ;
        //ValueMeter1Current.inputValue = ;
        //ValueMeter1Voltage.inputValue = ;
        //ValueMeter1Frequency.inputValue = ;
        Invoke("StartScene", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Gen1Power = ValueMeter1Power.inputValue;
        Gen1Current = ValueMeter1Current.inputValue;
        Gen1Voltage = ValueMeter1Voltage.inputValue;
        Gen1Frequency = ValueMeter1Frequency.inputValue;

        IsolatorToggle = SynchroManage.isolatorSwitch;

        if (IsolatorToggle == true)
        {
            ValueMeter1Frequency.enabled = false;
            ValueMeter1Power.enabled = true;
            ValueMeter1Current.enabled = true;
        }
    }
    public void StartScene()
    {
        ValueMeter1Power.enabled = false;
        ValueMeter1Current.enabled = false;
    }
}
