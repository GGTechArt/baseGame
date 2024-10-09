using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] int sceneID;
    [SerializeField] int currentScore;
    [SerializeField] bool isCompleted;

    public int SceneID { get => sceneID; set => sceneID = value; }
    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public bool IsCompleted { get => isCompleted; set => isCompleted = value; }
}