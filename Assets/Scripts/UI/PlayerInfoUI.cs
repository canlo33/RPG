using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Slider healthBar;
    public Text healthBarText;
    public Slider manaBar;
    public Text manaBarText;
    public Slider xpBar;
    public Text xpBarText;
    public PlayerStats playerStats;
    public TextMeshProUGUI playerNameText;
    public Text playerLevelText;

    private void Update()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, playerStats.GetHealthPercentage(), 3f * Time.deltaTime);
        healthBarText.text = playerStats.currentHealth + "/" + playerStats.maxHealth;
        manaBar.value = Mathf.Lerp(manaBar.value, playerStats.GetManaPercentage(), 3f * Time.deltaTime);
        manaBarText.text = playerStats.currentMana + "/" + playerStats.maxMana;
        xpBar.value = Mathf.Lerp(xpBar.value, (float)playerStats.currentExp/ playerStats.maxExp, 3f * Time.deltaTime);
        xpBarText.text = playerStats.currentExp + "/" + playerStats.maxExp + "EXP";
        playerLevelText.text = "" + playerStats.level;
        playerNameText.text = playerStats.name;
    }

}
