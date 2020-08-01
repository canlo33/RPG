using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_AttackState : AttackState
{
    private GreenSpider greenSpider;
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
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isEnemyDead)
        {
            stateMachine.ChangeState(greenSpider.dieState);
            return;
        }

        else if (Time.time >= startTime + entity.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerHealthSystem.Damage(greenSpider.attackStateData.basicAttackDamage);
            if (isPlayerInAttackRange)
            {
                stateMachine.ChangeState(greenSpider.attackState);

            }
            else
            {
                stateMachine.ChangeState(greenSpider.chasePlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
