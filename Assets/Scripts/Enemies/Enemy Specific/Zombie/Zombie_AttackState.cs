using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_AttackState : AttackState
{
    private Zombie zombie;
    public Zombie_AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Zombie zombie) : base(stateMachine, entity, animationName)
    {
        this.zombie = zombie;
        damageTimer = 1.25f;
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
        if (Time.time >= startTime + damageTimer && !didAttack)
        {
            didAttack = true;
            playerStats.Damage(entity.entityData.attackDamage, false);
            entity.playerController.PlayerHit();
            GameMaster.instance.CameraShake(3f, 0.1f);
        }
        if (Time.time >= startTime + 1.6f)
           stateMachine.ChangeState(zombie.chasePlayerState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isEnemyDead)
            stateMachine.ChangeState(zombie.dieState);
        else if (isPlayerDead)
            stateMachine.ChangeState(zombie.walkState);
    }
}
