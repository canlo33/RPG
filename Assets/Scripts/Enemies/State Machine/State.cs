using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;
    protected float animationTime;
    protected string animationName;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isPlayerInAttackRange;

    public State(FiniteStateMachine stateMachine, Entity entity , string animationName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animationName = animationName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.animator.SetBool(animationName, true);
        BoolChecks();
    }
    public virtual void Exit()
    {
        entity.animator.SetBool(animationName, false);
    }

    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        BoolChecks();
    }

    public virtual void BoolChecks()
    {
        isDetectingLedge = entity.IsDetectingLedge();
        isDetectingWall = entity.IsDetectingWall();
        isPlayerInMinAgroRange = entity.IsPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.IsPlayerInMaxAgroRange();
        isPlayerInAttackRange = entity.IsPlayerInAttackRange();
    }

}
