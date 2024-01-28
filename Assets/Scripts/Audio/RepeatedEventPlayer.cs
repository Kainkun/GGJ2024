using UnityEngine;

public class RepeatedEventPlayer : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 30;
    [SerializeField] private AK.Wwise.Event audioEvent;

    private float _elapsed;

    private void Update()
    {
        _elapsed += Time.deltaTime;

        if (_elapsed > delaySeconds)
        {
            _elapsed = 0;
            audioEvent.Post(gameObject);
        }
    }
}
