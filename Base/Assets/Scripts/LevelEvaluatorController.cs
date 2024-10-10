using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEvaluatorController : MonoBehaviour
{
    GameManager gameManager;
    DataManager data;

    private void Start()
    {
        gameManager = ServiceLocator.GetService<GameManager>();
        data = ServiceLocator.GetService<DataManager>();
    }
    public void Evaluate()
    {
        int stars = 0;
        int enemiesKilled = gameManager.Waves.GetEnemiesKilled();

        foreach (var item in gameManager.LevelData.StarsValues)
        {
            if (enemiesKilled >= item)
            {
                stars++;
            }
        }

        if (stars > 0)
        {
            LevelData levelData = data.Data.GetLevelDataByID(gameManager.LevelData.LevelID);
            levelData.CompleteLevel(stars);
            data.SaveAllData();
        };
    }
}
