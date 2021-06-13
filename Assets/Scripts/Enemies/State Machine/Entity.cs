using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Entity : MonoBehaviour
{
    protected FiniteStateMachine stateMachine;
    public IdleState respownState;
    public Rigidbody rb { get; private set; }
    public LootTable lootTable { get; private set; }
    public Animator animator { get; private set; }
    public HealthSystem healthSystem;
    private new Renderer renderer;
    public Transform player;
    public PlayerStats playerStats;
    public MageController playerController;
    private TextMeshPro damageText;
    private Vector2 damageTextPosition;
    private Animator damageTextAnimator;
    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }
    public Data_Entity entityData;
    private Vector3 startPosition;
    public bool isEnraged = false;
    public float respownTimer = 10f;
    private void Awake()
    {
        healthSystem = new HealthSystem(entityData.maxHealth);
    }
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        stateMachine = new FiniteStateMachine();
        if(GetComponent<LootTable>() != null)
            lootTable = GetComponent<LootTable>();
        StartPosition = transform.position;
        player = GameMaster.instance.player.transform;
        playerController = player.GetComponent<MageController>();
        playerStats = playerController.playerStats;
        renderer = GetComponentInChildren<Renderer>();
        damageText = transform.Find("DamageText").GetComponentInChildren<TextMeshPro>();
        damageTextAnimator = damageText.transform.parent.GetComponent<Animator>();
        damageTextPosition = damageText.transform.parent.localPosition;
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
       Vector3 patrolPoint = new Vector3(Random.Range(StartPosition.x - entityData.patrolRange, StartPosition.x + entityData.patrolRange), transform.position.y, Random.Range(StartPosition.z - entityData.patrolRange, StartPosition.z + entityData.patrolRange));
        while (Vector3.Distance(transform.position, patrolPoint) <= entityData.patrolRange)
            {
                patrolPoint = new Vector3(Random.Range(StartPosition.x - entityData.patrolRange, StartPosition.x + entityData.patrolRange), transform.position.y, Random.Range(StartPosition.z - entityData.patrolRange, StartPosition.z + entityData.patrolRange));
            }
        return patrolPoint;        
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
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, entityData.minAgroRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, entityData.maxAgroRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, entityData.attackRange);
    }
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Mob" || collider.gameObject.tag == "Player")
            Physics.IgnoreCollision(GetComponentInChildren<Collider>(), collider.collider);
    }
    public void FadeAlpha()
    {
        Color col = renderer.material.color;
        if (col.a > 0)
        {
            col.a = Mathf.Lerp(col.a, 0, 1f * Time.deltaTime);
            renderer.material.color = col;
        }            
    }
    public void Respown()
    {
        healthSystem.Heal(entityData.maxHealth);
        GetComponent<RespownAfterDeath>().isFading = false;
        Color col = renderer.material.color;
        col.a = 1f;
        renderer.material.color = col;
        transform.position = startPosition;
        stateMachine.ChangeState(respownState);
        animator.Play("Idle");
        GetComponentInChildren<Collider>().enabled = true;
        this.enabled = true;
        rb.useGravity = true;
        isEnraged = false;
    }
    public void DamageTextPopup(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        var x = Random.Range(-0.25f, 0.25f);
        damageTextAnimator.Play("Popup");
        damageText.transform.parent.localPosition = new Vector2(x, 0) + damageTextPosition;
    }

}
