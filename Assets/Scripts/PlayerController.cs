using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private FpsPlayerReferences fpsReferences;
        
    [SerializeField]
    private FpsPlayerSettings fpsSettings;
        
    private FpsPlayerLogic _fpsLogic;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _fpsLogic = new FpsPlayerLogic(fpsSettings, fpsReferences);
    }

    private void Update()
    {
        _fpsLogic.Update(Time.deltaTime);
    }
}
