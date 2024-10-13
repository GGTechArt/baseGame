using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterConfig : MonoBehaviour
{
    public delegate void CharacterDestroyedHandler(CharacterConfig character);
    public static CharacterDestroyedHandler OnCharacterDestroyed;

    [SerializeField] EnemyMovement movement;
    [SerializeField] HealthComponent health;
    BaseDamageable _damageable;
    BaseCurable _curable;

    CharacterSO _data;

    public BaseDamageable Damageable { get => _damageable; set => _damageable = value; }
    public CharacterSO Data { get => _data; set => _data = value; }

    private void Awake()
    {

    }

    public void ConfigureCharacter(CharacterSO data)
    {
        Data = data;
        health.Configure(Data.Health);
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
