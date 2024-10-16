using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterConfig : MonoBehaviour
{
    public delegate void CharacterDestroyedHandler(CharacterConfig character);
    public static CharacterDestroyedHandler OnCharacterDestroyed;

    [SerializeField] EnemyMovement movement;
    [SerializeField] HealthComponent health;
    [SerializeField] BaseDamageable _damageable;
    BaseCurable _curable;

    public BaseDamageable Damageable { get => _damageable; set => _damageable = value; }

    private void Awake()
    {

    }

    public void ConfigureCharacter(CharacterSO data)
    {
        health = new HealthComponent(data.Health);
        _damageable = new BaseDamageable(health);
        _curable = new BaseCurable(health);

        _damageable.OnDeath += CharacterKilled;
    }

    public void CharacterKilled()
    {
        CharacterDestroyed(true);
    }

    public void CharacterDestroyed(bool destroy)
    {
        _damageable.OnDeath -= CharacterKilled;
        OnCharacterDestroyed?.Invoke(this);

        if (destroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        CharacterDestroyed(false);
    }
}
