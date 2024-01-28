using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ExploreTriggersSystem : MonoBehaviour
{
    public UnityEvent allTriggersTriggered;

    private List<PlayerTrigger> triggers = new List<PlayerTrigger>();
    private HashSet<PlayerTrigger> triggeredTriggers = new HashSet<PlayerTrigger>();


    private void Start()
    {
        triggers = GetComponentsInChildren<PlayerTrigger>().ToList();
        foreach (PlayerTrigger exploreTrigger in triggers)
        {
            exploreTrigger.triggered.AddListener(() => onTrigger(exploreTrigger));
        }
    }

    private void onTrigger(PlayerTrigger playerTrigger)
    {
        triggeredTriggers.Add(playerTrigger);
        if (triggeredTriggers.Count >= triggers.Count)
        {
            allTriggersTriggered?.Invoke();
            print("all triggers triggered");
        }
    }
}