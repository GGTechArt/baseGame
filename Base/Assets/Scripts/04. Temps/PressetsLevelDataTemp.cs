using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PressetsLevelData : MonoBehaviour
{
    public LevelData levelData;
    public DataManager dataManager;


    [ContextMenu("PressetsLevelsData")]
    public void PressetLevelsData()
    {
        levelData.CurrentScore = Random.Range(0, 5);
        levelData.SceneID = 1;
        levelData.IsCompleted = true;
        Debug.Log("datos del nivel se han actualizado");
    }

    [ContextMenu("SaveCurrentData")]
    public void SaveCurretData()
    {
        dataManager.SaveData<LevelData>(levelData);
    }
}
