using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampEmissiveOn : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        _renderCube = objCube.GetComponent<Renderer>();
        _renderCube.material = emissiveMaterial;
        emissiveMaterial.EnableKeyword("_EMISSION");
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        lerpTime = 0f;
        //onLight = true;
        //StartCoroutine(OnCoroutine());
    }
    public void CallOnCoroutine()
    {
        onLight = true;
        lerpTime = 0;
        StartCoroutine(OnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpTime / lerpDuration >= 1.0f)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopOnCoroutineAndExitLoop();
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
    }
}
