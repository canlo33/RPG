using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider_AttackState : AttackState
{
    private GreenSpider greenSpider;
    public GreenSpider_AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_AttackState stateData, GreenSpider greenSpider) : base(stateMachine, entity, animationName, stateData)
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
        EnemyHasDied(greenSpider.dieState);
        if (Time.time >= startTime + entity.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerHealthSystem.Damage(stateData.basicAttackDamage);            
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
