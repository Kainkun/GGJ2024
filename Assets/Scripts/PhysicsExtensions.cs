using UnityEngine;

public static class PhysicsExtensions
{
    public static bool TryGetComponentFromRaycastHit<T>(this RaycastHit hit, out T result)
    {
        if (hit.rigidbody != null && hit.rigidbody.TryGetComponent(out result))
            return true;

        if (hit.collider != null && hit.collider.TryGetComponent(out result))
            return true;

        result = default;
        return false;
    }
}
