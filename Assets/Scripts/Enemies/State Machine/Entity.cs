using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;
    public Rigidbody rb { get; private set; }
    public Animator animator { get; private set; }

    private Vector3 velocityWorkspace;

    protected Vector3 startPosition;
 
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
        startPosition = transform.position;

    }
    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x , rb.velocity.y, velocity);
        rb.velocity = velocityWorkspace;
    }
 


}
