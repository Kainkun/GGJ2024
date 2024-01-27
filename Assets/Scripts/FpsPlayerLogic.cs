using System;
using UnityEngine;

[Serializable]
public class FpsPlayerReferences
{
    public Transform yawTransform;
    public Transform pitchTransform;
    public CharacterController characterController;
}

[Serializable]
public class FpsPlayerSettings
{
    public float moveSpeed = 5;
    public float moveAcceleration = 50;
    public float moveDeceleration = 15;
    public float gravity = 9;
}

public class FpsPlayerLogic
{
    private readonly FpsPlayerReferences _references;
    private readonly FpsPlayerSettings _settings;
    private float _pitch;
    private Vector3 _velocity;
    private float _yaw;

    public FpsPlayerLogic(FpsPlayerSettings settings, FpsPlayerReferences references)
    {
        _settings = settings;
        _references = references;
        _pitch = _references.pitchTransform.localRotation.eulerAngles.x;
        _yaw = _references.yawTransform.localRotation.eulerAngles.y;
    }
        
    public void TeleportTo(Vector3 position, Quaternion rotation)
    {
        _references.characterController.transform.position = position;
        _pitch = rotation.eulerAngles.x;
        _yaw = rotation.eulerAngles.y;
        Physics.SyncTransforms();
    }

    public void Update(float deltaTime)
    {
        // apply rotation
        _pitch -= Input.GetAxisRaw("Mouse Y");
        _yaw += Input.GetAxisRaw("Mouse X");
        _pitch = Mathf.Clamp(_pitch, -90, 90);
        _references.pitchTransform.localRotation = Quaternion.Euler(_pitch, 0, 0);
        _references.yawTransform.localRotation = Quaternion.Euler(0, _yaw, 0);

        // apply movement
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 targetVelocity = inputDirection * _settings.moveSpeed;
        targetVelocity = _references.yawTransform.TransformDirection(targetVelocity);
        targetVelocity.y = _velocity.y;

        bool isAccelerating = inputDirection != Vector3.zero;
        _velocity = isAccelerating ?
            Vector3.MoveTowards(_velocity, targetVelocity, _settings.moveAcceleration * deltaTime) :
            Vector3.Lerp(_velocity, targetVelocity, _settings.moveDeceleration * deltaTime);
            
        Vector3 gravityForce = Vector3.down * (_settings.gravity * Time.deltaTime);
        _velocity += gravityForce;

        _references.characterController.Move(_velocity * deltaTime);
        _velocity = _references.characterController.velocity;
        
        // this might be important
        Physics.SyncTransforms();
    }
}
