using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Updates Data", menuName = "Item Updates/New Turret Updates Data")]
public class TurretUpdatesSO : UpdatesSO
{
    [SerializeField] List<TurretStats> _updatesList = new List<TurretStats>();
    public List<TurretStats> UpdatesList { get => _updatesList; set => _updatesList = value; }
}
