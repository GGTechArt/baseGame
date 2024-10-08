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
    [SerializeField] BaseCurable _curable;

    private void Awake()
    {

    }

    public void ConfigureCharacter()
    {
        health = new HealthComponent();
        _damageable = new BaseDamageable(health);
        _curable = new BaseCurable(health);

        _damageable.OnDeath += CharacterDestroyed;
    }

    public void CharacterDestroyed()
    {
        _damageable.OnDeath -= CharacterDestroyed;
        OnCharacterDestroyed.Invoke(this);
    }

    private void OnDestroy()
    {
        CharacterDestroyed();
    }
}
