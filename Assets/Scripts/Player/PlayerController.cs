using UnityEngine;

public abstract class PlayerState : State
{
    protected PlayerController Controller { get; private set; }
    
    public void Initialize(PlayerController controller)
    {
        Controller = controller;
        OnInitialize();
    }
    
    public virtual void OnUpdate() {}
    protected virtual void OnInitialize() {}
}

public class PlayerController : MonoBehaviour
{
    public PlayerFreeMovementState freeMovementState;
    public PlayerCheckPaperState checkPaperState;
    
    [Header("References")]
    [SerializeField] private Transform yawTransform;
    [SerializeField] private Transform pitchTransform;
    [SerializeField] private GameObject interactionSource;
    [SerializeField] private CharacterController characterController;
    [SerializeField] public CanvasGroup crossHair;
    
    [Header("Settings")]
    [SerializeField] private PlayerMovementSettings movementSettings;
    [SerializeField] private PlayerRotationSettings rotationSettings;
    [SerializeField] private DistanceFootstepSettings footstepSettings;
    
    public PlayerMovementLogic MovementLogic;
    public PlayerInteractionLogic InteractionLogic;
    public PlayerRotationLogic RotationLogic;
    public DistanceFootstepLogic FootstepLogic;

    public StateMachine<PlayerState> StateMachine { get; private set; }

    private void Awake()
    {
        MovementLogic = new PlayerMovementLogic(movementSettings, new PlayerMovementReferences{yawTransform = yawTransform, characterController = characterController});
        RotationLogic = new PlayerRotationLogic(rotationSettings, new PlayerRotationReferences{yawTransform = yawTransform, pitchTransform = pitchTransform});
        InteractionLogic = new PlayerInteractionLogic(new PlayerInteractReferences{interactSource = interactionSource, viewTransform = pitchTransform});
        FootstepLogic = new DistanceFootstepLogic(footstepSettings, new DistanceFootstepReferences {body = gameObject});
        
        StateMachine = new StateMachine<PlayerState>();
        freeMovementState.Initialize(this);
        checkPaperState.Initialize(this);
        StateMachine.TransitionTo(freeMovementState);
    }

    private void Update()
    {
        StateMachine.CurrentState?.OnUpdate();
    }
}
