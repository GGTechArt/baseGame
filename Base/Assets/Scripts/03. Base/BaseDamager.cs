using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamager : MonoBehaviour, IDamager
{

    [SerializeField] protected float damageValue;
    public float damage { get => damageValue; set => damageValue = value; }

    public void SetDamage(IDamageable damageable)
    {
        if (damageable != null)
            damageable.ReceiveDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterConfig character;
        if (other.TryGetComponent(out character))
            SetDamage(character.Damageable);
    }
}
