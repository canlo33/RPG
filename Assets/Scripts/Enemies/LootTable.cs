using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public List<Loot> Loot = new List<Loot>();
    public int minGold;
    public int maxGold;
    public void RollLoot()
    {
        int goldAmount = Random.Range(minGold, maxGold + 1);
        InventoryManager.instance.AddGold(goldAmount);
        foreach (var loot in Loot)
        {
            int roll = Random.Range(0, 101);
            if (roll <= loot.dropChance)
            {
                InventoryManager.instance.AddItem(loot.item, loot.amount);
            }
        }
    }
    public void AddToLoot(Loot loot)
    {
        Loot.Add(loot);
    }
    public void RemoveFromLoot(Loot loot)
    {
        Loot.Remove(loot);
    }
}

[System.Serializable]
public class Loot
{
    public Item item;
    public int dropChance;
    public int amount;

}
