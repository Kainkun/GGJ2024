using System;
using UnityEngine;

[Serializable]
public class DistanceFootstepSettings
{
    public float stepDistance = 1;
    public AK.Wwise.Event stepEvent;
}

[Serializable]
public class DistanceFootstepReferences
{
    public GameObject body;
}

public class DistanceFootstepLogic
{
    private readonly DistanceFootstepSettings _settings;
    private readonly DistanceFootstepReferences _references;
    private float _elapsedDistance;
    private Vector3 _prevPos;
    
    public DistanceFootstepLogic(DistanceFootstepSettings settings, DistanceFootstepReferences references)
    {
        _settings = settings;
        _references = references;
    }
    
    public void Update()
    {
        Vector3 pos = _references.body.transform.position;
        float distance = (pos - _prevPos).magnitude;
        _elapsedDistance += distance;

        if (_elapsedDistance > _settings.stepDistance)
        {
            _settings.stepEvent.Post(_references.body);
            _elapsedDistance = 0;
        }
        
        _prevPos = pos;
    }
}
