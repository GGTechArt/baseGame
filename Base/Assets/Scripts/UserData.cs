using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public float Score { get => _score; set => _score = value; }
    [SerializeField] float _score;

    public List<LevelData> Levels { get => _levels; set => _levels = value; }
    [SerializeField] List<LevelData> _levels = new List<LevelData>();

    public UserData(float score, WorldDataSO world)
    {
        _score = score;

        for (int i = 0; i < world.Levels.Count; i++)
        {
            _levels.Add(new LevelData(world.Levels[i].LevelID, false));
        }
    }
}