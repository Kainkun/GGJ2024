using System;
using UnityEngine;

[Serializable]
public class PlayerMovementReferences
{
    public Transform yawTransform;
    public CharacterController characterController;
}

[Serializable]
public class PlayerMovementSettings
{
    public float moveSpeed = 5;
    public float moveAcceleration = 50;
    public float moveDeceleration = 15;
    public float gravity = 9;
}

public class PlayerMovementLogic
{
    private readonly PlayerMovementReferences _movementReferences;
    private readonly PlayerMovementSettings _movementSettings;
    private Vector3 _velocity;

    public PlayerMovementLogic(PlayerMovementSettings movementSettings, PlayerMovementReferences movementReferences)
    {
        _movementSettings = movementSettings;
        _movementReferences = movementReferences;
    }
        
    public void SetPosition(Vector3 position)
    {
        _movementReferences.characterController.transform.position = position;
        Physics.SyncTransforms();
    }

    public void Update(float deltaTime, Vector3 inputDirection)
    {
        Vector3 targetVelocity = inputDirection * _movementSettings.moveSpeed;
        targetVelocity = _movementReferences.yawTransform.TransformDirection(targetVelocity);
        targetVelocity.y = _velocity.y;

        bool isAccelerating = inputDirection != Vector3.zero;
        _velocity = isAccelerating ?
            Vector3.MoveTowards(_velocity, targetVelocity, _movementSettings.moveAcceleration * deltaTime) :
            Vector3.Lerp(_velocity, targetVelocity, _movementSettings.moveDeceleration * deltaTime);
            
        Vector3 gravityForce = Vector3.down * (_movementSettings.gravity * deltaTime);
        _velocity += gravityForce;

        _movementReferences.characterController.Move(_velocity * deltaTime);
        _velocity = _movementReferences.characterController.velocity;
        
        // this might be important
        Physics.SyncTransforms();
    }
}
