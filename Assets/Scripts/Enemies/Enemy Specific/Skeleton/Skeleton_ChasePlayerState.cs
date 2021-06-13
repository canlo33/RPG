using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_ChasePlayerState : ChasePlayerState
{
    private Skeleton skeleton;
    public Skeleton_ChasePlayerState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_ChasePlayerState stateData, Skeleton skeleton) : base(stateMachine, entity, animationName, stateData)
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
        if (isEnemyDead)
        {
            stateMachine.ChangeState(skeleton.dieState);
        }
        else if (isPlayerInAttackRange)
        {
            stateMachine.ChangeState(skeleton.attackState);
        }
        if (!isPlayerInMaxAgroRange && !isEnraged)
        {
            entity.rb.velocity = Vector3.zero;
            stateMachine.ChangeState(skeleton.walkState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
