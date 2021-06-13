using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_CircleAroundState : TransitionState
{
    private Titan titan;
    public Titan_CircleAroundState(FiniteStateMachine stateMachine, Entity entity, string animationName, Titan titan) : base(stateMachine, entity, animationName)
    {
        this.titan = titan;
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
        entity.transform.LookAt(new Vector3(entity.player.position.x, entity.transform.position.y, entity.player.position.z));
        if (isEnemyDead)
            stateMachine.ChangeState(titan.dieState);
        if (!isPlayerInMaxAgroRange && !entity.isEnraged)
        {
            entity.rb.velocity = Vector3.zero;
            stateMachine.ChangeState(titan.walkState);
        }
        if (Time.time >= startTime + 5f)
        {
            if(animationID == 2)
                stateMachine.ChangeState(titan.jumpAttackState);
            else
                stateMachine.ChangeState(titan.chasePlayerState);
            return;
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.transform.Translate(Vector3.left * 3.5f * Time.fixedDeltaTime);
    }
}
