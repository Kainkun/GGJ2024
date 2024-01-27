using UnityEngine;
using UnityEngine.Events;

public struct InteractEventData
{
    public Interactable Interactable;
    public GameObject Source;
}

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private float maxInteractDistance = 1;

    [SerializeField] 
    private UnityEvent<InteractEventData> onInteract;
    
    public virtual bool CanInteract(GameObject source)
    {
        float distanceSqr = (source.transform.position - transform.position).sqrMagnitude;
        bool isInRange = distanceSqr < maxInteractDistance * maxInteractDistance;
        return isInRange && enabled && gameObject.activeInHierarchy;
    }

    public virtual void Interact(GameObject source)
    {
        print($"Interacted with {name}");
        
        onInteract.Invoke(new InteractEventData
        {
            Interactable = this,
            Source = source,
        });
    }
}
