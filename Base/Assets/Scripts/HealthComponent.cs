using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthComponent : MonoBehaviour
{
    public delegate void HealthChangedDelegate(float currentHealth);
    public HealthChangedDelegate HealthChanged;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;


    public void Configure(float health)
    {
        _health = health;
        _maxHealth = health;
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void SetHealth(float newHealth)
    {
        _health = newHealth;
        HealthChanged?.Invoke(_health);
    }
}
