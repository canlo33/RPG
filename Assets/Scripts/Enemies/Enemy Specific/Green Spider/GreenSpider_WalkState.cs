using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_WalkState : WalkState
{
    private GreenSpider greenSpider;
    public GreenSpider_WalkState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_WalkState stateData, GreenSpider greenSpider) : base(stateMachine, entity, animationName, stateData)
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
        if (isDetectingWall || !isDetectingLedge)
        {
            stateMachine.ChangeState(greenSpider.idleState);
        }
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(greenSpider.chasePlayerState);
        }

        else if (Vector3.Distance(entity.transform.position, patrolPoint) <= 0.3f)
        {
            stateMachine.ChangeState(greenSpider.idleState);
        }
    }
}
