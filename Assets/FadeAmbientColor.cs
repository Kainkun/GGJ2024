using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAmbientColor : MonoBehaviour
{
    public float time = 1;
    public Color32 color;

    public void fadeTo()
    {
        StartCoroutine(CR_FadeTo());
    }

    IEnumerator CR_FadeTo()
    {
        Color32 startColor = UnityEngine.RenderSettings.ambientLight;
        float t = 0;
        while (t < time)
        {
            t += Time.deltaTime;
            UnityEngine.RenderSettings.ambientLight =
                Color32.Lerp(startColor, color, t / time);
            yield return null;
        }
    }
}