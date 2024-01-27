﻿using UnityEngine;

public abstract class PlayerState : State
{
    protected PlayerController Controller { get; private set; }
    
    public void Initialize(PlayerController controller)
    {
        Controller = controller;
        OnInitialize();
    }
    
    public virtual void OnUpdate() {}
    protected virtual void OnInitialize() {}
}

public class PlayerController : MonoBehaviour
{
    public PlayerFreeMovementState freeMovementState;

    public StateMachine<PlayerState> StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = new StateMachine<PlayerState>();
        
        freeMovementState.Initialize(this);
        
        StateMachine.TransitionTo(freeMovementState);
    }

    private void Update()
    {
        StateMachine.CurrentState?.OnUpdate();
    }
}
