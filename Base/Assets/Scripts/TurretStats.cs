using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurretStats
{
    [Header("Tower Data")]
    [SerializeField] float _fireRate;
    [SerializeField] float _speed;
    [SerializeField] GameObject _prefab;

    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
}
