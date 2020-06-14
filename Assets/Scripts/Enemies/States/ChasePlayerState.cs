using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : State
{
    protected Data_ChasePlayerState stateData;

    public ChasePlayerState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_ChasePlayerState stateData) : base(stateMachine, entity, animationName)
    {
        this.stateData = stateData;
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
        entity.transform.LookAt(new Vector3(entity.player.position.x, entity.transform.position.y, entity.player.position.z));
        entity.GoTo(stateData.movementSpeed);

    }
}
