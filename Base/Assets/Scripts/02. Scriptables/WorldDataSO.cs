using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "World Data", menuName = "New World Data")]
public class WorldDataSO : ScriptableObject
{
    [SerializeField] List<LevelDataSO> _levels;
    public List<LevelDataSO> Levels { set => _levels = value; get => _levels;}    
}
