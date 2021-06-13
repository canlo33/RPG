using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one DialogueManager Instance found");
            Destroy(this);
            return;
        }
        instance = this;
    }

    #endregion
    public Queue<string> Dialogue { get; set; }

}
