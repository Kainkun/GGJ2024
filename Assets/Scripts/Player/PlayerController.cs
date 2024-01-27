using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    
    [SerializeField]
    private Transform yawTransform;
    
    [SerializeField]
    private Transform pitchTransform;

    [SerializeField] 
    private CharacterController characterController;
    
    [Header("Settings")]
        
    [SerializeField]
    private PlayerMovementSettings movementSettings;
    
    [SerializeField] 
    private PlayerRotationSettings rotationSettings;
        
    private PlayerMovementLogic _movementLogic;
    private PlayerRotationLogic _rotationLogic;
    private PlayerInteractionLogic _interactionLogic;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        _movementLogic = new PlayerMovementLogic(movementSettings, new PlayerMovementReferences{yawTransform = yawTransform, characterController = characterController});
        _rotationLogic = new PlayerRotationLogic(rotationSettings, new PlayerRotationReferences{yawTransform = yawTransform, pitchTransform = pitchTransform});
        _interactionLogic = new PlayerInteractionLogic(new PlayerInteractReferences{interactSource = gameObject, viewTransform = pitchTransform});
    }

    private void Update()
    {
        _movementLogic.Update(Time.deltaTime, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        _rotationLogic.Update(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        _interactionLogic.Update(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0));
    }
}
