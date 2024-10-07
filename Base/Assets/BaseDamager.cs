using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamager : MonoBehaviour, IDamager
{

    [SerializeField] protected float damageValue;
    public float damage { get => damageValue; set => damageValue = value; }

    public void SetDamage(IDamageable damageable)
    {
        Debug.Log("Hace daño");
        damageable.ReceiveDamage(damage);
    }
}
