using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Data_AttackState stateData;
    public AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_AttackState stateData) : base(stateMachine, entity, animationName)
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
        entity.transform.LookAt(new Vector3(entity.player.position.x, entity.transform.position.y, entity.player.position.z));
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
