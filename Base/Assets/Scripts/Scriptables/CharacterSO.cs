using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSO : ScriptableObject
{
    [Header("Character Data")]
    [SerializeField] GameObject _prefab;
    [SerializeField] float _movementSpeed;

    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
