using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_DieState : DieState
{
    private Skeleton skeleton;
    public Skeleton_DieState(FiniteStateMachine stateMachine, Entity entity, string animationName, Skeleton skeleton) : base(stateMachine, entity, animationName)
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
    }
}
