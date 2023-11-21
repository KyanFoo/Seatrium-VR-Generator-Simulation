# Table of contents
* [Title/Introduction](#Title/Introduction)
* [Project Background](#Project-Background)

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

