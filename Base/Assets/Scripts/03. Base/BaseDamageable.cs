using System;
using UnityEngine;

[Serializable]
public class BaseDamageable : IDamageable
{
    public delegate void DamageReceivedHandler();
    public DamageReceivedHandler OnDamageReceived;

    public delegate void DeathHandler();
    public DeathHandler OnDeath;

    [SerializeField] HealthComponent healthValue;

    public BaseDamageable(HealthComponent healthValue)
    {
        this.healthValue = healthValue;
    }

    public virtual void ReceiveDamage(float damage)
    {
        float health = healthValue.GetHealth() - damage;

        if (health <= 0)
        {
            healthValue.SetHealth(0);
            OnDeath?.Invoke();
        }

        else
        {
            healthValue.SetHealth(health);
            OnDamageReceived?.Invoke();
        }
    }
}
