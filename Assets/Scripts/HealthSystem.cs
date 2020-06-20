using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem 
{
    public int maxHealth;
    public int currentHealth;
    public bool isInvulrable = false;

    
    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
    public int GetHealth()
    {
        return currentHealth;
    }
    public void Damage(int amount)
    {
        if (isInvulrable) return;

        currentHealth -= amount;
        if(currentHealth < 0)  currentHealth = 0;
        
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if(currentHealth > maxHealth) currentHealth = maxHealth;
    }


}
