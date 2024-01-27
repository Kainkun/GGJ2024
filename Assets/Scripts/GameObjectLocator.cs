using UnityEngine;

public abstract class GameObjectLocator : ScriptableObject
{
    public abstract bool TryFindCharacter(out GameObject gameObject);
}
