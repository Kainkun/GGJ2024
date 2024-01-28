﻿using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class PlayerCheckPaperState : PlayerState
{
    [SerializeField] 
    private PlayableDirector timeline;

    public override void OnEnter()
    {
        base.OnEnter();
        timeline.Play();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        Controller.MovementLogic.Update(Time.deltaTime, Vector3.zero);
        Controller.RotationLogic.Update(0, 0);
        Controller.InteractionLogic.Update(false);
        Controller.FootstepLogic.Update();
        
        if (timeline.state == PlayState.Paused && Input.GetKeyDown(KeyCode.Mouse1))
            Controller.StateMachine.TransitionTo(Controller.freeMovementState);
    }

    public override void OnExit()
    {
        base.OnExit();
        timeline.Resume();
    }
}
