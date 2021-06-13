using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public delegate void OnUITriggered();
    public OnUITriggered onUITriggeredCallBack;
    #region Singleton
    public static UIManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one UIManager instance found");
            return;
        }
        instance = this;
    }
    #endregion

}
