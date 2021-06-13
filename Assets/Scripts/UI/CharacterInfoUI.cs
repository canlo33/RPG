using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfoUI : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerStats playerStats;
    private bool isOpen = false;
    public TextMeshProUGUI level;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI defense;
    public TextMeshProUGUI health;
    public TextMeshProUGUI mana;
    private Animator anim;
    void Start()
    {
        playerStats = GameMaster.instance.playerStats;
        anim = GetComponent<Animator>();
        InventoryManager.instance.onInventoryChangedCallBack += UpdateUI;
    }
    void UpdateUI()
    {
        level.text = "" + playerStats.level;
        attack.text = "" + playerStats.currentDamage;
        defense.text = "" + playerStats.currentDefense;
        health.text = "" + playerStats.maxHealth;
        mana.text = "" + playerStats.currentMana;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isOpen)
                OpenCharacterStats();
            else
                CloseCharacterStats();
        }
    }
    public void OpenCharacterStats()
    {
        GameMaster.instance.MouseUnlock();
        anim.Play("Open");
        UpdateUI();
        isOpen = true;
    }
    public void CloseCharacterStats()
    {
        GameMaster.instance.MouseLock();
        anim.Play("Close");
        isOpen = false;
    }
}
