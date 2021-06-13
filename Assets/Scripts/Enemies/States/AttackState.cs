using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public float damageTimer;
    protected bool didAttack = false;
    public AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName) : base(stateMachine, entity, animationName)
    {
       
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }

    public override void Enter()
    {
        base.Enter();
        didAttack = false;
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
