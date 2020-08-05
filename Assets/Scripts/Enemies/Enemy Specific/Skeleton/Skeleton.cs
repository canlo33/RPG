using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Entity
{
    public Skeleton_IdleState idleState { get; private set; }
    public Skeleton_WalkState walkState { get; private set; }
    public Skeleton_ChasePlayerState chasePlayerState { get; private set; }
    public Skeleton_AttackState attackState { get; private set; }
    public Skeleton_DieState dieState { get; private set; }

    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_WalkState walkStateData;
    [SerializeField]
    private Data_ChasePlayerState chasePlayerStateData;
    [SerializeField]
    public Data_AttackState attackStateData;


    public override void Start()
    {
        base.Start();
        idleState = new Skeleton_IdleState(stateMachine, this, "idle", idleStateData, this);
        walkState = new Skeleton_WalkState(stateMachine, this, "walk", walkStateData, this);
        chasePlayerState = new Skeleton_ChasePlayerState(stateMachine, this, "run", chasePlayerStateData, this);
        walkState = new Skeleton_WalkState(stateMachine, this, "walk", walkStateData, this);
        attackState = new Skeleton_AttackState(stateMachine, this, "attack", attackStateData, this);
        dieState = new Skeleton_DieState(stateMachine, this, "die", this);
        stateMachine.Initialize(idleState);

    }
}
