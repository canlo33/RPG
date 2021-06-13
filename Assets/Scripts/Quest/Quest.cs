using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SocialPlatforms;
using System;

public class Quest : MonoBehaviour
{
    public List<QuestGoal> Goals { get; set; } = new List<QuestGoal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExpReward { get; set; }
    public int GoldReward { get; set; }
    public Item ItemReward { get; set; }
    public int ItemAmount { get; set; }
    public bool Completed { get; set; }
    public bool GoalsCompleted { get; set; }
    public int RequiredLevel { get; set; }

    private PlayerStats PlayerStats { get; set; }

    private void Awake()
    {
        PlayerStats = GameMaster.instance.playerStats;
    }

    public void CheckGoals()
    {
        GoalsCompleted = Goals.All(goal => goal.Completed);
    }
    public void GiveReward()
    {
        // Give EXP Reward, Coin Reward, Item Reward or whatever reward you wanna give.
        Completed = true;
        PlayerStats.currentExp += ExpReward;
        InventoryManager.instance.playerGold += GoldReward;
        if (ItemReward != null)
            InventoryManager.instance.AddItem(ItemReward, ItemAmount);
        PlayerStats.LevelUp();
        
    }
    public virtual void Init()
    {
        Goals.ForEach(g => g.Init());
    }
    public virtual void UnInit()
    {
        Goals.ForEach(g => g.UnInit());
    }
}
