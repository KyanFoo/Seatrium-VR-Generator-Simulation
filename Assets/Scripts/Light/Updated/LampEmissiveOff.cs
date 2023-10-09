using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampEmissiveOff : MonoBehaviour
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

    public bool offLight;
    // Start is called before the first frame update
    void Start()
    {
        _renderCube = objCube.GetComponent<Renderer>();
        _renderCube.material = emissiveMaterial;
        emissiveMaterial.EnableKeyword("_EMISSION");
        intensity = 0;
        emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        offLight = true;
        StartCoroutine(OnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpTime / lerpDuration >= 1.0f)
        {
            // Disable "LightOnCoroutine" Coroutine.//
            StopCoroutineAndExitLoop();
        }
    }
    IEnumerator OnCoroutine()
    {
        while (offLight == true)
        {
            lerpTime = Time.time - lerpStartTime;

            emissiveMaterial.EnableKeyword("_EMISSION");

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            yield return null;
        }
        Debug.Log("Finish Lighting Up");
    }
    private void StopCoroutineAndExitLoop()
    {
        offLight = false;
        StopCoroutine(OnCoroutine());
        enabled = false;
    }
}
