using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    public UnityEvent triggered;
    private bool hasTriggered = false;
    public bool repeatTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if ((!hasTriggered || repeatTrigger) && other.CompareTag("Player"))
        {
            print("Trigger");
            triggered?.Invoke();
            hasTriggered = true;
        }
    }
}