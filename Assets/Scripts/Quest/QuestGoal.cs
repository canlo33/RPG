using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoal
{
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }
    public Quest Quest { get; set; }

    public virtual void Init()
    {
    
    }
    public virtual void UnInit()
    {
        CurrentAmount = 0;
        Completed = false;
    }
    public virtual void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
            Complete();
    }
    public void Complete()
    {
        Completed = true;
        Quest.CheckGoals();
    }
}
