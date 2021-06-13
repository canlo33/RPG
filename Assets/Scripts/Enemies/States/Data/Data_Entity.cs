using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/EntityData")]
public class Data_Entity : ScriptableObject
{
    public string mobName = "Please Name Me";
    public int mobID;
    public float patrolRange = 5f;
    public float minAgroRange = 5f;
    public float maxAgroRange = 10f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public int maxHealth = 100;
    public int expReward = 50;
}
