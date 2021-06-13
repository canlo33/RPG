using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Ability[] allAbility;
    public Ability[] playerAbility;

    #region Singleton
    public static AbilityManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than once Equipment Manager instance found");
            return;
        }
        instance = this;
    }
    #endregion
}
