using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AttackState : AttackState
{
    private Skeleton skeleton;
    public Skeleton_AttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_AttackState stateData, Skeleton skeleton) : base(stateMachine, entity, animationName, stateData)
    {
        this.skeleton = skeleton;
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
        EnemyHasDied(skeleton.dieState);
        if (Time.time >= startTime + entity.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerHealthSystem.Damage(stateData.basicAttackDamage);
            if (isPlayerInAttackRange)
            {                
                stateMachine.ChangeState(skeleton.attackState);
            }
            else
            {
                stateMachine.ChangeState(skeleton.chasePlayerState);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
