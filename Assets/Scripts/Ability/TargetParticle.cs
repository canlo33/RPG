using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParticle : MonoBehaviour
{
    public Ability ability;
    public GameObject hit;
    public float speed;
    [HideInInspector]public Transform target;
    private PlayerStats playerStats;

    [Space]
    [Header("PROJECTILE ROUTE")]
    private float randomUpAngle;
    private float randomSideAngle;
    public float sideAngle = 25;
    public float upAngle = 25;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameMaster.instance.playerStats;
        RandomAngle();
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 forward = ((target.position) - transform.position);
        Vector3 crossDirection = Vector3.Cross(forward, Vector3.up);
        Quaternion randomDeltaRotation = Quaternion.Euler(0, randomSideAngle, 0) * Quaternion.AngleAxis(randomUpAngle, crossDirection);
        offset = new Vector3(0f, target.localScale.y, 0f);
        Vector3 direction = randomDeltaRotation * (target.position - transform.position + offset);
        float distanceThisFrame = Time.deltaTime * speed;
        // Already reached the target.
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.rotation = Quaternion.LookRotation(direction);
    }
    void RandomAngle()
    {
        randomUpAngle = Random.Range(0, upAngle);
        randomSideAngle = Random.Range(-sideAngle, sideAngle);
    }
    public void UpdateTarget(Transform targetPosition)
    {
        target = targetPosition;
    }
    public void HitTarget()
    {
        Destroy(gameObject);
        GameObject particle = Instantiate(hit, target.position + offset, transform.rotation);
        ParticleSystem particleSystem = particle.transform.GetChild(0).GetComponent<ParticleSystem>();
        Destroy(particle, particleSystem.main.duration);
        Entity entity = target.GetComponent<Entity>();
        entity.isEnraged = true;
        float damageAmount = (playerStats.currentDamage * ability.damageMultiplier) + ability.baseDamage;
        damageAmount = Random.Range(damageAmount * .85f, damageAmount * 1.15f);
        entity.healthSystem.Damage((int)damageAmount);
        entity.DamageTextPopup((int)damageAmount);
    }
}
