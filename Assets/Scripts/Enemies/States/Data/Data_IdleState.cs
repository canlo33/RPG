using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWalkStateData", menuName = "Data/State Data/IdleState")]
public class Data_IdleState : ScriptableObject
{
    public float minIdleDuration = 3f;
    public float maxIdleDuration = 5f;
}
