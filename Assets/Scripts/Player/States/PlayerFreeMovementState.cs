using System;
using UnityEngine;

[Serializable]
public class PlayerFreeMovementState : PlayerState
{
    public GameObject interactHint;
    public GameObject checkPaperHint;
    public GameObject movementHint;
    public int checkPaperHintCount;
    private float _elapsedDistance;
    private Vector3 _prevPos;
    
    public override void OnEnter()
    {
        base.OnEnter();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Vector3 curPos = Controller.transform.position;
        Controller.MovementLogic.Update(Time.deltaTime, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        Controller.RotationLogic.Update(Input.GetAxisRaw("Mouse X") + Input.GetAxisRaw("LookHack"), Input.GetAxisRaw("Mouse Y"));
        Controller.InteractionLogic.Update(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0));
        Controller.FootstepLogic.Update();
        Controller.crossHair.alpha = Mathf.MoveTowards(Controller.crossHair.alpha, Controller.InteractionLogic.HasTarget ? 1 : 0, Time.deltaTime * 15);
        interactHint.SetActive(Controller.InteractionLogic.HasTarget);
        _elapsedDistance += (curPos - _prevPos).magnitude;
        
        if(_elapsedDistance > 10)
            movementHint.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Controller.StateMachine.TransitionTo(Controller.checkPaperState);
            checkPaperHintCount--;
        }
        
        if (checkPaperHintCount <= 0)
            checkPaperHint.SetActive(false);

        _prevPos = curPos;
    }

    public override void OnExit()
    {
        base.OnExit();
        Controller.crossHair.alpha = 0;
    }
}
