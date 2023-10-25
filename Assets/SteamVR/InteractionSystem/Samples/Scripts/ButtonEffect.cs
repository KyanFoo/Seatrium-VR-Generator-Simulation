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
        public Color color;
        public float intensity;
        public void OnButtonDown(Hand fromHand)
        {
            //ColorSelf(Color.yellow);
            emissiveMaterial.EnableKeyword("_EMISSION");
            emissiveMaterial.SetColor("_EmissionColor", color * intensity);
            fromHand.TriggerHapticPulse(1000);
        }

        public void OnButtonUp(Hand fromHand)
        {
            emissiveMaterial.DisableKeyword("_EMISSION");
            //ColorSelf(Color.red);
        }

        private void ColorSelf(Color newColor)
        {
            Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
            for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
            {
                renderers[rendererIndex].material.color = newColor;
            }
        }
    }
}