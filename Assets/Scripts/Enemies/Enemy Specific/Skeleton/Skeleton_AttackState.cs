using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AttackState : AttackState
{
    private Skeleton skeleton;
    public Skeleton_AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Skeleton skeleton) : base(stateMachine, entity, animationName)
    {
        this.skeleton = skeleton;
        damageTimer = .75f;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }
    public override void Enter()
    {        
        base.Enter();
        didAttack = false;
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
            if (Time.time >= startTime + 1.77f)
        {
            if (isPlayerInAttackRange)
                stateMachine.ChangeState(skeleton.attackState);
            else
                stateMachine.ChangeState(skeleton.chasePlayerState);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isEnemyDead)
            stateMachine.ChangeState(skeleton.dieState);
        else if (isPlayerDead)
            stateMachine.ChangeState(skeleton.walkState);
    }
}
