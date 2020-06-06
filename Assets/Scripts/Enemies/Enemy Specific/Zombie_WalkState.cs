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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}
