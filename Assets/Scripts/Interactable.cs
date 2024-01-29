using System;
using UnityEngine;
using UnityEngine.Events;

public struct InteractEventData
{
    public Interactable Interactable;
    public GameObject Source;
}

// note: we could make canInteract and interact virtual in the future, if we need more control
public class Interactable : MonoBehaviour
{
    [SerializeField]
    private float maxInteractDistance = 1;

    [SerializeField] 
    private float cooldown = 0;

    [SerializeField] 
    public UnityEvent<InteractEventData> onInteract;

    private float _cooldown;
    
    public bool CanInteract(GameObject source)
    {
        float distanceSqr = (source.transform.position - transform.position).sqrMagnitude;
        bool isInRange = distanceSqr < maxInteractDistance * maxInteractDistance;
        bool offCooldown = _cooldown <= 0;
        return isInRange && offCooldown && enabled && gameObject.activeInHierarchy;
    }

    private void Update()
    {
        _cooldown -= Time.deltaTime;
        _cooldown = Mathf.Max(0, _cooldown);
    }

    public void Interact(GameObject source)
    {
        if (_cooldown > 0)
            return;
            
        print($"Interacted with {name}");
        _cooldown = cooldown;
        
        onInteract.Invoke(new InteractEventData
        {
            Interactable = this,
            Source = source,
        });
    }
}
