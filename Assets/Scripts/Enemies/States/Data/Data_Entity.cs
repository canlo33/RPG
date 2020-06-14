using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/EntityData")]
public class Data_Entity : ScriptableObject
{
    public float patrolRange = 5f;
    public float wallCheckDistance = 1f;
    public float ledgeCheckDistance = .2f;
    public float minAgroRange = 5f;
    public float maxAgroRange = 10f;
    public float attackRange = 2f;

    public LayerMask ground;
    public LayerMask wall;
    public LayerMask player;
}
