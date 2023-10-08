using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour
{
    public Material emissiveMaterial;
    private Renderer _RenderCube;
    public GameObject objCube;

    private Color color;
    private float intensity;

    // Start is called before the first frame update
    void Start()
    {
        _RenderCube = objCube.GetComponent<Renderer>();
        //emissiveMaterial = _RenderCube.GetComponent<Renderer>().material;
        _RenderCube.material = emissiveMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            emissiveMaterial.EnableKeyword("_EMISSION");
            color = Color.red;
            intensity = 5.0f;
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        }
        if (Input.GetKeyDown("down"))
        {
            emissiveMaterial.EnableKeyword("_EMISSION");
            color = Color.blue;
            intensity = 5.0f;
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
        }

    }
}
