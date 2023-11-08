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

    [Header("Generator 2 Variables")]
    public float Gen2Power;
    public float Gen2Current;
    public float Gen2Voltage;
    public float Gen2Frequency;
    public ValueMeter ValueMeter2Power;
    public ValueMeter ValueMeter2Current;
    public ValueMeter ValueMeter2Voltage;
    public ValueMeter ValueMeter2Frequency;

    [Header("Others Variables")]
    public SynchroscopeManager SynchroManage;
    public bool IsolatorToggle;
    public int checker = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("RunningGeneratorPrset", 1f);
        Invoke("GovernorSwitchSetting1", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Gen1Power = ValueMeter1Power.inputValue;
        Gen1Current = ValueMeter1Current.inputValue;
        Gen1Voltage = ValueMeter1Voltage.inputValue;
        Gen1Frequency = ValueMeter1Frequency.inputValue;

        Gen2Power = ValueMeter2Power.inputValue;
        Gen2Current = ValueMeter2Current.inputValue;
        Gen2Voltage = ValueMeter2Voltage.inputValue;
        Gen2Frequency = ValueMeter2Frequency.inputValue;

        IsolatorToggle = SynchroManage.isolatorSwitch;

        if (IsolatorToggle == true && checker == 0)
        {
            GovernorSwitchSetting2();
            //ValueMeter1Power.enabled = true;
            //ValueMeter1Current.enabled = true;
            //ValueMeter1Frequency.enabled = false;
            //checker = 1;
        }
        if (IsolatorToggle == false && checker == 1)
        {
            GovernorSwitchSetting1();
            //ValueMeter1Power.enabled = false;
            //ValueMeter1Current.enabled = false;
            //ValueMeter1Frequency.enabled = true;
            //checker = 0;
        }
    }
    public void RunningGeneratorPrset()
    {
        ValueMeter1Power.inputValue = 350f;
        ValueMeter1Current.inputValue = 400f;
        ValueMeter1Voltage.inputValue = 500f;
        ValueMeter1Frequency.inputValue = 57f;
    }
    public void GovernorSwitchSetting1()
    {
        ValueMeter1Power.enabled = false;
        ValueMeter1Current.enabled = false;
        ValueMeter1Frequency.enabled = true;

        ValueMeter2Power.enabled = false;
        ValueMeter2Current.enabled = false;
        ValueMeter2Frequency.enabled = true;

        checker = 0;
    }
    public void GovernorSwitchSetting2()
    {
        ValueMeter1Power.enabled = true;
        ValueMeter1Current.enabled = true;
        ValueMeter1Frequency.enabled = false;

        ValueMeter2Power.enabled = true;
        ValueMeter2Current.enabled = true;
        ValueMeter2Frequency.enabled = false;

        checker = 1;
    }
    public void AutoVoltage()
    {
        ValueMeter2Voltage.inputValue = ValueMeter1Voltage.inputValue;
    }
}
