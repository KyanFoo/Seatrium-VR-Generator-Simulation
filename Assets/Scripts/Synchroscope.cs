using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synchroscope : MonoBehaviour
{
    private bool isOn;

    public Material emissiveMaterial1;
    public Material emissiveMaterial2;
    public Material emissiveMaterial3;

    public GameObject lightBulb1;
    public GameObject lightBulb2;
    public GameObject lightBulb3;

    private Renderer _renderBulb1;
    private Renderer _renderBulb2;
    private Renderer _renderBulb3;

    public Color color;
    public int highIntensity;
    private float midIntensity;
    private int lowIntenisty;
    private float intensity;
    // Start is called before the first frame update
    void Start()
    {
        isOn = false;

        _renderBulb1 = lightBulb1.GetComponent<Renderer>();
        _renderBulb2 = lightBulb2.GetComponent<Renderer>();
        _renderBulb3 = lightBulb3.GetComponent<Renderer>();

        _renderBulb1.material = emissiveMaterial1;
        _renderBulb2.material = emissiveMaterial2;
        _renderBulb3.material = emissiveMaterial3;

        midIntensity = highIntensity / 2;
        lowIntenisty = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //emissiveMaterial1.EnableKeyword("_EMISSION");
        //intensity = 5.0f;
        //emissiveMaterial1.SetColor("_EmissionColor", color * intensity);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOn = true;
            while (isOn == true)
            {
                FlashBulb1();
                FlashBulb2();

            }
        }
    }
    public void FlashBulb1()
    {
        emissiveMaterial1.EnableKeyword("_EMISSION");
        intensity = highIntensity;
        emissiveMaterial1.SetColor("_EmissionColor", color * intensity);

        emissiveMaterial2.EnableKeyword("_EMISSION");
        intensity = lowIntenisty;
        emissiveMaterial2.SetColor("_EmissionColor", color * intensity);

    }
    public void FlashBulb2()
    {
        emissiveMaterial2.EnableKeyword("_EMISSION");
        intensity = lowIntenisty;
        emissiveMaterial2.SetColor("_EmissionColor", color * intensity);

        emissiveMaterial1.EnableKeyword("_EMISSION");
        intensity = highIntensity;
        emissiveMaterial1.SetColor("_EmissionColor", color * intensity);
    }
}
