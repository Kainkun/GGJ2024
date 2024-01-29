﻿using UnityEngine;

namespace ScriptedEvents
{

    public class EventPlayer : MonoBehaviour
    {
        public GameObject source;
        public bool autoPlay = false;
        public AK.Wwise.Event eventToPlay;

        private void Start()
        {
            if (autoPlay)
                PlayEvent();
        }

        public void PlayEvent()
        {
            eventToPlay.Post(source == null ? gameObject : source);
        }
    }
}
