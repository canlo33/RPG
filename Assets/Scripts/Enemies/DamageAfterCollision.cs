using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAfterCollision : MonoBehaviour
{
    public int damageAmount;
    public bool isTrueDamage;
    private GameMaster gameMaster;
    private void Start()
    {
        gameMaster = GameMaster.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.CompareTag("Player"))
        {
            // Calculate a damage amount between 90% to 100% and apply to the player with player hit effect.
            int damage = (int)Random.Range(damageAmount * 0.9f, damageAmount);
            gameMaster.playerController.PlayerHit();
            gameMaster.playerStats.Damage(damage, isTrueDamage);            
        }
    }
}
