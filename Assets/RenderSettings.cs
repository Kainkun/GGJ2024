using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderSettings : MonoBehaviour
{
    public UniversalRenderPipelineAsset urp;

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
}