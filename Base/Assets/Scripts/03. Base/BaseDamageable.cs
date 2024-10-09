using System;
using UnityEngine;

public class BaseDamageable : IDamageable
{
    public delegate void DamageReceivedHandler();
    public DamageReceivedHandler OnDamageReceived;

    public delegate void DeathHandler();
    public DeathHandler OnDeath;

    HealthComponent healthValue;

    public BaseDamageable(HealthComponent healthValue)
    {
        this.healthValue = healthValue;
    }

    public virtual void ReceiveDamage(float damage)
    {
        float health = healthValue.GetHealth();

        if (health - damage <= 0)
        {
            healthValue.SetHealth(0);
            OnDeath?.Invoke();
        }

        else
        {
            healthValue.SetHealth(health - damage);
            OnDamageReceived?.Invoke();
        }
    }
}
