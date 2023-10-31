using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

public class Pointer : MonoBehaviour
{
    public float defaultLength = 5.0f;
    public GameObject Dot;
    public VRInputModule inputModule;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        //referencing line renderer in inspector
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        Ray theRay = new Ray(transform.position, transform.forward);
        RaycastHit hitt;
        if (Physics.Raycast(transform.position, transform.forward, out hitt))
        {
            if (hitt.collider.tag == "canvas")
            {
                Debug.Log("Its a canvas");
                UpdateLine();
            }
            else
            {
                Debug.Log("not a canvas");
                StopLine();
            }
        }
        //lineRenderer.enabled = false;
        //Dot.SetActive(false);

        //UpdateLine();

    }

    private void UpdateLine()
    {

        lineRenderer.enabled = true;
        Dot.SetActive(true);

        //Use default length for length of line
        PointerEventData data = inputModule.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;

        //call raycast
        RaycastHit raycast = CreateRaycast(targetLength);

        //default end, if our raycast doesnt hit anything, the location of the dot will be at the end
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //Check for any collider hit
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, defaultLength))
        {
            if (hit.collider != null)
            {
                endPosition = hit.point;
            }
        }
       
        //Set position of dot
        Dot.transform.position = endPosition;

        //Set position for line renderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPosition);

    }

    //Create Raycast to detect if pointer hits something
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast (ray, out hit, defaultLength);

        return hit;
    }

    private void StopLine()
    {
        lineRenderer.enabled = false;
        Dot.SetActive(false);
    }
}
