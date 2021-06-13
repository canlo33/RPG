using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_StunState : TransitionState
{
    private Titan titan;
    public Titan_StunState(FiniteStateMachine stateMachine, Entity entity, string animationName, Titan titan) : base(stateMachine, entity, animationName)
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
        if (isEnemyDead)
            stateMachine.ChangeState(titan.dieState);
        if (Time.time >= startTime + 7f)
            stateMachine.ChangeState(titan.roarState);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
