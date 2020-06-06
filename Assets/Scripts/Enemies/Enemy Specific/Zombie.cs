using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    public Zombie_WalkState walkState { get; private set; }
    public Zombie_IdleState idleState { get; private set; }

    [SerializeField]
    private Data_WalkState walkStateData;
    [SerializeField]
    private Data_IdleState idleStateData;

    public override void Start()
    {
        base.Start();
        walkState = new Zombie_WalkState(stateMachine, this, "walk", walkStateData, this);
        idleState = new Zombie_IdleState(stateMachine, this, "idle", idleStateData, this);
        stateMachine.Initialize(idleState);
    }

}
