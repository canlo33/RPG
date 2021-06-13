using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan_RoarState : TransitionState
{
    private Titan titan;
    private bool isRoared;
    public Titan_RoarState(FiniteStateMachine stateMachine, Entity entity, string animationName, Titan titan) : base(stateMachine, entity, animationName)
    {
        this.titan = titan;
    }
    public override void BoolChecks()
    {
        base.BoolChecks();
    }
    public override void Enter()
    {
        base.Enter();
        isRoared = false;
        entity.transform.LookAt(new Vector3(entity.player.position.x, entity.transform.position.y, entity.player.position.z));
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .25 && !isRoared)
        {
            GameMaster.instance.CameraShake(2f, 3.5f);
            isRoared = true;            
        }            
        if (entity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .95)
        {
            if(entity.healthSystem.GetHealthPercentage() > 90)
                stateMachine.ChangeState(titan.circleAroundState);
            else
            stateMachine.ChangeState(titan.aoeAttackState);
        }            
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
