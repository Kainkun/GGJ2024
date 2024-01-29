using System;
using UnityEngine;

namespace Player
{
    public class PaperStateChanger : MonoBehaviour
    {
        public PaperRandomizer.State state;
        
        public void ChangeState()
        {
            FindAnyObjectByType<PaperRandomizer>().CurrentState = state;
        }
    }

}
