using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Items/Potion")]
[System.Serializable]
public class Potion : Item
{
    public float cdTimer = 3f;
    public bool isInCd = false;
    public override void Use()
    {
        if (!GameMaster.instance.playerController.enabled)
            return;
        if (amount <= 0)
        {
            RemoveFromInventory();
            return;
        }
        if (isInCd)
            return;
        InventoryManager.instance.StartCoroutine(CooldownSetter());
        GameMaster.instance.playerStats.currentHealth += health;
        GameMaster.instance.playerStats.currentMana += mana;
        GameMaster.instance.playerController.PlaySoundEffect(GameMaster.instance.playerController.potionDrinkingSound);
        if (GameMaster.instance.playerStats.currentHealth > GameMaster.instance.playerStats.maxHealth)
            GameMaster.instance.playerStats.currentHealth = GameMaster.instance.playerStats.maxHealth;
        if (GameMaster.instance.playerStats.currentMana > GameMaster.instance.playerStats.maxMana)
            GameMaster.instance.playerStats.currentMana = GameMaster.instance.playerStats.maxMana;
        amount--;
        if (amount <= 0)
            RemoveFromInventory();
        base.Use();
    }

    private IEnumerator CooldownSetter()
    {
        isInCd = true;
        yield return new WaitForSeconds(cdTimer);
        isInCd = false;
    }

    private void OnEnable()
    {
        isInCd = false;
        amount = 1;
    }
}
