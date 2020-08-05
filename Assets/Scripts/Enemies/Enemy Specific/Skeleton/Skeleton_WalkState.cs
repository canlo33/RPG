﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_WalkState : WalkState
{
    private Skeleton skeleton;
    public Skeleton_WalkState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_WalkState stateData, Skeleton skeleton) : base(stateMachine, entity, animationName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        EnemyHasDied(skeleton.dieState);
        if (isDetectingWall || !isDetectingLedge)
        {
            stateMachine.ChangeState(skeleton.idleState);
        }
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(skeleton.chasePlayerState);
        }

        else if (Vector3.Distance(entity.transform.position, patrolPoint) <= 0.3f)
        {
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}