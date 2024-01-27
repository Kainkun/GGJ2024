using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image black;
    public AnimationCurve curve;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeToScene(int scene)
    {
        StartCoroutine(CR_FadeToScene(scene));
    }

    IEnumerator CR_FadeToScene(int scene)
    {
        float t = 0;
        while (t < curve.keys[curve.length - 1].time)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            black.color = new Color(0, 0, 0, a);
            yield return null;
        }

        SceneManager.LoadScene(scene);

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            black.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }
}