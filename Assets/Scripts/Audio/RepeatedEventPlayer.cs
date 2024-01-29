using UnityEngine;

public class RepeatedEventPlayer : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 30;
    [SerializeField] private AK.Wwise.Event audioEvent;

    private float _elapsed;
    private bool _isPlaying;
    
    public void Play()
    {
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false;
        audioEvent.Stop(gameObject);
    }

    private void Update()
    {
        if (!_isPlaying)
            return;
        
        _elapsed += Time.deltaTime;

        if (_elapsed > delaySeconds)
        {
            _elapsed = 0;
            audioEvent.Post(gameObject);
        }
    }
}
