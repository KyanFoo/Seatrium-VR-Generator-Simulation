using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusion : MonoBehaviour
{
    public Material emissiveMaterial;
    private Renderer _renderCube;
    public GameObject objCube;

    public Color color;

    public float startIntensity;
    public float endIntensity;
    private float intensity;

    public float savedStartIntensity;
    public float savedEndIntensity;

    private float lerpStartTime;
    public float lerpTime;
    public float lerpDuration = 2.0f;

    public bool onLight;
    public bool offLight;
    // Start is called before the first frame update
    void Start()
    {
        _renderCube = objCube.GetComponent<Renderer>();
        _renderCube.material = emissiveMaterial;
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        lerpTime = 0f;

        CallOnCoroutine();
    }
    public void CallOnCoroutine()
    {
        onLight = true;
        lerpTime = 0;
        StartCoroutine(OnCoroutine());
    }
    public void CallOffCoroutine()
    {
        offLight = true;
        lerpTime = 0;
        StartCoroutine(OffCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpTime / lerpDuration >= 1.0f && onLight == true)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopOnCoroutineAndExitLoop();
        }
        if (lerpTime / lerpDuration >= 1.0f && offLight == true)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopOffCoroutineAndExitLoop();
        }
        if (Input.GetKeyDown("up"))
        {
            lerpDuration = lerpDuration + 1;
        }
        if (Input.GetKeyDown("down"))
        {
            lerpDuration = lerpDuration - 1;
        }
    }
    IEnumerator OnCoroutine()
    {
        while (onLight == true)
        {
            lerpTime += Time.deltaTime;

            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        Debug.Log("Finish Lighting Up");
    }
    private void StopOnCoroutineAndExitLoop()
    {
        onLight = false;
        StopCoroutine(OnCoroutine());
        CallOffCoroutine();
    }
    IEnumerator OffCoroutine()
    {
        while (offLight == true)
        {
            lerpTime += Time.deltaTime;

            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(endIntensity, startIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        Debug.Log("Finish Lighting Up");
    }
    private void StopOffCoroutineAndExitLoop()
    {
        offLight = false;
        StopCoroutine(OffCoroutine());
        CallOnCoroutine();
    }
}
