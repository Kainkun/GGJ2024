using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image black;
    public AnimationCurve fadeToGame;
    public AnimationCurve fadeToEpilogue;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeToGame()
    {
        StartCoroutine(CR_FadeToGame());
    }

    IEnumerator CR_FadeToGame()
    {
        float t = 0;
        float value;
        while (t < fadeToGame.keys[fadeToGame.length - 1].time)
        {
            t += Time.deltaTime;
            value = fadeToGame.Evaluate(t);
            black.color = new Color(0, 0, 0, value);
            yield return null;
        }

        black.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(0.5f);

        while (t > 0)
        {
            t -= Time.deltaTime;
            value = fadeToGame.Evaluate(t);
            black.color = new Color(0, 0, 0, value);
            yield return null;
        }

        black.color = new Color(0, 0, 0, 0);
    }

    public void FadeToEpilogue()
    {
        StartCoroutine(CR_FadeToEpilogue());
    }

    IEnumerator CR_FadeToEpilogue()
    {
        float t = 0;
        float value;
        while (t < fadeToEpilogue.keys[fadeToEpilogue.length - 1].time)
        {
            t += Time.deltaTime;
            value = fadeToEpilogue.Evaluate(t);
            black.color = new Color(0, 0, 0, value);
            yield return null;
        }

        black.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(0.5f);

        while (t > 0)
        {
            t -= Time.deltaTime;
            value = fadeToEpilogue.Evaluate(t);
            black.color = new Color(0, 0, 0, value);
            yield return null;
        }

        black.color = new Color(0, 0, 0, 0);
    }
}