//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ButtonEffect : MonoBehaviour
    {

        public Material emissiveMaterial;

        private float startIntensity = 0f;
        private float endIntensity = 1f;

        public Color color;
        private float intensity;

        public float lerpTime;
        public float lerpDuration = 1.0f;
        public void OnButtonDown(Hand fromHand)
        {
            ColorSelf(Color.yellow);
        }

        public void OnButtonUp(Hand fromHand)
        {
            ColorSelf(Color.red);
        }

        private void ColorSelf(Color newColor)
        {
            lerpTime = Time.deltaTime;

            intensity = Mathf.Lerp(startIntensity, endIntensity, lerpTime / lerpDuration);
            emissiveMaterial.SetColor("_EmissionColor", newColor * intensity);
        }
    }
}