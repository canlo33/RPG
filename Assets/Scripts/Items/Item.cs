using UnityEngine;
[CreateAssetMenu(fileName ="New Item", menuName = "Items/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    public new string name;
    public string description = "Add Description";
    public Sprite icon;
    public bool stackable = false;
    public int amount = 1;
    public ItemType itemType;
    public Rarity itemRarity;
    public int sellPrice = 5;
    public int itemLevel = 1;
    public int attack;
    public int defense;
    public int health;
    public int mana;
    private PlayerStats playerStats;
    public virtual void Use()
    {
        InventoryManager.instance.onInventoryChangedCallBack.Invoke();
    }
    public void RemoveFromInventory()
    {
        InventoryManager.instance.RemoveItem(this);
    }
    public void AddItemStats()
    {
        if(playerStats == null)
            playerStats = GameMaster.instance.playerStats;
        playerStats.maxHealth += health;
        playerStats.maxMana += mana;
        playerStats.currentHealth += health;
        playerStats.currentMana += mana;
        playerStats.currentDamage += attack;
        playerStats.currentDefense += defense;
        playerStats.itemDamage += attack;
        playerStats.itemDefense += defense;
        playerStats.itemHealth += health;
        playerStats.itemMana += mana;

    }
    public void RemoveItemStats()
    {
        if (playerStats == null)
            playerStats = GameMaster.instance.playerStats;
        playerStats.maxHealth -= health;
        if (playerStats.currentHealth > playerStats.maxHealth)
            playerStats.currentHealth = playerStats.maxHealth;
        playerStats.maxMana -= mana;
        if (playerStats.currentMana > playerStats.maxMana)
            playerStats.currentMana = playerStats.maxMana;
        playerStats.currentMana -= mana;
        playerStats.currentDamage -= attack;
        playerStats.currentDefense -= defense;
        playerStats.itemDamage -= attack;
        playerStats.itemDefense -= defense;
        playerStats.itemHealth -= health;
        playerStats.itemMana -= mana;
    }
}
public enum ItemType
{
    Earring,
    Helmet,
    Weapon,
    Pauldron,
    Ring,
    Pant,
    Gauntlet,
    Boot,
    Potion,
    Quest,
    Default
}
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic
}
