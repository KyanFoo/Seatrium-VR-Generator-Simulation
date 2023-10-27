using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CanvasPointer : MonoBehaviour
{
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand; // Choose which hand to use.
    public SteamVR_Action_Boolean interactAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    private GameObject currentCanvas;
    private SteamVR_Behaviour_Pose controllerPose;
    private LineRenderer pointerLine;

    void Start()
    {
        controllerPose = GetComponent<SteamVR_Behaviour_Pose>();
        pointerLine = GetComponent<LineRenderer>();
        pointerLine.enabled = false;
    }

    void Update()
    {
        // Check if the interaction button is pressed.
        if (interactAction.GetState(handType))
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerPose.transform.position, controllerPose.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Canvas"))
                {
                    // Pointing at a canvas.
                    currentCanvas = hit.collider.gameObject;
                    pointerLine.enabled = true;
                    pointerLine.SetPosition(0, controllerPose.transform.position);
                    pointerLine.SetPosition(1, hit.point);

                    // Implement your interaction logic with the canvas here.
                    // For example, show a highlight or perform a click action.

                    // To hide the pointer, you can disable the LineRenderer when not pointing at the canvas.
                }
                else
                {
                    // Not pointing at a canvas. Hide the pointer.
                    if (currentCanvas != null)
                    {
                        // Implement any logic to reset the canvas state here (e.g., hide a highlight).
                    }
                    pointerLine.enabled = false;
                }
            }
            else
            {
                // Not pointing at anything. Hide the pointer.
                if (currentCanvas != null)
                {
                    // Implement any logic to reset the canvas state here (e.g., hide a highlight).
                }
                pointerLine.enabled = false;
            }
        }
        else
        {
            // Interaction button not pressed. Hide the pointer.
            if (currentCanvas != null)
            {
                // Implement any logic to reset the canvas state here (e.g., hide a highlight).
            }
            pointerLine.enabled = false;
        }
    }
}