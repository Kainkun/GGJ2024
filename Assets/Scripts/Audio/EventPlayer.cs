using UnityEngine;

namespace ScriptedEvents
{
    public class EventPlayer : MonoBehaviour
    {
        public AK.Wwise.Event eventToPlay;
        
        public void PlayEvent()
        {
            eventToPlay.Post(gameObject);
        }
    }
}
