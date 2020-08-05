using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_AttackState : AttackState
{
    private Zombie zombie;
    public Zombie_AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName,Data_AttackState stateData, Zombie zombie) : base(stateMachine, entity, animationName, stateData)
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
        EnemyHasDied(zombie.dieState);
        if (Time.time >= startTime + entity.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerHealthSystem.Damage(stateData.basicAttackDamage);
            if (isPlayerInAttackRange)
            {
                stateMachine.ChangeState(zombie.attackState);
            }
            else
            {
                stateMachine.ChangeState(zombie.chasePlayerState);
            }           
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();            
    }

}
