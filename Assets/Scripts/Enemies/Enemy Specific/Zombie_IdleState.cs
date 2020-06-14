using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_IdleState : IdleState
{
    private Zombie zombie;
    public Zombie_IdleState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_IdleState stateData, Zombie zombie) : base(stateMachine, entity, animationName, stateData)
    {
        this.zombie = zombie;
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

        if(isIdleTimeOver)
        {
            stateMachine.ChangeState(zombie.walkState);
        }
        else if(isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(zombie.chasePlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
