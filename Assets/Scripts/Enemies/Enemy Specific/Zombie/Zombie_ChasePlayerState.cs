using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_ChasePlayerState : ChasePlayerState
{
    private Zombie zombie;
    public Zombie_ChasePlayerState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_ChasePlayerState stateData, Zombie zombie) : base(stateMachine, entity, animationName, stateData)
    {
        this.zombie = zombie;
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
            stateMachine.ChangeState(zombie.dieState);
            return;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isPlayerInAttackRange)
        {
            stateMachine.ChangeState(zombie.attackState);
        }
        else if (!isPlayerInMaxAgroRange)
        {
            entity.rb.velocity = Vector3.zero;
            stateMachine.ChangeState(zombie.idleState);
        }

    }
}
