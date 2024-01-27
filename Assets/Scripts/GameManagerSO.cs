using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerSO", menuName = "GameManagerSO", order = 1)]
public class GameManagerSO : ScriptableObject
{
    public static void FadeToGame()
    {
        GameManager.instance.animator.Play("FadeToGame");
    }
    
    public static void FadeToEpilogue()
    {
        GameManager.instance.animator.Play("FadeToEpilogue");
    }
}