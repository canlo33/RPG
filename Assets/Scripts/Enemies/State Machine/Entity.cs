using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;
    public Rigidbody rb { get; private set; }
    public Animator animator { get; private set; }
    public HealthSystem healthSystem { get; private set; }
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Data_Entity entityData;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform wallCheck;
    private Vector3 startPosition;
    public Transform player;  
 
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new FiniteStateMachine();
        StartPosition = transform.position;
        player = GameObject.Find("Player").transform;
        healthSystem = new HealthSystem(entityData.maxHealth);
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();        
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    public virtual void GoTo(float speed)
    {
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
    public virtual Vector3 Patrol()
    {        
       Vector3 patrolPoint = new Vector3(Random.Range(StartPosition.x - entityData.patrolRange, StartPosition.x + entityData.patrolRange), 0f, Random.Range(StartPosition.z - entityData.patrolRange, StartPosition.z + entityData.patrolRange));
        while (Vector3.Distance(transform.position, patrolPoint) <= 5f)
            {
                patrolPoint = new Vector3(Random.Range(StartPosition.x - entityData.patrolRange, StartPosition.x + entityData.patrolRange), 0f, Random.Range(StartPosition.z - entityData.patrolRange, StartPosition.z + entityData.patrolRange));
            }
        return patrolPoint;        
    }    
    public virtual bool IsDetectingLedge()
    {
        return Physics.Raycast(ledgeCheck.position, -transform.up, entityData.ledgeCheckDistance, entityData.ground);
    }
    public virtual bool IsDetectingWall()
    {
        return Physics.Raycast(wallCheck.position, transform.forward, entityData.wallCheckDistance, entityData.wall);
    }
    public virtual bool IsPlayerInMinAgroRange()
    {
         return Vector3.Distance(transform.position, player.transform.position) <= entityData.minAgroRange;        
    }
    public virtual bool IsPlayerInMaxAgroRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= entityData.maxAgroRange;
    }
    public virtual bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= entityData.attackRange;
    }
    public virtual bool IsEnemyDead()
    {
        return healthSystem.GetHealth() == 0;
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, entityData.minAgroRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, entityData.maxAgroRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, entityData.attackRange);

    }
   


}
