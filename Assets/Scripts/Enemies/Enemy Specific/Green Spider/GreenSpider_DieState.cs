using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_DieState : DieState
{
    private GreenSpider greenSpider;
    public GreenSpider_DieState(FiniteStateMachine stateMachine, Entity entity, string animationName, GreenSpider greenSpider) : base(stateMachine, entity, animationName)
    {
        this.greenSpider = greenSpider;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
