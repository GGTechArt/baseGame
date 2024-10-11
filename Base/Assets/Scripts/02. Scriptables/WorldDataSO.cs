using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "World Data", menuName = "New World Data")]
public class WorldDataSO : ScriptableObject
{
    [SerializeField] List<LevelDataSO> _levels;
    public List<LevelDataSO> Levels { set => _levels = value; get => _levels; }

    public LevelDataSO GetLevelDataByID(string id)
    {
        return _levels.Exists(x => x.LevelID == id) ? _levels.Find(x => x.LevelID == id) : null;
    }
}
