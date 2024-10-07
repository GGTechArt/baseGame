using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveData
{
    [SerializeField] List<EnemyDataSO> _enemies;
    [SerializeField] int _enemiesAmmount;
    [SerializeField] float _minRateTime, _maxRateTime;
    [SerializeField] float _nextWaveTime;

    public List<EnemyDataSO> Enemies { get => _enemies; set => _enemies = value; }
    public int EnemiesAmmount { get => _enemiesAmmount; set => _enemiesAmmount = value; }
    public float MinRateTime { get => _minRateTime; set => _minRateTime = value; }
    public float MaxRateTime { get => _maxRateTime; set => _maxRateTime = value; }
    public float NextWaveTime { get => _nextWaveTime; set => _nextWaveTime = value; }
}
