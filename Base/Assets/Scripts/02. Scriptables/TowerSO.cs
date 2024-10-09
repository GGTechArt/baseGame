using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Data", menuName = "New Tower Data")]
public class TowerSO : ItemSO
{
    [Header("Tower Data")]
    [SerializeField] float _fireRate;
    [SerializeField] float _speed;

    [SerializeField] List<AlliedUnitSO> _allies;
}
