using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_AoeAttackState : AttackState
{
    private Titan titan;
    public Titan_AoeAttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Titan titan) : base(stateMachine, entity, animationName)
    {
        this.titan = titan;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();        
    }
    public override void Enter()
    {
        animationID = 4;
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .95)
            stateMachine.ChangeState(titan.circleAroundState);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
