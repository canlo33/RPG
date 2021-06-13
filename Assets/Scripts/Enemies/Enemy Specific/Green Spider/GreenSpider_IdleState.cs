using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_IdleState : IdleState
{
    private GreenSpider greenSpider;
    public GreenSpider_IdleState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_IdleState stateData, GreenSpider greenSpider) : base(stateMachine, entity, animationName, stateData)
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
        if (isEnemyDead)
            stateMachine.ChangeState(greenSpider.dieState);
        if (isEnraged && !isEnemyDead)
            stateMachine.ChangeState(greenSpider.chasePlayerState);
        if (isIdleTimeOver)
            stateMachine.ChangeState(greenSpider.walkState);
        else if (isPlayerInMinAgroRange)
            stateMachine.ChangeState(greenSpider.chasePlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
