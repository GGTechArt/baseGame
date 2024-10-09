using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealthComponent
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;

    public HealthComponent(float health)
    {
        _health = health;
    }

    private void Start()
    {
        _maxHealth = _health;
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
    }
}
