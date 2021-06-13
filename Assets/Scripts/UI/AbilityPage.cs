using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPage : MonoBehaviour
{
    public Texture2D mouseImage;
    public AbilityPanel[] panels;
    private Animator anim;
    private bool isOpen = false;
    private GameMaster gameMaster;
    private void Start()
    {
        gameMaster = GameMaster.instance;
        anim = GetComponent<Animator>();
        panels = GetComponentsInChildren<AbilityPanel>();
        gameMaster.OnPlayerLevelUpCallBack += UpdateUI;
        gameMaster.OnPlayerLevelUpCallBack.Invoke();
        UIManager.instance.onUITriggeredCallBack += CloseAbilityPage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isOpen)
                OpenAbilityPage();
            else
                CloseAbilityPage();
        }
    }
    public void OpenAbilityPage()
    {
        UIManager.instance.onUITriggeredCallBack.Invoke();
        gameMaster.MouseUnlock();
        anim.Play("Open");
        isOpen = true;
    }
    public void CloseAbilityPage()
    {
        if (!isOpen)
            return;
        gameMaster.MouseLock();
        anim.Play("Close");
        isOpen = false;
    }
    private void UpdateUI()
    {
        foreach (var panel in panels)
        {
            panel.UpdatePanelUI();
        }
    }

}
