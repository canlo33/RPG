using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_SpinAttackState : AttackState
{
    private Titan titan;
    public Titan_SpinAttackState(FiniteStateMachine stateMachine, Entity entity, string animationName, Titan titan) : base(stateMachine, entity, animationName)
    {
        this.titan = titan;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }
    public override void Enter()
    {
        animationID = 1;
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Rotate towards to the player.
        entity.transform.LookAt(new Vector3(entity.player.position.x, entity.transform.position.y, entity.player.position.z));
        //Check if the animation is done.
        if (entity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .95)
            stateMachine.ChangeState(titan.circleAroundState);
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
