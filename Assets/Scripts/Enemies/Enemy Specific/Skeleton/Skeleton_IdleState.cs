using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_IdleState : IdleState
{
    private Skeleton skeleton;
    public Skeleton_IdleState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_IdleState stateData, Skeleton skeleton) : base(stateMachine, entity, animationName, stateData)
    {
        this.skeleton = skeleton;
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
        EnemyHasDied(skeleton.dieState);
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(skeleton.walkState);
        }
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(skeleton.chasePlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
