using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    protected Data_WalkState stateData;
    protected Vector3 patrolPoint;


    public WalkState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_WalkState stateData) : base(stateMachine, entity, animationName)
    {
        this.stateData = stateData;        
    }

    public override void Enter()
    {
        base.Enter();
        patrolPoint = entity.Patrol();
        entity.transform.LookAt(patrolPoint);
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
        entity.GoTo(stateData.movementSpeed);
    }

    public override void BoolChecks()
    {
        base.BoolChecks();
    }

}
