using UnityEngine;

namespace Player
{
    public class PaperStateChanger : MonoBehaviour
    {
        public void ChangeState(PaperRandomizer.State state)
        {
            FindAnyObjectByType<PaperRandomizer>().CurrentState = state;
        }
    }
}
