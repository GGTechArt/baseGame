using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypeEnum
{
    Goblin
}

[CreateAssetMenu(fileName = "Enemy Data", menuName = "New Enemy Data")]
public class EnemyDataSO : CharacterSO
{
    [Header("Enemy Data")]
    [SerializeField] EnemyTypeEnum _type;
    [SerializeField] int _killedScore;

    public int KilledScore { get => _killedScore; set => _killedScore = value; }
}
