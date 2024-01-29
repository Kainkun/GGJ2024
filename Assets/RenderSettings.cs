using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderSettings : MonoBehaviour
{
    public UniversalRenderPipelineAsset urp;
    
    public float fogDensity = 0.01f;
    public Color fogColor = Color.grey;

    private void Awake()
    {
        SetSunDistance(27f);
    }

    public void SetSunDistance(float distance)
    {
        QualitySettings.shadowDistance = distance;
        urp = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;
        urp.shadowDistance = distance;
    }

    public void SetFog()
    {
        UnityEngine.RenderSettings.fogDensity = fogDensity;
        UnityEngine.RenderSettings.fogColor = fogColor;
    }
}