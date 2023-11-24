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

Interactable Script:
The operator will be unable to interact with it without this script. You can examine its code to see if their codes could help to develop the simulation further.

Circular Drive Script:
It enables the knobs to execute a full 360-degree circular motion, enabling operators to manipulate the value meter by twisting and turning the knobs. Within the Inspector, a "Unity Event" permits developers to incorporate functions that activate when the rotation reaches either its minimum or maximum position.

Flip Knob Behaviour Script:
This script is designed to manage interactive knobs. It defines rotational limitations and functions to verify a complete interaction with the knob, as well as logging messages as needed.

The following lines of code, executed at the start of the scene, instruct the "Flip Circular Drive" to engage limits, configuring them at -45 and 45 degrees, allowing the knobs on both sides to flip to a 45-degree angle.
## Rotaty Knob
The Rotary Knob is used by the operators to select the generator that is linked to the synchroscope display. This affects not only the synchronization lamps but also the needle. The rotary knob has its own script and can be supported by external elements.

Interactable Script:
The operator will be unable to interact with it without this script. You can examine its code to see if their codes could help to develop the simulation further.

Circular Drive Script:
It enables the knobs to execute a full 360-degree circular motion, enabling operators to manipulate the value meter by twisting and turning the knobs. Within the Inspector, a "Unity Event" permits developers to incorporate functions that activate when the rotation reaches either its minimum or maximum position.

Rotary Knob Behaviour Script:
This script is designed to managing the behaviour of interactive rotary knobs by adjusting the number of intervals at the start of the scene and snapping to the nearest rotation when the knob is turned by the operator.

The following lines of code that generate an array of rotation values based on intervals set by the developer in the Inspector at the start of the scene.
With "4" interval points, for example, the angles would be 90, 180, 270, and 360.

The following line of code continuously monitor the rotary knob's current rotation, analysing its upper and lower values. When the operator releases the handle of the knob, the script chooses the upper or lower value, snapping to the position closest to the current rotation.
## Value Meter
Operators utilize value meters to evaluate the values of various energy types generated by the vessel's engine. The operators are in charge of inspecting the value meters on both incoming and upcoming generators.

Value Meter Script:
The code for the value meters is highly adaptable, allowing for modifications to the range of values displayed on the analogue panel. The flip value, which has an effect on the value meter, can also be altered. Despite using the same script, it can be applied to a variety of value meter units.

Included below are functions for increasing and decreasing to manipulate the value meters. Criteria have been established to determine the conditions under which specific energy meters can or cannot be adjusted during different phases of the game.
## Synchroscope Needle
This instrument, which is similar to the 2 Bright 1 Dark Method, is used for generator synchronization by operators using the analogue synchroscope method. The needle pointer denotes the required adjustment direction and speed for achieving generator synchronization.

Synchroscope Needle Script:
The provided code illustrates the rotation of the synchroscope needle, which can turn clockwise or counterclockwise depending on the frequency of the generator. An additional touch has been added: when the isolator switch closes the circuit and the needle approaches the 12 o'clock position, it smoothly returns to the 12 o'clock position.
## Synchroscope Lamp
This instrument, which is similar to the analogue synchroscope method, is used for generator synchronization by operators using the 2 Bright 1 Dark Method. This is indicated by identifying the rotation direction of the generator's field, which is typically represented by three bulbs.

Synchroscope Lamp Script:
The provided code illustrates how to properly fade in and out the lamp. Before directing the lamp to fade in, the script must first activate the material's emissive property. Following functions govern the fading in and out of synchroscope bulbs, with their initiation controlled by the "Synchroscope Manager." 

Before the function can be executed, Boolean checks must be completed, and as the fade-in duration approaches completion, the automatic fade-out occurs; the process operates vice versa.
## Synchroscope Manager
The provided code is essential for the simulation's functionality, as it manages everything related to the synchroscope. It specifies when functions should be called and stores variables from relevant scripts. While the previous script allows the synchroscope lamps to fade in and out in a single cycle, only the game manager allows the lamps to fade in after evaluating certain Booleans related to rotation cycles. It also evaluates a specific Boolean to determine whether the lamps should rotate clockwise or counterclockwise motion. 

Synchroscope Manager Script:
The provided lines of code allow the interactive knobs of the "Governor Switch" to change the duration of the lerp, allowing the "Synchroscope Needle" to move faster or slower.
## Game Manager
The "Game Manager" has complete control over the simulation's game flow. It is responsible for tasks such as loading different module scenes, coordinating restarts, and configuring preset variables within the scene.

Game Manager Script:
The functions listed below define the preset values used by the value meters when accessing various modules. To finish the simulation, the Game Manager evaluates predefined criteria to see if they have been met, determining the operator's success or failure. It should be noted that different modules have different criteria that must be met.
## Post Processing Manager
This script is solely utilized for scene post-processing, allowing the emissive material to bloom and radiate with the intensity of a light source. This improves the simulation's realism and immersion.
