using UnityEngine;

public abstract class GameCharacterLocator : ScriptableObject
{
    public abstract bool TryFindCharacter(out GameObject gameObject);
}
