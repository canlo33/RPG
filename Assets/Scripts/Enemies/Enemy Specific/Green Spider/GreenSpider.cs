using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSpider : Entity
{
    public GreenSpider_IdleState idleState { get; private set; }
    public GreenSpider_WalkState walkState { get; private set; }
    public GreenSpider_ChasePlayerState chasePlayerState { get; private set; }
    public GreenSpider_AttackState attackState { get; private set; }
    public GreenSpider_DieState dieState { get; private set; }

    [SerializeField]
    private Data_WalkState walkStateData;
    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_ChasePlayerState chasePlayerStateData;
    public override void Start()
    {
        base.Start();
        walkState = new GreenSpider_WalkState(stateMachine, this, "walk", walkStateData, this);
        idleState = new GreenSpider_IdleState(stateMachine, this, "idle", idleStateData, this);
        chasePlayerState = new GreenSpider_ChasePlayerState(stateMachine, this, "run", chasePlayerStateData, this);
        attackState = new GreenSpider_AttackState(stateMachine, this, "attack", this);
        dieState = new GreenSpider_DieState(stateMachine, this, "die", this);
        respownState = idleState;
        stateMachine.Initialize(walkState);
    }
}
