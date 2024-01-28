using UnityEngine;

public class MaterialTalkingAnim : MonoBehaviour
{
    public Material mouthOpenMaterial;
    public Material mouthClosedMaterial;
    public Renderer targetRenderer;
    public float mouthMoveDelay = 0.1f;
    
    private float _speakingDuration;
    private float _babbleCooldown;
    private bool _mouthOpen;
    public bool IsSpeaking { get; set; }

    private void Start()
    {
        DialogueSystem.Instance.OnRunDialogue += HandleRunDialogue;
    }
    private void HandleRunDialogue(DialogueEventData data)
    {
        if (data.Speaker != gameObject)
            return;

        _speakingDuration = data.Duration;
    }

    private void Update()
    {
        _speakingDuration -= Time.deltaTime;
        _speakingDuration = Mathf.Max(_speakingDuration, 0);
        IsSpeaking = _speakingDuration > 0;

        if (IsSpeaking)
        {
            _babbleCooldown -= Time.deltaTime;
            _babbleCooldown = Mathf.Max(_babbleCooldown, 0);

            if (_babbleCooldown == 0)
            {
                _babbleCooldown = mouthMoveDelay;
                _mouthOpen = !_mouthOpen;
                targetRenderer.material = _mouthOpen ? mouthOpenMaterial : mouthClosedMaterial;
            }
        }
        else targetRenderer.material = mouthClosedMaterial;
    }
}
