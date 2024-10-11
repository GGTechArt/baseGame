using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower Data", menuName = "New Tower Data")]
public class TowerSO : BuildableItemSO
{
    [SerializeField] List<AlliedUnitSO> _allies;
}
