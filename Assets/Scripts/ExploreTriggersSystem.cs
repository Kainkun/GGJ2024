using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ExploreTriggersSystem : MonoBehaviour
{
    public UnityEvent allTriggersTriggered;

    private List<ExploreTrigger> triggers = new List<ExploreTrigger>();
    private HashSet<ExploreTrigger> triggered = new HashSet<ExploreTrigger>();


    private void Start()
    {
        triggers = GetComponentsInChildren<ExploreTrigger>().ToList();
        foreach (ExploreTrigger exploreTrigger in triggers)
        {
            exploreTrigger.triggered += () => onTrigger(exploreTrigger);
        }
    }

    private void onTrigger(ExploreTrigger exploreTrigger)
    {
        triggered.Add(exploreTrigger);
        if (triggered.Count >= triggers.Count)
        {
            allTriggersTriggered?.Invoke();
            print("DONE");
        }
    }
}