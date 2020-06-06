using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;
    protected string animationName;

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

    }

}
