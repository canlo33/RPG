using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_ChasePlayerState : ChasePlayerState
{
    private GreenSpider greenSpider;
    public GreenSpider_ChasePlayerState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_ChasePlayerState stateData, GreenSpider greenSpider) : base(stateMachine, entity, animationName, stateData)
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
        {
            stateMachine.ChangeState(greenSpider.dieState);
            return;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isPlayerInAttackRange)
        {
            stateMachine.ChangeState(greenSpider.attackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            entity.rb.velocity = Vector3.zero;
            stateMachine.ChangeState(greenSpider.idleState);
        }

    }
}
