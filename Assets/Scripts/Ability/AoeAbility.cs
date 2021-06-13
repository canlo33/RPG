using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAbility : MonoBehaviour
{
    public Ability ability;
    public LayerMask enemyLayer;
    public int repeatAmount;
    private PlayerStats playerStats;

    // Update is called once per frame
    void Start()
    {
        playerStats = GameMaster.instance.playerStats;
        StartCoroutine(DamageEnemiesAround(repeatAmount));
    }
    IEnumerator DamageEnemiesAround(int repeatAmount)
    {
        int currentRepeat = 0;
        while(currentRepeat < repeatAmount)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, ability.abilityRadius, enemyLayer);
            foreach (var enemy in colliders)
            {
                Entity entity = enemy.transform.parent.GetComponent<Entity>();
                if (entity != null && entity.enabled)
                {
                    entity.isEnraged = true;
                    float damageAmount = (playerStats.currentDamage * ability.damageMultiplier) + ability.baseDamage;
                    damageAmount = Random.Range(damageAmount * .85f, damageAmount * 1.15f);                    
                    if (entity.healthSystem != null)
                        entity.healthSystem.Damage((int)damageAmount);
                    entity.DamageTextPopup((int)damageAmount);
                }                
            }
            currentRepeat++;
            yield return new WaitForSeconds(2f);
        }
        
    }
}
