using UnityEngine;

[CreateAssetMenu]
public class GameCharacterNameLocator : GameCharacterLocator
{
    [SerializeField]
    private string gameObjectName;
    
    public override bool TryFindCharacter(out GameObject gameObject)
    {
        gameObject = GameObject.Find(gameObjectName);
        return gameObject != null;
    }
}
