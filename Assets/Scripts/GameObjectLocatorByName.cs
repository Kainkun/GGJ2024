using UnityEngine;

[CreateAssetMenu]
public class GameObjectLocatorByName : GameObjectLocator
{
    [SerializeField]
    private string gameObjectName;
    
    public override bool TryFindCharacter(out GameObject gameObject)
    {
        gameObject = GameObject.Find(gameObjectName);
        return gameObject != null;
    }
}
