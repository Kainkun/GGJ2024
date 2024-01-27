﻿using System;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public string id;
    public Line[] lines;

    [Serializable]
    public struct Line
    {
        public float duration;
        public GameCharacterLocator speaker;
        public AK.Wwise.Event audioEvent;
    }
}