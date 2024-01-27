using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}