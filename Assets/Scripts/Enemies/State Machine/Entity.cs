using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;
    public Rigidbody rb { get; private set; }
    public Animator animator { get; private set; }

    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform wallCheck;
    public Data_Entity entityData;
    public Vector3 startPosition;
    public Transform player;
 
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();
        startPosition = transform.position;
        player = GameObject.Find("Player").transform;        
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
       Vector3 patrolPoint = new Vector3(Random.Range(startPosition.x - entityData.patrolRange, startPosition.x + entityData.patrolRange), 0f, Random.Range(startPosition.z - entityData.patrolRange, startPosition.z + entityData.patrolRange));
        while (Vector3.Distance(startPosition, patrolPoint) <= 2.5f)
            {
                patrolPoint = new Vector3(Random.Range(startPosition.x - entityData.patrolRange, startPosition.x + entityData.patrolRange), 0f, Random.Range(startPosition.z - entityData.patrolRange, startPosition.z + entityData.patrolRange));
            }
        Debug.Log(Vector3.Distance(startPosition, patrolPoint));
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
