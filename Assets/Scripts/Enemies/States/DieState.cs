using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public delegate void OnDieState(int entityID);
    public static event OnDieState OnEnemyDied;

    public DieState(FiniteStateMachine stateMachine, Entity entity, string animationName) : base(stateMachine, entity, animationName)
    {
    }
    public override void BoolChecks()
    {
        base.BoolChecks();        
    }
    public override void Enter()
    {
        base.Enter();
        entity.isEnraged = false;
         entity.GetComponent<RespownAfterDeath>().isDead = true;
        entity.enabled = false;
        entity.rb.useGravity = false;
        entity.GetComponentInChildren<Collider>().enabled = false;
        entity.playerStats.currentExp += entity.entityData.expReward;
        entity.playerStats.LevelUp();
        if (OnEnemyDied != null)
            OnEnemyDied.Invoke(entity.entityData.mobID);
        if (entity.lootTable != null)
            entity.lootTable.RollLoot();
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
