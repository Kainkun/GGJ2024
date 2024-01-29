using UnityEngine;

namespace Player
{
    public class RttpPlayer : MonoBehaviour
    {
        [SerializeField] private AK.Wwise.RTPC rtpc;
        [SerializeField] private float duration;

        private bool _isChanging;
        private float _elapsed;

        private void Update()
        {
            if (_isChanging)
            {
                _elapsed += Time.deltaTime;
                float t = _elapsed / duration;
                t = Mathf.Clamp01(t);
                rtpc.SetGlobalValue(t);
            }
        }

        public void Change()
        {
            _isChanging = true;
        }
    }
}
