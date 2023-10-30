using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{

    public Camera Camera;
    public SteamVR_Input_Sources targetSource;
    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject = null;
    private PointerEventData Data = null;

    protected override void Awake()
    {
        base.Awake();

        Data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        //Reset Data, set camera 
        Data.Reset();
        Data.position = new Vector2(Camera.pixelWidth / 2, Camera.pixelHeight / 2);

        //Raycast
        eventSystem.RaycastAll(Data, m_RaycastResultCache);
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = Data.pointerCurrentRaycast.gameObject;

        //Clear Raycast
        m_RaycastResultCache.Clear();

        //Hover State
        HandlePointerExitAndEnter(Data, currentObject);

        //Press
        if (clickAction.GetStateDown(targetSource))
        {
            ProcessPress(Data);
        }

        //Release
        if(clickAction.GetStateUp(targetSource))
        {
            ProcessRelease(Data);
        }
    }   

    public PointerEventData GetData()
    {
        return Data;
    }

    private void ProcessPress(PointerEventData data)
    {

    }

    private void ProcessRelease(PointerEventData data)
    {

    }
}
