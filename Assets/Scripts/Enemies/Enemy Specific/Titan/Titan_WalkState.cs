using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_WalkState : WalkState
{
    private Titan titan;
    public Titan_WalkState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_WalkState stateData, Titan titan) : base(stateMachine, entity, animationName, stateData)
    {
        this.titan = titan;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMaxAgroRange || isEnraged)
            stateMachine.ChangeState(titan.roarState);
        else if (Vector3.Distance(entity.transform.position, patrolPoint) <= 0.25f)
            stateMachine.ChangeState(titan.idleState);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
