using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyMovement movement;
    [SerializeField] HealthComponent health;
    [SerializeField] BaseDamageable damageable;
    [SerializeField] BaseCurable curable;

    private void Start()
    {

    }

    public void ConfigureCharacter()
    {
        health = new HealthComponent();
        damageable = new BaseDamageable(health);
        curable = new BaseCurable(health);
    }
}
