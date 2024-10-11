using System;
using UnityEngine;

[Serializable]
public class LevelData
{
    [SerializeField] string _levelID;
    [SerializeField] int _currentScore;
    [SerializeField] bool _isCompleted = false;

    public LevelData(string levelID, bool isCompleted)
    {
        _levelID = levelID;
        _isCompleted = isCompleted;
        _currentScore = 0;
    }

    public string LevelID { get => _levelID; set => _levelID = value; }
    public int CurrentScore { get => _currentScore; set => _currentScore = value; }
    public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }

    public void CompleteLevel(int score)
    {
        _isCompleted = true;
        _currentScore = score;
    }

}