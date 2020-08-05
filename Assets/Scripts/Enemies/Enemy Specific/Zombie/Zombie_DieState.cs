using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_DieState : DieState
{
    private Zombie zombie;
    public Zombie_DieState(FiniteStateMachine stateMachine, Entity entity, string animationName, Zombie zombie) : base(stateMachine, entity, animationName)
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
        animationID = Random.Range(1, 3);
        entity.animator.SetInteger("animationID", animationID);
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
            entity.animator.enabled = false;
            entity.GetComponent<Zombie>().enabled = false;
            Object.Destroy(entity.gameObject,2f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
