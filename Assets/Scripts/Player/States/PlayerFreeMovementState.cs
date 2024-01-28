using System;
using UnityEngine;

[Serializable]
public class PlayerFreeMovementState : PlayerState
{
    public override void OnEnter()
    {
        base.OnEnter();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Controller.MovementLogic.Update(Time.deltaTime, new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
        Controller.RotationLogic.Update(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Controller.InteractionLogic.Update(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0));
        Controller.FootstepLogic.Update();
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
            Controller.StateMachine.TransitionTo(Controller.checkPaperState);
    }
}
