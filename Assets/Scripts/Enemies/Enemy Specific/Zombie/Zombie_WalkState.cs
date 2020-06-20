using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_WalkState : WalkState
{
    private Zombie zombie;
    public Zombie_WalkState(FiniteStateMachine stateMachine, Entity entity, string animationName, Data_WalkState stateData, Zombie zombie) : base(stateMachine, entity, animationName, stateData)
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
        if (isEnemyDead)
        {
            stateMachine.ChangeState(zombie.dieState);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            stateMachine.ChangeState(zombie.idleState);
        }
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(zombie.chasePlayerState);
        }

        else if (Vector3.Distance(entity.transform.position, patrolPoint) <= 0.3f)
        {
            stateMachine.ChangeState(zombie.idleState);
        }
    }
      
    
}
