using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_IdleState : IdleState
{
    private Titan titan;
    public Titan_IdleState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_IdleState stateData, Titan titan) : base(stateMachine, entity, animationName, stateData)
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
        animationID = Random.Range(1, 4);
        entity.animator.SetInteger("animationID", animationID);
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
        if (isIdleTimeOver)
            stateMachine.ChangeState(titan.walkState);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
