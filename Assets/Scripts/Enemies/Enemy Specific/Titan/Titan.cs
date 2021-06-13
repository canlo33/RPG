using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titan : Entity
{
    public Titan_IdleState idleState { get; private set; }
    public Titan_WalkState walkState { get; private set; }
    public Titan_ChasePlayerState chasePlayerState { get; private set; }
    public Titan_JumpAttackState jumpAttackState { get; private set; }
    public Titan_ComboAttackState comboAttackState { get; private set; }
    public Titan_SpinAttackState spinAttackState { get; private set; }
    public Titan_CircleAroundState circleAroundState { get; set; }
    public Titan_AoeAttackState aoeAttackState { get; set; }
    public Titan_RoarState roarState { get; set; }
    public Titan_StunState stunState { get; set; }
    public Titan_DieState dieState { get; private set; }
    private bool[] isStunned = new bool[2];

    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_WalkState walkStateData;
    [SerializeField]
    private Data_ChasePlayerState chasePlayerStateData;
    public override void Start()
    {
        base.Start();
        idleState = new Titan_IdleState(stateMachine, this, "idle", idleStateData, this);
        walkState = new Titan_WalkState(stateMachine, this, "walk", walkStateData, this);
        jumpAttackState = new Titan_JumpAttackState(stateMachine, this, "attack", this);
        comboAttackState = new Titan_ComboAttackState(stateMachine, this, "attack", this);
        spinAttackState = new Titan_SpinAttackState(stateMachine, this, "attack", this);
        aoeAttackState = new Titan_AoeAttackState(stateMachine, this, "attack", this);
        circleAroundState = new Titan_CircleAroundState(stateMachine, this, "circleAround", this);
        chasePlayerState = new Titan_ChasePlayerState(stateMachine, this, "run", chasePlayerStateData, this);
        stunState = new Titan_StunState(stateMachine, this, "stun", this);
        roarState = new Titan_RoarState(stateMachine, this, "roar", this);
        dieState = new Titan_DieState(stateMachine, this, "die", this);
        respownState = idleState;
        stateMachine.Initialize(idleState);
    }
    public override void Update()
    {
        base.Update();
        Stunned();
        if (Input.GetKeyDown(KeyCode.X))
            healthSystem.Damage(healthSystem.maxHealth / 4);
    }
    private void Stunned()
    {
        if (isStunned[0] && isStunned[1])
            return;
        else if (!isStunned[0] && healthSystem.GetHealthPercentage() <= 75)
        {
            isStunned[0] = true;
            stateMachine.ChangeState(stunState);
        }
        else if (!isStunned[1] && healthSystem.GetHealthPercentage() <= 30)
        {
            isStunned[1] = true;
            stateMachine.ChangeState(stunState);
        }
    }

}
