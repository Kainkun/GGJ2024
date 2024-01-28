using System;
using UnityEngine;

[Serializable]
public class PlayerFreeMovementState : PlayerState
{
    [Header("References")]
    
    [SerializeField]
    private Transform yawTransform;
    
    [SerializeField]
    private Transform pitchTransform;

    [SerializeField] 
    private GameObject interactionSource;
    
    [SerializeField] 
    private CharacterController characterController;

    [Header("Settings")]
    
    [SerializeField]
    private PlayerMovementSettings movementSettings;
    
    [SerializeField] 
    private PlayerRotationSettings rotationSettings;

    [SerializeField] 
    private DistanceFootstepSettings footstepSettings;

    private PlayerMovementLogic _movementLogic;
    private PlayerInteractionLogic _interactionLogic;
    private PlayerRotationLogic _rotationLogic;
    private DistanceFootstepLogic _footstepLogic;

    protected override void OnInitialize()
    {
        base.OnInitialize();
        _movementLogic = new PlayerMovementLogic(movementSettings, new PlayerMovementReferences{yawTransform = yawTransform, characterController = characterController});
        _rotationLogic = new PlayerRotationLogic(rotationSettings, new PlayerRotationReferences{yawTransform = yawTransform, pitchTransform = pitchTransform});
        _interactionLogic = new PlayerInteractionLogic(new PlayerInteractReferences{interactSource = interactionSource, viewTransform = pitchTransform});
        _footstepLogic = new DistanceFootstepLogic(footstepSettings, new DistanceFootstepReferences {body = Controller.gameObject});
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        _movementLogic.Update(Time.deltaTime, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        _rotationLogic.Update(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        _interactionLogic.Update(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0));
        _footstepLogic.Update();
    }
}
