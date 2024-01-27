using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animator animator;
    public static GameManager instance;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void Initialization()
    {
        instance = Instantiate(Resources.Load<GameObject>("GameManagerCanvas")).GetComponent<GameManager>();
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadEpilogue()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        animator.Play("BackToMenu");
    }
}