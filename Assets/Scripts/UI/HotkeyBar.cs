using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotkeyBar : MonoBehaviour
{
    public Hotkey[] hotkey;
    public Ability[] ability = new Ability[9];
    private MageController playerController;
    public delegate void OnHotkeyBarUpdated();
    public OnHotkeyBarUpdated onHotkeyBarUpdatedCallBack;
    private void Start()
    {
        hotkey = GetComponentsInChildren<Hotkey>();
        ability = AbilityManager.instance.playerAbility;
        playerController = GameMaster.instance.playerController;
        for (int i = 0; i < ability.Length; i++)
        {
            hotkey[i].ability = ability[i];
        }
        UpdateHotkeys();
    }
    private void Update()
    {
        if (!playerController.canCast || !playerController.enabled)
            return;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hotkey[0].ability != null)
                hotkey[0].ability.Cast(playerController);
            else if (hotkey[0].potion != null)
                hotkey[0].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hotkey[1].ability != null)
                hotkey[1].ability.Cast(playerController);
            else if (hotkey[1].potion != null)
                hotkey[1].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (hotkey[2].ability != null)
                hotkey[2].ability.Cast(playerController);
            else if (hotkey[2].potion != null)
                hotkey[2].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (hotkey[3].ability != null)
                hotkey[3].ability.Cast(playerController);
            else if (hotkey[3].potion != null)
                hotkey[3].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (hotkey[4].ability != null)
                hotkey[4].ability.Cast(playerController);
            else if (hotkey[4].potion != null)
                hotkey[4].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (hotkey[5].ability != null)
                hotkey[5].ability.Cast(playerController);
            else if (hotkey[5].potion != null)
                hotkey[5].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (hotkey[6].ability != null)
                hotkey[6].ability.Cast(playerController);
            else if (hotkey[6].potion != null)
                hotkey[6].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            if (hotkey[7].ability != null)
                hotkey[7].ability.Cast(playerController);
            else if (hotkey[7].potion != null)
                hotkey[7].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if (hotkey[8].ability != null)
                hotkey[8].ability.Cast(playerController);
            else if (hotkey[8].potion != null)
                hotkey[8].potion.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (hotkey[9].ability != null)
                hotkey[9].ability.Cast(playerController);
            else if (hotkey[9].potion != null)
                hotkey[9].potion.Use();
        }
    }
    void UpdateHotkeys()
    {
        foreach (var hotkey in hotkey)
        {
            hotkey.UpdateHotkeyUI();
        }
    }
}
