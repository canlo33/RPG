using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected Data_IdleState stateData;
    protected float idleDuration;
    protected bool isIdleTimeOver;
    public IdleState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_IdleState stateData) : base(stateMachine, entity, animationName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0);
        isIdleTimeOver = false;
        SetRandomIdleDuration();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time > startTime + idleDuration)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetRandomIdleDuration()
    {
        idleDuration = Random.Range(stateData.minIdleDuration, stateData.maxIdleDuration);
    }

}
