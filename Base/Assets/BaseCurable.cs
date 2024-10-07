using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCurable : ICurable
{
    public delegate void HealReceivedHandler();
    public HealReceivedHandler OnHealReceived;

    HealthComponent healthValue;
    public BaseCurable(HealthComponent healthValue)
    {
        this.healthValue = healthValue;
    }
    public void ReceiveHealing(float healingValue)
    {
        if (healthValue.GetHealth() + healingValue > healthValue.GetMaxHealth())
        {
            healthValue.SetHealth(healthValue.GetMaxHealth());
        }
        else
        {
            healthValue.SetHealth(healthValue.GetHealth() + healingValue);
        }
    }
}
