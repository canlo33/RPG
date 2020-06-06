using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    protected Data_WalkState stateData;
    public WalkState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_WalkState stateData) : base(stateMachine, entity, animationName)
    {
        this.stateData = stateData;        
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);
        //TODO: Pick a random patrol point.
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void Patrol()
    {
        
    }
}
