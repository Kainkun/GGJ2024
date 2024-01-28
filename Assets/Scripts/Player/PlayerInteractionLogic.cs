using System;
using UnityEngine;

[Serializable]
public class PlayerInteractReferences
{
    public Transform viewTransform;
    public GameObject interactSource;
}

public class PlayerInteractionLogic
{
    private readonly PlayerInteractReferences _references;
    private readonly RaycastHit[] _hitBuffer = new RaycastHit[20];
    private bool _prevHasTarget;
    
    public Interactable Target { get; private set; }
    public bool HasTarget { get; private set; }

    public event Action OnInteract;
    public event Action<Interactable> OnGainTarget;
    public event Action OnLoseTarget;

    public PlayerInteractionLogic(PlayerInteractReferences references)
    {
        _references = references;
    }
    
    public void Update(bool isInteractKeyDown)
    {
        Ray interactRay = new Ray(_references.viewTransform.position, _references.viewTransform.forward);
        int hitCount = Physics.RaycastNonAlloc(interactRay, _hitBuffer);
        RaycastHit nearest = default;

        // find the nearest interactable or solid object.
        {
            float nearestDist = float.PositiveInfinity;

            for (int i = 0; i < hitCount; ++i)
            {
                if (_hitBuffer[i].distance < nearestDist)
                {
                    if (_hitBuffer[i].transform != _references.interactSource.transform && (_hitBuffer[i].TryGetComponentFromRaycastHit(out Interactable _) || !_hitBuffer[i].collider.isTrigger))
                    {
                        nearest = _hitBuffer[i];
                        nearestDist = _hitBuffer[i].distance;
                    }
                }
            }
        }

        // if we found an interactable, set it as our target
        HasTarget = nearest.TryGetComponentFromRaycastHit(out Interactable target) && target.CanInteract(_references.interactSource);
        Target = HasTarget ? target : null;

        if (HasTarget && !_prevHasTarget)
        {
            // just gained target
            OnGainTarget?.Invoke(Target);
        }
        if (!HasTarget && _prevHasTarget)
        {
            // just lost target
            OnLoseTarget?.Invoke();
        }
        
        if (isInteractKeyDown && HasTarget && Target.CanInteract(_references.interactSource))
        {
            Target.Interact(_references.interactSource);
            OnInteract?.Invoke();
        }

        _prevHasTarget = HasTarget;
    }
}
