using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_ChasePlayerState : ChasePlayerState
{
    private Titan titan;
    public Titan_ChasePlayerState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_ChasePlayerState stateData, Titan titan) : base(stateMachine, entity, animationName, stateData)
    {
        this.titan = titan;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }
    public override void Enter()
    {
        animationID = entity.animator.GetInteger("animationID");
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isPlayerInAttackRange)
        {
            switch (animationID)
            {
                case 1: stateMachine.ChangeState(titan.comboAttackState);
                    break;
                case 3:
                    stateMachine.ChangeState(titan.spinAttackState);
                    break;
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
