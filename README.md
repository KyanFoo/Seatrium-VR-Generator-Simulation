# Table of contents
* [Title/Introduction](#introduction)
* [Project Background](#project-background)
* [Project Setup](#project-setup)
  * [Unity](#unity)
  * [Steam VR](#steamvr)
* [Scope of Functionalities](#scope-of-functionalities)
  * [Flip Knob](#flip-knob)
  * [Rotaty Knob](#rotaty-knob)
  * [Value Meter](#value-meter)
  * [Synchroscope Needle](#synchroscope-needle)
  * [Synchroscope Lamp](#synchroscope-lamp)
  * [Synchroscope Manager](#synchroscope-manager)
  * [Game Manager](#game-manager)
  * [Post Processing Manager](#post-processing-manager)



# Introduction
Welcome to the developer README for “Seatrium-VR-Training-Simulation”. This document serves as a comprehensive guide for developers looking to dive into the codebase of our simulation game. Here you will find all essential information and key insights to help you understand and contribute to the further development of “Seatrium-VR-Training-Simulation”.
# Project Background
This simulation serves as a safety training platform for operators, given the absence of real-life models for them to practice on. In the absence of physical counterparts, virtual reality becomes the optimal solution for simulating scenarios, enabling operators to assess their comprehension of situations and task management skills.

The primary goal of operators in this simulation game is to synchronize generators in parallel. Achieving synchronization ensures the operation of generators is both more stable and efficient. This, in turn, allows operators to meet power demands more effectively, reducing energy wastage and enhancing fuel efficiency.
# Project Setup
Here are the essential instructions for configuring the project to enable the use of SteamVR in Unity.
## Unity
Follow these steps to set up Project in Unity:
1.	Begin by creating a new 3D project using Editor Version "2021.3.11f1". • Note: While you may use other versions in the future, the Unity project is expected to remain functional without any risk of data loss.
2.	Import the "Unity Package" into the newly created Unity Project. • Upon importing the "Unity Package," you should not encounter any issues. However, if the materials appear in pink, it indicates that you may have imported the package into a URP (Universal Render Pipeline) Project. • To resolve this, refer to instructions on how to switch to the Unity Standard Render Pipeline.
## SteamVR
Follow these steps to set up SteamVR in Unity:
1.	Open the Unity Asset Store and log in to your account.
2.	Add the "SteamVR Plugin" and "XR Plugin Management" to your account by clicking the "Add to My Assets" button. To do this, go to: • Window > Package Manager > My Assets > SteamVR Plugin > Import • Window > Package Manager > My Assets > XR Plugin Management > Import
3.	After the importing loading screen is complete, accept the required conditions by clicking the "Accept All" button.
4.	If you intend to use VR, import XR Plugin Management: • Navigate to File > Build Settings > Player Settings > XR Plug-in Management > Open VR Loader
5.	Define actions for SteamVR Inputs: • Go to Window > SteamVR Input > Yes > Save and generate
6.	Now, you can fully utilize the play scenes and control your hands in SteamVR's "Interactions_Example." If you encounter any confusion in the steps mentioned, refer to this link for a more detailed explanation: How to Setup SteamVR For Unity 2020 | SteamVR Import Steps | Unity VR Tutorial.
# Scope of Functionalities
Below is an extensive summary of what operators should anticipate from the simulation. Only specific details regarding script aspects will be provided in this section.
## Flip Knob
The interactable asset is used by the operators to adjust the value of the value meters on the generator’s switchboard. Within this small knob, there is consist of several essential scripts that contribute to its functionality. These scripts include "Interactable," "Flip Circular Drive," and "Flip Knob Behaviour."

### Interactable Script:  
The operator will be unable to interact with it without this script. You can examine its code to see if their codes could help to develop the simulation further.

### Circular Drive Script:  
It enables the knobs to execute a full 360-degree circular motion, enabling operators to manipulate the value meter by twisting and turning the knobs. Within the Inspector, a "Unity Event" permits developers to incorporate functions that activate when the rotation reaches either its minimum or maximum position.

### Flip Knob Behaviour Script:  
This script is designed to manage interactive knobs. It defines rotational limitations and functions to verify a complete interaction with the knob, as well as logging messages as needed.

The following lines of code, executed at the start of the scene, instruct the "Flip Circular Drive" to engage limits, configuring them at -45 and 45 degrees, allowing the knobs on both sides to flip to a 45-degree angle.
```
void Start()
    {
        //Automatic notify the "Circular Drive" Script to enable limited.//
        flipcirculardrive.limited = true;

        //Set your Maximum & Minium Angle 
        flipcirculardrive.minAngle = -45.0f;
        flipcirculardrive.maxAngle = 45.0f;
    }
```
The following line of code are executed each time the flip knob has completely engaged with its minimum or maximum limit, leading to an increase or decrease in the value of the associated value meter. Initially, its purpose is to log a message, notifying the developer that a full interaction has occurred.
```
public void IncreaseValue()
    {
        //Retreive value from Main Script "CircularDrive".//
        linearvalue = flipcirculardrive.linearMapping.value;

        //Round up from float value to int.//
        intlinearvalue = (Mathf.RoundToInt(linearvalue));

        //Actions whether the player got properly interact the knob.//
        Debug.Log(intlinearvalue);
        Debug.Log("Increase");
    }
    public void DecreaseValue()
    {
        //Retreive value from Main Script "CircularDrive".//
        linearvalue = flipcirculardrive.linearMapping.value;

        //Round up from float value to int.//
        intlinearvalue = (Mathf.RoundToInt(linearvalue));

        //Actions whether the player got properly interact the knob.//
        Debug.Log(intlinearvalue);
        Debug.Log("Decrease");
    }
```
## Rotaty Knob
The Rotary Knob is used by the operators to select the generator that is linked to the synchroscope display. This affects not only the synchronization lamps but also the needle. The rotary knob has its own script and can be supported by external elements.

### Interactable Script:  
The operator will be unable to interact with it without this script. You can examine its code to see if their codes could help to develop the simulation further.

### Circular Drive Script:  
It enables the knobs to execute a full 360-degree circular motion, enabling operators to manipulate the value meter by twisting and turning the knobs. Within the Inspector, a "Unity Event" permits developers to incorporate functions that activate when the rotation reaches either its minimum or maximum position.

### Rotary Knob Behaviour Script:  
This script is designed to managing the behaviour of interactive rotary knobs by adjusting the number of intervals at the start of the scene and snapping to the nearest rotation when the knob is turned by the operator.

The following lines of code that generate an array of rotation values based on intervals set by the developer in the Inspector at the start of the scene.
With "4" interval points, for example, the angles would be 90, 180, 270, and 360.
```
void Start()
    {
        //Creating the interval points of the knobs//
        //If there are "4" interval points, the angles of the points are 90, 180, 270, 360//
        myArray = new int[intervalPoint + 1];
        num = 360 / intervalPoint;
        //Within the array, the first value would be "0"//
        myArray[0] = 0;
        for (int i = 0; i < intervalPoint; i++)
        {
            //Create an Array to check how many points there are and what is the rotation angle//
            myArray[i + 1] = (num * (i + 1));
        }

        //Checking on how many variables are there in the gameobject//
        numberOfVariables = myArray.Length;
        numberPlace = 0;
    }
```
The following line of code continuously monitor the rotary knob's current rotation, analysing its upper and lower values. When the operator releases the handle of the knob, the script chooses the upper or lower value, snapping to the position closest to the current rotation.
```
void Update()
    {
        //Retreive data of the gameobject Z Rotation//
        currentZRotation = transform.rotation.eulerAngles.z;

        foreach (int value in myArray)
        {
            if (value <= currentZRotation)
            {
                lowerValue = value;
            }
            if (value >= currentZRotation)
            {
                upperValue = value;
                break;
            }
        }
        float lowerDifference = Mathf.Abs(currentZRotation - lowerValue);
        float upperDifference = Mathf.Abs(upperValue - currentZRotation);

        if (lowerDifference < upperDifference)
        {
            //Debug.Log("The closest value is: " + lowerValue);
            numberPlace = (int)lowerValue;
            targetRotation = numberPlace;
            currentRotationX = transform.rotation.eulerAngles.x;
            currentRotationY = transform.rotation.eulerAngles.y;
            SnapRotate();
        }
        else
        {
            //Debug.Log("The closest value is: " + upperValue);
            numberPlace = (int)upperValue;
            targetRotation = numberPlace;
            currentRotationX = transform.rotation.eulerAngles.x;
            currentRotationY = transform.rotation.eulerAngles.y;
            SnapRotate();
        }

        for (int i = 0; i < myArray.Length; i++)
        {
            if (myArray[i] == numberPlace)
            {
                numberPlace = i;
            }
        }
    }

void SnapRotate()
    {
        //Snap the GameObject to the rotation position of the "targetRotaion"//
        transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, targetRotation);
    }
```
## Value Meter
Operators utilize value meters to evaluate the values of various energy types generated by the vessel's engine. The operators are in charge of inspecting the value meters on both incoming and upcoming generators.

### Value Meter Script:  
The code for the value meters is highly adaptable, allowing for modifications to the range of values displayed on the analogue panel. The flip value, which has an effect on the value meter, can also be altered. Despite using the same script, it can be applied to a variety of value meter units.
```
//Represent the Maximum and Minimum of the GameObject rotation.//
    //Write in Maximum & Minimum value of the GameObject rotation.//
    //Manually rotate the "Needle_Pivot" to the Maximum and Minimum point of the meters' image.//
    [Header("Value Meter Settings")]
    public float minValueMeter;
    public float maxValueMeter;

    //Represent the minimum and maximum values of the "ValueMeter".//
    //Write in Maximum & Minimum value of the "ValueMeter"'s image canvas.//
    //Current(A) = 0 - 1000, Power(Kw) = 300 - 600.//
    //Frequency(Hz) = 55 - 65, Voltage(V) 0 - 600.//
    [Header("Game Object Settings")]
    public float minValue;
    public float maxValue;

public void SetPointerRotation(float value)
    {
        //Ensure that the value is within range.//
        value = Mathf.Clamp(value, minValueMeter, maxValueMeter);

        //Calculate the rotation of the pivot based on the "ValueMeter"'s value.//
        float normalizedValue = (value - minValueMeter) / (maxValueMeter - minValueMeter);
        float targetRotation = Mathf.Lerp(minValue, maxValue, normalizedValue);

        // Smoothly interpolate rotation when inputValue changes.//
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * lerpSpeed);

        // Rotate the needle of the valuemeter to targeted rotation.//
        pivotPoint.localRotation = Quaternion.Euler(0f, 0f, currentRotation);
    }
```
Included below are functions for increasing and decreasing to manipulate the value meters. Criteria have been established to determine the conditions under which specific energy meters can or cannot be adjusted during different phases of the game.
```
public void FlipIncrease()
    {
        //Function called to, first check whether Generator and the ValueMeter is on the same setting.//
        //Frequency ValueMeter is in Setting 1 & Power and Current ValueMeter is in Setting 2.//
        //They must check if the Generators is on the same setting so that they can increase its respective ValueMeter's output.//
        if (areYouSetting1 == true && nowSetting1Toggle == true || areYouSetting2 == true && nowSetting2Toggle == true)
        {
            //Check whether the ValueMeter is a Frequency ValueMeter in Generator 2.//
            if (areYouFrequency2 == true)
            {
                //If Yes, allow to call a function in Synchroscope Manager.//
                SynchroscopeManager.FlipIncrease();
            }
            inputValue = inputValue + flipValue;
        }
    }
    public void FlipDecrease()
    {
        //Function called to, first check whether Generator and the ValueMeter is on the same setting.//
        //Frequency ValueMeter is in Setting 1 & Power and Current ValueMeter is in Setting 2.//
        //They must check if the Generators is on the same setting so that they can decrease its respective ValueMeter's output.//
        if (areYouSetting1 == true && nowSetting1Toggle == true || areYouSetting2 == true && nowSetting2Toggle == true)
        {
            //Check whether the ValueMeter is a Frequency ValueMeter in Generator 2.//
            if (areYouFrequency2 == true)
            {
                //If Yes, allow to call a function in Synchroscope Manager.//
                SynchroscopeManager.FlipDecrease();
            }
            inputValue = inputValue - flipValue;
        }
    }
}
```
## Synchroscope Needle
This instrument, which is similar to the 2 Bright 1 Dark Method, is used for generator synchronization by operators using the analogue synchroscope method. The needle pointer denotes the required adjustment direction and speed for achieving generator synchronization.

### Synchroscope Needle Script:  
The provided code illustrates the rotation of the synchroscope needle, which can turn clockwise or counterclockwise depending on the frequency of the generator.
```
if (startSwitch == true)
        {
            //Rotate the Synchroscope Needle Clockwise.//
            //The reason why the supposed rotation is clockwise is -360 is because our game scene, everything is in reverse.//
            rotation = -360.0f;
            StartCoroutine(NeedleCoroutine());
        }
        if (startSwitch == true && reverseSwitch == true)
        {
            //Rotate the Synchroscope Needle Anti-Clockwise.//
            //The reason why the supposed rotation is Anti-clockwise is 360 is because our game scene, everything is in reverse.//
            rotation = 360.0f;
            StartCoroutine(NeedleCoroutine());

            //Find its current transform rotation.//
            startRotation = needle.rotation.eulerAngles;
            lerpTime = 0;
        }
        //Apply new rotation.//
        endRotation = startRotation + new Vector3(0, 0, rotation);
```
An additional touch has been added: when the isolator switch closes the circuit and the needle approaches the 12 o'clock position, it smoothly returns to the 12 o'clock position.
```
if (pauseSwitch == true)
        {
            currentRotation = needle.rotation.eulerAngles;
            if (currentRotation.z > 180)
            {
                //Constantly update to check if needle GameObject had past the 180 rotation.//
                //If it had when past already, it will rotate forward to the center.//
                currentRotationZ = currentRotation.z;
                centerRotation.z = 360;
                currentRotationZ = currentRotation.z - 360;
            }
            else
            {
                //Constantly update to check if needle GameObject had past the 180 rotation.//
                //If it haven't past, it will rotate back its direction to the center.//
                currentRotationZ = currentRotation.z;
                centerRotation.z = 0;
            }

            //Constantly update to check if variable value is within the deviation.//
            if (currentRotationZ >= -25 && currentRotationZ <= 25)
            {
                Debug.Log("Within 20");
                lerpTime = 0;
                PhaseSeqMatch = true;
                gameManager.ErrorChecker();
                StartCoroutine(CenterNeedleCoroutine());
            }
            else
            {
                PhaseSeqMatch = false;
                //Breaker tripping actions will be carried out in this function.
                gameManager.ErrorChecker();
                tutorialUI.SetActive(false);
            }
        }

IEnumerator CenterNeedleCoroutine()
    {
        Debug.Log("Center Needle");
        lerpTime += Time.deltaTime;
        needle.rotation = Quaternion.Euler(Vector3.Lerp(currentRotation, centerRotation, lerpTime / 1));
        yield return null;
    }
```
## Synchroscope Lamp
This instrument, which is similar to the analogue synchroscope method, is used for generator synchronization by operators using the 2 Bright 1 Dark Method. This is indicated by identifying the rotation direction of the generator's field, which is typically represented by three bulbs.

### Synchroscope Lamp Script:  
The provided code illustrates how to properly fade in and out the lamp. Before directing the lamp to fade in, the script must first activate the material's emissive property. Following functions govern the fading in and out of synchroscope bulbs, with their initiation controlled by the "Synchroscope Manager." 
```
void Start()
    {
        //Apply Renderer on GameObject.//
        _renderCube = objCube.GetComponent<Renderer>();

        //Apply Emissive Material onto GameObject.//
        _renderCube.material = emissiveMaterial;

        //Enable the Emissive Map and set the Color and Intensity of the Emissive Material.//
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        lerpTime = 0f;
    }
```
The functions stated below assist the synchroscope lamp material transition smoothly between fading in and out states. Boolean checks are performed prior to executing the function, and as the fade-in duration approaches completion, an automatic fade-out occurs; the process operates vice versa.
```
IEnumerator OnCoroutine()
    {
        //Bool to check to tell the script to "ON" the lamp.//
        while (onLight == true)
        {
            //"PauseSwitch" is used to check whether the player has switch "ON" the Isolator.//
            //If switched, the fading in of "EmissiveMaterial" will stop and it will not affect the entire game.//
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "EmissiveMaterial".//
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        //Debug.Log("Finish Lighting Up");
    }
    private void StopOnCoroutineAndExitLoop()
    {
        //When the GameObject is fading "IN" is almost complete it stop the fade "IN" function and call a function to fade "OUT".//
        onLight = false;
        StopCoroutine(OnCoroutine());
        CallOffCoroutine();
    }
    IEnumerator OffCoroutine()
    {
        //Bool to check to tell the script to "OFF" the lamp.//
        while (offLight == true)
        {
            //"PauseSwitch" is used to check whether the player has switch "ON" the Isolator.//
            //If switched, the fading in of "EmissiveMaterial" will stop and it will not affect the entire game.//
            if (pauseSwitch == false)
            {
                lerpTime += Time.deltaTime;
            }
            //Enable Emission is "EmissiveMaterial".
            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(endIntensity, startIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        //Debug.Log("Finish Lighting Down");
    }
    private void StopOffCoroutineAndExitLoop()
    {
        //When the GameObject is fading "OUT" is almost complete it stop the fade "OUT" function.
        offLight = false;
        StopCoroutine(OffCoroutine());
    }
```
## Synchroscope Manager
The provided code is essential for the simulation's functionality, as it manages everything related to the synchroscope. It specifies when functions should be called and stores variables from relevant scripts.

### Synchroscope Manager Script:  
The provided lines of code allow the interactive knobs of the "Governor Switch" to change the duration of the lerp, allowing the "Synchroscope Needle" to move faster or slower.
```
public void FlipIncrease()
    {
        //Function is called to, when the Governor Knob has been flipped.//
        //It would increase the speed of the Lamps fade "In" and "Out" sequence and Synchroscope Needle.//
        lerpDuration = lerpDuration + 0.1f;
    }
    public void FlipDecrease()
    {
        //Function is called to, when the Governor Knob has been flipped.//
        //It would decrease the speed of the Lamps fade "In" and "Out" sequence and Synchroscope Needle.//
        lerpDuration = lerpDuration - 0.1f;
    }
```
While the previous script allows the synchroscope lamps to fade in and out in a single cycle, only the synchroscope manager allows the lamps to fade in after evaluating certain Booleans related to rotation cycles. It also evaluates a specific Boolean to determine whether the lamps should rotate clockwise or counterclockwise motion.  
```
public void ActiveSync()
    {
        //Function is called to, call other fucntion to allow the lamps to fade "In" & "Out" in a speific order.//
        //It also check whether the lamps' speific order has to be Clockwise or Anti-Clockwise.//

        //Clockwise Motion.//
        if (reverseLoop == false)
        {
            if (!nextLamp0)
            {
                CallLamp0Script();
            }
            if (nextLamp0 == true && !nextLamp1 && lamp0Script.onLight == false)
            {
                CallLamp1Script();
            }
            if (nextLamp0 == true && nextLamp1 == true && !nextLamp2 && lamp1Script.onLight == false)
            {
                CallLamp2Script();
            }
            if (nextLamp0 == true && nextLamp1 == true && nextLamp2 == true && lamp2Script.onLight == false)
            {
                ResetBool();
            }
        }
        //Anti-Clockwise Motion.//
        if (reverseLoop == true)
        {
            if (!nextLamp0 && reverseLoop == true)
            {
                CallLamp0Script();
            }
            if (nextLamp0 == true && !nextLamp2 && lamp0Script.onLight == false && reverseLoop == true)
            {
                CallLamp2Script();
            }
            if (nextLamp0 == true && nextLamp2 == true && !nextLamp1 && lamp2Script.onLight == false && reverseLoop == true)
            {
                CallLamp1Script();
            }
            if (nextLamp0 == true && nextLamp1 == true && nextLamp2 == true && lamp1Script.onLight == false && reverseLoop == true)
            {
                ResetBool();
            }
        }
    }
    //Functions below are called to, call their lamp function to fade "In" & also Bool is check "True".
    //It is to tell the code that it has been fade "In" and await for the next lamp's turn.
    public void CallLamp0Script()
    {
        lamp0Script.CallOnCoroutine();
        nextLamp0 = true;
    }
    public void CallLamp1Script()
    {
        lamp1Script.CallOnCoroutine();
        nextLamp1 = true;
    }
    public void CallLamp2Script()
    {
        lamp2Script.CallOnCoroutine();
        nextLamp2 = true;
    }
    public void ResetBool()
    {
        //Function is called to, after a cycle has been complete, reset is needed to repear the process again.//
        nextLamp0 = false;
        nextLamp1 = false;
        nextLamp2 = false;
    }
```
## Game Manager
The "Game Manager" has complete control over the simulation's game flow. It is responsible for tasks such as loading different module scenes, coordinating restarts, and configuring preset variables within the scene.

### Game Manager Script:  
The functions listed below define the preset values used by the value meters when accessing various modules.
```
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
```
Preset function that are used for different modules.
```
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
        randomizeVariable = Random.Range(57, 63);
        //Debug.Log(randomizeVariable + " Frequency");
        ValueMeter1Frequency.inputValue = randomizeVariable;

        randomizeVariable = Random.Range(50, 600);
        //Debug.Log(randomizeVariable + " Voltage");
        ValueMeter1Voltage.inputValue = randomizeVariable;

        randomizeVariable = Random.Range(350, 550);
        //Debug.Log(randomizeVariable + " Power");
        ValueMeter1Power.inputValue = randomizeVariable;

        randomizeVariable = Random.Range(100, 600);
        //Debug.Log(randomizeVariable + " Current");
        ValueMeter1Current.inputValue = randomizeVariable;
    }
```
To finish the simulation, the Game Manager evaluates predefined criteria to see if they have been met, determining the operator's success or failure. It should be noted that different modules have different criteria that must be met.
```
void Update()
    {
        if (IsolatorToggle == true)
        {
            if (isStartSynchroscopeScene == true)
            {
                RequirementCheck();
            }
        }
        if (Gen1Power == Gen2Power || Gen2Power >= Gen1Power - 5 && Gen2Power <= Gen1Power + 5)
        {
            RequirementCheck();
        }
    }
public void RequirementCheck()
    {
        if (PhaseSeqMatch == true && FrequencyMatch == true && VoltageMatch == true)
        {
            if(isStartSynchroscopeScene == true || isStartLoadSharingScene == true)
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
public void RequirementCheck()
    {
        if (PhaseSeqMatch == true && FrequencyMatch == true && VoltageMatch == true)
        {
            if(isStartSynchroscopeScene == true || isStartLoadSharingScene == true)
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
```
Upon operators engaging with the isolator switch to initiate load sharing, the "Game Manager" triggers a function to alter the management of the frequency value to the power and current values of the "Governor Switch."
```
if (IsolatorToggle == true || VoltageMatch == true && FrequencyMatch == true)
        {
            //Debug.Log("Setting2");
            GovernorSwitchSetting2();
        }
        else
        {
            //Debug.Log("Setting1");
            GovernorSwitchSetting1();
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
```
## Post Processing Manager
This script is solely utilized for scene post-processing, allowing the emissive material to bloom and radiate with the intensity of a light source. This improves the simulation's realism and immersion.
Follow thse steps to setup Post Processing in Unity:
1. Install "Post Processing" into your unity project. To do this, go to: WIndow > Package Manager > Post Processing > Install
2. Create "Empty" and add "Post-process Volume" component.
3. Click on "Is Global" and Create a new profile on the scene you want to add post processing.
4. Click onto your camera add "Post-process Layer" component.
5. Drag your camera into the "Trigger".
6. Rememebr to select "USe Graphics Settings" in HDR and MSAA.
View this YouTube video for a clearer grasp of configuring post-processing: "How to add post processing in unity 3d projects"

## Canvas pointer scripts
These scripts play a crucial role in generating the pointer that learners see while interacting with the canvas. This feature is a vital aspect of UI interaction, eliminating the need for learners to physically move to a button for interaction. The pointer enables learners to seamlessly interact with the canvas without having to move away from the generator controls.

## VR Input Module
This script replaces the steamVR input module with the same input controls however, it helps to add on an additional input which is the canvas pointer. This script should cause not issues and there is nothing be be adjusted inside. Make sure that the inspector contains the canvas pointer camera. The target source refers to which hand the pointer will appear on. The click actions is the action to interact with the canvas. 

## Pointer script
This scripts creates the raycast and the functions behind the canvas pointer. There are three variables in the inspector to take note of. Default length which is the length in which the raycast will reach until it detects a collider. The dot is a reference to the gameobject of a dot that indicates to the user what they are pointer towards. The Input module reference will be the VR input module. This scripts help to detect if there is a collider with a tag "canvas" on it and if it does, it will draw the line renderer. Once it hits colliders without a tag, the line renderer will disable. 

## Button Transitioner


