using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;
    protected float animationTime;
    protected int animationID = 1;
    protected string animationName;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool isPlayerInAttackRange;
    protected bool isEnemyDead = false;
    protected bool isEnraged = false;
    protected bool isPlayerDead = false;
    protected PlayerStats playerStats;    

    public State(FiniteStateMachine stateMachine, Entity entity , string animationName)
    {
        this.stateMachine = stateMachine;
        this.entity = entity;
        this.animationName = animationName;
        playerStats = entity.playerStats;        
    }
    public virtual void Enter()
    {
        entity.animator.SetInteger("animationID", animationID);
        entity.animator.SetBool(animationName, true);
        startTime = Time.time;
    }
    public virtual void Exit()
    {
        entity.animator.SetBool(animationName, false);
    }
    public virtual void LogicUpdate()
    {
        BoolChecks();
    }
    public virtual void PhysicsUpdate()
    {        
    }
    public virtual void BoolChecks()
    {
        isPlayerInMinAgroRange = entity.IsPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.IsPlayerInMaxAgroRange();
        isPlayerInAttackRange = entity.IsPlayerInAttackRange();
        isPlayerDead = playerStats.currentHealth == 0;
        isEnemyDead = entity.IsEnemyDead();
        isEnraged = entity.isEnraged;
        if (isPlayerDead)
            entity.isEnraged = false;
    }

}
