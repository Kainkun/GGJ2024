using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreTrigger : MonoBehaviour
{
    public Action triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered?.Invoke();
        }
    }
}