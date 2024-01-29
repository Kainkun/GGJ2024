using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartEpilogue()
    {
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     GetComponent<Animator>().SetTrigger("FadeOut");
        // }
    }
}