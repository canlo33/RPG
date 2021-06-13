using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_AttackState : AttackState
{
    private GreenSpider greenSpider;
    private float stateDuration = 1.41f;
    public GreenSpider_AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, GreenSpider greenSpider) : base(stateMachine, entity, animationName)
    {
        this.greenSpider = greenSpider;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }

    public override void Enter()
    {
        animationID = Random.Range(1, 4);
        base.Enter();
    }

    public override void Exit()
    {
        playerStats.Damage(entity.entityData.attackDamage, false);
        entity.playerController.PlayerHit();
        GameMaster.instance.CameraShake(3f, 0.1f);
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateDuration)
        {
            if (isPlayerInAttackRange)
                stateMachine.ChangeState(greenSpider.attackState);
            else
                stateMachine.ChangeState(greenSpider.chasePlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isEnemyDead)
            stateMachine.ChangeState(greenSpider.dieState);
        else if (isPlayerDead)
            stateMachine.ChangeState(greenSpider.walkState);
    }
}
