using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    public Zombie_WalkState walkState { get; private set; }
    public Zombie_IdleState idleState { get; private set; }
    public Zombie_ChasePlayerState chasePlayerState { get; private set; }
    public Zombie_AttackState attackState { get; private set; }
    public Zombie_DieState dieState { get; private set; }


    [SerializeField]
    private Data_WalkState walkStateData;
    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_ChasePlayerState chasePlayerStateData;

    public override void Start()
    {
        base.Start();
        walkState = new Zombie_WalkState(stateMachine, this, "walk", walkStateData, this);
        idleState = new Zombie_IdleState(stateMachine, this, "idle", idleStateData, this);        
        chasePlayerState = new Zombie_ChasePlayerState(stateMachine, this, "run", chasePlayerStateData, this);
        attackState = new Zombie_AttackState(stateMachine, this, "attack", this);
        dieState = new Zombie_DieState(stateMachine, this, "die", this);
        respownState = idleState;
        stateMachine.Initialize(walkState);
    }   
}
