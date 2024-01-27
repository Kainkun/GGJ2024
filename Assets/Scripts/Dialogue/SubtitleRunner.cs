using TMPro;
using UnityEngine;
 
public class SubtitleRunner : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text subtitleTextDisplay;

    private float _showTime;
    private bool _isShown;

    private void Awake()
    {
        subtitleTextDisplay.SetText(string.Empty);
    }

    public void DisplaySubtitle(string text, float duration)
    {
        subtitleTextDisplay.SetText(text);
        _showTime = duration;
        _isShown = true;
    }

    private void Update()
    {
        _showTime -= Time.deltaTime;

        if (_showTime <= 0)
        {
            if (_isShown)
                subtitleTextDisplay.SetText(string.Empty);
            
            _isShown = false;
        }
    }
}
