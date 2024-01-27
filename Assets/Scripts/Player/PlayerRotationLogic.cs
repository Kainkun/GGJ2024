using System;
using UnityEngine;

[Serializable]
public class PlayerRotationReferences
{
    public Transform pitchTransform;
    public Transform yawTransform;
}

[Serializable]
public class PlayerRotationSettings
{
    public float sensitivity = 1;
    public bool invertY;
}

public class PlayerRotationLogic
{
    private readonly PlayerRotationReferences _references;
    private readonly PlayerRotationSettings _settings;
    private float _pitch;
    private float _yaw;

    public PlayerRotationLogic(PlayerRotationSettings settings, PlayerRotationReferences references)
    {
        _settings = settings;
        _references = references;
        _pitch = _references.pitchTransform.localRotation.eulerAngles.x;
        _yaw = _references.yawTransform.localRotation.eulerAngles.y;
    }

    public void SetRotation(Quaternion rotation)
    {
        _pitch = rotation.eulerAngles.x;
        _yaw = rotation.eulerAngles.y;
    }
    
    public void Update(float mouseDeltaX, float mouseDeltaY)
    {
        _pitch += mouseDeltaY * _settings.sensitivity * (_settings.invertY ? 1 : -1);
        _yaw += mouseDeltaX * _settings.sensitivity;
        _pitch = Mathf.Clamp(_pitch, -90, 90);
        _references.pitchTransform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        _references.yawTransform.localRotation = Quaternion.Euler(0, _yaw, 0);
    }
}
