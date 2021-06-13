using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    //Player related
    public new string name;
    public int level = 1;
    public int currentExp = 0;
    public int maxExp = 100;
    public int maxLevel = 10;
    public int maxHealth = 100;
    public int maxMana = 100;
    public int currentHealth = 100;
    public int currentMana = 100;
    public bool isInvulnerable = false;
    //Stats
    public int baseDamage = 50;
    public int baseDefense = 50;
    public int currentDamage = 50;
    public int currentDefense = 50;
    //Equipment Bonus
    public int itemDamage;
    public int itemDefense;
    public int itemHealth;
    public int itemMana;
    //Caching
    private GameMaster gameMaster;
    private StartManager startManager;
    public void LevelUp()
    {
        if (level == maxLevel)
        {
            currentExp = maxExp;
            return;
        }
        if(currentExp >= maxExp)
        {
            SetLevel(level+1);
            if (gameMaster.OnPlayerLevelUpCallBack != null)
                gameMaster.OnPlayerLevelUpCallBack.Invoke();
        }
    }
    public void SetLevel(int i)
    {
        if (i > 10)
            i = 10;
        level = i;
        maxHealth = (100 * i) + itemHealth;
        maxMana = (100 * i) + itemMana;
        baseDamage = 25;
        baseDefense = 50;
        baseDamage += (int)(baseDamage * i * 0.25f);
        baseDefense += (int)(baseDefense * i * 0.75f);
        currentDamage = baseDamage + itemDamage;
        currentDefense = baseDefense + itemDefense;
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentExp = 0;
        maxExp = 100 * i * i;
        isInvulnerable = false;
    }
    public void Reset()
    {
        gameMaster = GameMaster.instance;
        startManager = StartManager.instance;
        itemDamage = 0;
        itemDefense = 0;
        itemHealth = 0;
        itemMana = 0;
        SetLevel(1);
    }
    public void Damage(int amount, bool byPassInvulnerable)
    {
        if (!byPassInvulnerable && isInvulnerable)
            return;
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public void Mana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
            currentMana = maxMana;
    }
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth * 100f;
    }
    public float GetManaPercentage()
    {
        return (float)currentMana / maxMana * 100f;
    }
    public void SaveProgress()
    {
        startManager.savedData.playerName = name;
        startManager.savedData.playerLevel = level;
        startManager.savedData.currentPlayerExp = currentExp;
        startManager.savedData.playerPosition = new Vector3(515f, 10, 475f);
    }
    public void LoadProgress()
    {       
        SetLevel(startManager.savedData.playerLevel);
        currentExp = startManager.savedData.currentPlayerExp;
        gameMaster.player.transform.position = startManager.savedData.playerPosition;
    }
}

