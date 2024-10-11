using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSO : ScriptableObject
{
    [Header("Character Data")]
    [SerializeField] GameObject _prefab;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _health;

    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public float Health { get => _health; set => _health = value; }
}
