using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Generator 1 Variables")]
    //Represent the variable values of the Generator 1's Switchboard.//
    public float Gen1Power;
    public float Gen1Current;
    public float Gen1Voltage;
    public float Gen1Frequency;
    //Drag & Drop the ValueMeter's script to the respective slots.//
    public ValueMeter ValueMeter1Power;
    public ValueMeter ValueMeter1Current;
    public ValueMeter ValueMeter1Voltage;
    public ValueMeter ValueMeter1Frequency;

    [Header("Generator 2 Variables")]
    //Represent the variable values of the Generator 2's Switchboard.//
    public float Gen2Power;
    public float Gen2Current;
    public float Gen2Voltage;
    public float Gen2Frequency;
    //Drag & Drop the ValueMeter's script to the respective slots.//
    public ValueMeter ValueMeter2Power;
    public ValueMeter ValueMeter2Current;
    public ValueMeter ValueMeter2Voltage;
    public ValueMeter ValueMeter2Frequency;

    [Header("Secondary Script Variables")]
    //Represent the "Secondary Script" to access variables.//
    public SynchroscopeManager SynchroManage;
    public SynchroNeedle SynchroNeedle;
    public RotaryKnobBehaviour RotaryKnob;

    //Represent the Bools to check whether the Generator 1 & 2 are switched "ON" or "OFF".//
    public bool IsolatorToggle;

    //Represent the Bools to check whether the Generator 1 or 2 is at which setting.//
    public bool setting1;
    public bool setting2;

    [Header("Generator Toggle On/Off")]
    public bool Generator1Toggle;
    public bool Generator2Toggle;

    public bool FrequencyMatch;
    public bool VoltageMatch;
    public bool PhaseSeqMatch;

    //Check if all variable in the generator is in sync in order to show synchronised
    public GameObject synchronisedPage;
    public AudioSource syncedSound;
    public AudioClip passedSound;
    public GameObject mainMenu;
    public GameObject modulePassed;

    private int randomizeVariable;

    public GameObject passFreq;
    public GameObject passVolt;
    public GameObject passPhase;

    public bool isPracticeScene;
    [HideInInspector]
    public bool isStartSynchroscopeScene;
    [HideInInspector]
    public bool isStartLoadSharingScene;
    // Start is called before the first frame update
    void Start()
    {
        //Auto switch "On" the Generator 1.//
        //Generator1SwitchOn();

        //Preset Generator 1's variable values since it is also a "Running Generator".//
        //Invoke("RunningGeneratorPrset", 1f);

        if (isPracticeScene == true)
        {
            StartPracticeScene();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            Debug.Log("Training");
            StartTrainingScene();
        }
        if (Input.GetKeyDown("2"))
        {
            Debug.Log("Practice");
            StartPracticeScene();
        }
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("Synchroscope");
            StartSynchroscopeScene();
        }
        if (Input.GetKeyDown("4"))
        {
            Debug.Log("Load Sharing");
            StartLoadSharingScene();
        }

        if (Input.GetKeyDown("5"))
        {
            Restart();
        }
        //Constantly update to check variables value of ValueMeters in Generator 1.//
        Gen1Power = ValueMeter1Power.inputValue;
        Gen1Current = ValueMeter1Current.inputValue;
        Gen1Voltage = ValueMeter1Voltage.inputValue;
        Gen1Frequency = ValueMeter1Frequency.inputValue;

        //Constantly update to check variables value of ValueMeters in Generator 2.//
        Gen2Power = ValueMeter2Power.inputValue;
        Gen2Current = ValueMeter2Current.inputValue;
        Gen2Voltage = ValueMeter2Voltage.inputValue;
        Gen2Frequency = ValueMeter2Frequency.inputValue;

        //Check to see if the Isolator Switch has been switched "ON" or "OFF".//
        IsolatorToggle = SynchroManage.isolatorSwitch;

        PhaseSeqMatch = SynchroNeedle.PhaseSeqMatch;
        //Since the Governor Switch is quite speical.//
        //In the first phase, It can regulate the frequency output.//
        //However, after switching "ON" the isolator switch, it will regulate the output of Power and Current output.//
        if (IsolatorToggle == true)
        {
            //Debug.Log("Setting2");
            GovernorSwitchSetting2();
        }
        else
        {
            //Debug.Log("Setting1");
            GovernorSwitchSetting1();
        }

        if (IsolatorToggle == true)
        {
            if (isStartSynchroscopeScene == true)
            {
                RequirementCheck();
            }
            if (Gen1Power == Gen2Power)
            {
                RequirementCheck();
            }
            if (Gen2Power >= Gen1Power - 5 && Gen2Power <= Gen1Power + 5)
            {
                RequirementCheck();
            }
        }
    }
    public void Generator1SwitchOn()
    {
        //Function is called to, "ON" the Generator 1.//
        Generator1Toggle = true;
    }
    public void Generator2SwitchOn()
    {
        //Function is called to, "ON" the Generator 2.//
        Generator2Toggle = true;
    }
    public void TrainingRunningGeneratorPreset()
    {
        //Function called to, preset ValueMeter's variables of Generator 1 also known as "Running Generators".//
        ValueMeter1Power.inputValue = 350f;
        ValueMeter1Current.inputValue = 400f;
        ValueMeter1Voltage.inputValue = 500f;
        ValueMeter1Frequency.inputValue = 57f;
    }
    public void PracticeRunningGeneratorPreset()
    {
        randomizeVariable = Random.Range(57, 65);
        //Debug.Log(randomizeVariable + " Frequency");
        ValueMeter1Frequency.inputValue = randomizeVariable;

        randomizeVariable = Random.Range(50, 600);
        //Debug.Log(randomizeVariable + " Voltage");
        ValueMeter1Voltage.inputValue = randomizeVariable;

        randomizeVariable = Random.Range(300, 600);
        //Debug.Log(randomizeVariable + " Power");
        ValueMeter1Power.inputValue = randomizeVariable;

        randomizeVariable = Random.Range(0, 1000);
        //Debug.Log(randomizeVariable + " Current");
        ValueMeter1Current.inputValue = randomizeVariable;
    }
    public void AutoVoltage()
    {
        //Function called to, allowing the Incoming Generator to automatically set its voltage to be same as Running Generator.//
        ValueMeter2Voltage.inputValue = ValueMeter1Voltage.inputValue;
        VoltageMatch = true;
    }
    public void AutoFrequency()
    {
        //Function called to, allowing the Incoming Generator to automatically set its voltage to be same as Running Generator.//
        ValueMeter2Frequency.inputValue = ValueMeter1Frequency.inputValue;
    }
    public void AutoSynchroscope()
    {
        RotaryKnob.LoadSharingRotate();
    }
    public void GovernorSwitchSetting1()
    {
        //Function called to, Generator is on Setting 1, allowing the operaters to adjust the value of Frequency.//
        setting1 = true;
        setting2 = false;
    }
    public void GovernorSwitchSetting2()
    {
        //Function called to, Generator is on Setting 1, allowing the operaters to adjust the value of Power & Current.//
        setting1 = false;
        setting2 = true;
    }
    public void StartTrainingScene()
    {
        //Auto switch "On" the Generator 1.//
        Generator1SwitchOn();

        mainMenu.SetActive(false);

        //Preset Generator 1's variable values since it is also a "Running Generator".//
        Invoke("TrainingRunningGeneratorPreset", 1f);
    }
    public void StartPracticeScene()
    {

        //Auto switch "On" the Generator 1.//
        Generator1SwitchOn();

        mainMenu.SetActive(false);

        //Preset Generator 1's variable values since it is also a "Running Generator".//
        Invoke("PracticeRunningGeneratorPreset", 1f);
    }
    public void StartSynchroscopeScene()
    {
        isStartSynchroscopeScene = true;
        //Auto switch "On" the Generator 1.//
        Generator1SwitchOn();

        mainMenu.SetActive(false);

        //Preset Generator 1's variable values since it is also a "Running Generator".//
        Invoke("PracticeRunningGeneratorPreset", 1f);

        Generator2SwitchOn();

        Invoke("AutoVoltage", 1f);
        Invoke("AutoFrequency", 1f);
    }
    public void StartLoadSharingScene()
    {
        isStartLoadSharingScene = true;
        //Auto switch "On" the Generator 1.//
        Generator1SwitchOn();

        mainMenu.SetActive(false);

        //Preset Generator 1's variable values since it is also a "Running Generator".//
        Invoke("PracticeRunningGeneratorPreset", 1f);

        Generator2SwitchOn();

        Invoke("AutoVoltage", 1f);
        Invoke("AutoFrequency", 1f);
        Invoke("AutoSynchroscope", 1f);
    }

        public void QuitScene()
    {
        //Function is called, to quit the scene.//
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void RequirementCheck()
    {
        if (PhaseSeqMatch == true && FrequencyMatch == true && VoltageMatch == true)
        {
            if(isStartSynchroscopeScene == true)
            {
                modulePassed.SetActive(true);
            }
            else
            {
                synchronisedPage.SetActive(true);
            }
            syncedSound.PlayOneShot(passedSound);
        }   


    }

    public void ErrorChecker()
    {
        //Debug.Log("Isolator has been switched");
        if (Gen1Frequency == Gen2Frequency)
        {
            //Debug.Log("Pass");
            FrequencyMatch = true;
            passFreq.SetActive(true);
        }
        else
        {
            //Debug.Log("Fail");
            FrequencyMatch = false;
            BreakerTrip();
        }

        if (Gen1Voltage == Gen2Voltage)
        {
            VoltageMatch = true;
            passVolt.SetActive(true);
        }
        else
        {
            VoltageMatch = false;
            BreakerTrip();
        }
        
        if (!SynchroNeedle.PhaseSeqMatch)
        {
            BreakerTrip();
        }
        else
        {
            passPhase.SetActive(true);
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BreakerTrip()
    {
        SynchroNeedle.blackOut.SetActive(true);
        SynchroNeedle.failedSync.SetActive(true);
        SynchroNeedle.light1.SetActive(false);
        SynchroNeedle.light2.SetActive(false);
        SynchroNeedle.light3.SetActive(false);
        SynchroNeedle.light4.SetActive(false);
    }

    public void Restart()
    {
        if (isStartSynchroscopeScene)
        {
            StartSynchroscopeScene();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

