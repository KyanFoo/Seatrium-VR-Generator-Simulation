# Table of contents
* [Title/Introduction](#Seatrium-VR-Training-OPG-(Synchronization-of-Generators))
* [Project Background](#project-background)
* [Setup Project](#project-setup)
  * [Unity](#unity)
  * [Steam VR](#steam-vr)
* [Scope of Functionalities](#scope-of-functionalities)
  * [Flip Knobs](#flip-knobs)
  * [Rotary Knobs](#rotary-knobs)
  * [Value Meters](#value-meters)
  * [Synchroscope Needle](#synchroscope-needle)
  * [Synchroscope Lamps](#synchroscope-lamps)
  * [Synchroscope Manager](#synchroscope-manager)
  * [Game Manager](#game-manager)
  * [Post Processing Manager](#post-processing-manager)







# Seatrium-VR-Training-OPG (Synchronization of Generators)
Welcome to the developer README for “Seatrium-VR-Training-Simulation”. This document serves as a comprehensive guide for developers looking to dive into the codebase of our simulation game. Here you will find all essential information and key insights to help you understand and contribute to the further development of “Seatrium-VR-Training-Simulation”.

# Project Background
This simulation serves as a safety training platform for operators, given the absence of real-life models for them to practice on. In the absence of physical counterparts, virtual reality becomes the optimal solution for simulating scenarios, enabling operators to assess their comprehension of situations and task management skills.

The primary goal of operators in this simulation game is to synchronize generators in parallel. Achieving synchronization ensures the operation of generators is both more stable and efficient. This, in turn, allows operators to meet power demands more effectively, reducing energy wastage and enhancing fuel efficiency.

# Project Setup
Here are the essential instructions for configuring the project to enable the use of SteamVR in Unity.

## Unity
Follow these steps to set up Project in Unity:
1.	Begin by creating a new 3D project using Editor Version "2021.3.11f1".
•	Note: While you may use other versions in the future, the Unity project is expected to remain functional without any risk of data loss.
2.	Import the "Unity Package" into the newly created Unity Project.
•	Upon importing the "Unity Package," you should not encounter any issues. However, if the materials appear in pink, it indicates that you may have imported the package into a URP (Universal Render Pipeline) Project.
•	To resolve this, refer to instructions on how to switch to the Unity Standard Render Pipeline.

## SteamVR
Follow these steps to set up SteamVR in Unity:
1.	Open the Unity Asset Store and log in to your account.
2.	Add the "SteamVR Plugin" and "XR Plugin Management" to your account by clicking the "Add to My Assets" button.
To do this, go to:
•	Window > Package Manager > My Assets > SteamVR Plugin > Import
•	Window > Package Manager > My Assets > XR Plugin Management > Import
4.	After the importing loading screen is complete, accept the required conditions by clicking the "Accept All" button.
5.	If you intend to use VR, import XR Plugin Management:
•	Navigate to File > Build Settings > Player Settings > XR Plug-in Management > Open VR Loader
6.	Define actions for SteamVR Inputs:
•	Go to Window > SteamVR Input > Yes > Save and generate
7.	Now, you can fully utilize the play scenes and control your hands in SteamVR's "Interactions_Example."
If you encounter any confusion in the steps mentioned, refer to this link for a more detailed explanation: How to Setup SteamVR For Unity 2020 | SteamVR Import Steps | Unity VR Tutorial.

## Flip Knob (Interactable)  
The interactable asset is used by the operators to adjust the value of the value meters on the generator’s switchboard. Within this small knob, there is consist of several essential scripts that contribute to its functionality.
These scripts include "Interactable," "Flip Circular Drive," and "Flip Knob Behaviour."

### Interactable Script:  
Without this script, the operator won’t be able to interactive with. You can look through its code to see if their codes could develop the simulation further.

### Flip Circular Drive Script:  
It allows the knobs to move in a circular motion, allowing operators to twist and turn the knobs to manage the value meter. In the Inspector, a Unity event allows developers to add functions triggered when the rotation reaches its minimum or maximum.

### Flip Knob Behaviour Script:  
Designed to govern interactable knobs, this script establishes rotational limits and functions to verify full interaction with the knob, logging a message accordingly.  
The following lines of code, executed at the scene's start, instruct the "Flip Circular Drive" to activate limits, setting them at -45 and 45, allowing the knobs to flip to a 45-degree angle on either side.

## Rotary Knob (Interactable)  
Operators utilize the interactive feature to specify the generator linked to the synchroscope display. This not only alters the visual representation but also impacts the synchronization lamps and needle. The rotary switch has its own script and relies on external elements for functionality.

### Interactable Script:  
Without this script, the operator won’t be able to interactive with. You can look through its code to see if their codes could develop the simulation further.

### Flip Circular Drive Script:  
It allows the knobs to move in a circular motion, allowing operators to twist and turn the knobs to manage the value meter. In the Inspector, a Unity event allows developers to add functions triggered when the rotation reaches its minimum or maximum.

### Rotary Kob Behaviour Script:  
This script is designed for managing the behavior of interactive rotary knobs by adjusting the number of intervals when the scene starts and snapping to the nearest rotation when the knob is turned by the operator.

The following lines of code generate an array of rotation values based on intervals predetermined by the developer in the Inspector at the start of the scene. For example, with "4" interval points, the angles would be 90, 180, 270, and 360.

The following line of code continuously monitor the rotary knob's current rotation, analyzing its upper and lower values. When the operator releases the handle of the knob, the script chooses the upper or lower value, snapping to the position closest to the current rotation.

## Value Meters (Current/Power/Frequency/Voltage)
Operators utilize value meters to assess the values of various energy types generated by the vessel's engine. The operators are responsible for inspecting the value meters associated with both incoming and upcoming generators.

### Value Meter Script:  
The code for the value meters is highly adaptable, allowing for modifications to the range of values displayed on the analog panel. The flip value, which impacts the value meter, can also be altered. Despite utilizing the same script, it can be applied to various units of value meters.

Included below are functions for increasing and decreasing to manipulate the value meters. Criteria have been established to determine under what conditions specific energy meters can or cannot be adjusted during distinct phases of the game.

## Synchroscope Needle
Employed by operators utilizing the analog synchroscope method for generator synchronization, this tool, similar to the 2 Bright 1 Dark Method, aims to verify the generator’s phase sequence. The pointer needle indicates the necessary adjustment direction and speed to synchronize the generator.

 The provided code illustrates the rotation of the synchroscope needle, which can turn either clockwise or counterclockwise based on the generator's frequency. Additionally, an additonal touch has been incorporated: when the isolator switch closes the circuit and the needle approaches the 12 o'clock position, it smoothly returns to the 12 o'clock position.

## Synchroscope Lamps
Utilized by operators employing the 2 Bright 1 Dark Method for generator synchronization, the synchroscope lamp, similar to the analog synchroscope method, serves to check the generator’s phase sequence. It signifies this by identifying the rotation direction of the generator’s field, typically represented by three bulbs.

### Synchroscope Lamps Script:  
The crucial code below ensures that the lamp fades in and out properly. The script must first enable the material to be emissive before instructing the lamp to fade in. The subsequent functions dictate the fading in or out of the synchroscope bulbs, with their execution triggered by the "Synchroscope Manager." Boolean checks must be completed before the function proceeds, and as the fade-in duration approaches completion, the automatic fade-out occurs; the process operates vice versa.

## Synchroscope Manager:
This pivotal script is essential for the functionality of the simulation, managing everything associated with the synchroscope. It dictates when to call functions and presets variables from relevant scripts. While the previous script enables the synchroscope lamps to fade in and out in a single cycle, only the game manager, after assessing certain booleans related to rotation cycles, permits the lamps to fade in. It also evaluates a specific boolean to determine whether the rotation cycle of the lamps should be clockwise or counterclockwise.

### Synchroscope Manager Script:  
The subsequent lines of code empower the interactive knobs of the "Governor Switch" to adjust the lerp duration, enabling the "Synchroscope Needle" to move at a faster or slower pace.

## Game Manager
The "Simulation Director" holds dominion over the entire game flow in the simulation. Its responsibilities include tasks like loading different module scenes, coordinating restarts, and setting up preset variables within the scene.

The group of functions below defines the various preset values that the value meters will use when entering various modules.

To wrap up the simulation, the Game Manager examines predetermined criteria to determine whether they were met, determining whether the operator passed or failed. It is worth noting that different modules present different criteria that must be met.

## Post Processing Manager Script:
This script serves solely for post-processing within the scene, allowing the emissive material to bloom and radiate with the intensity of a light source. This gives the simulation a more realistic and immersive feel.
