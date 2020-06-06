using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newWalkStateData", menuName = "Data/State Data/WalkState")]
public class Data_WalkState : ScriptableObject
{
    public float movementSpeed = 3f;
    public float patrolRange = 5f;
 
}
