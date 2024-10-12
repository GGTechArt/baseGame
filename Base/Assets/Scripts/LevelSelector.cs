using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] LevelDataSO previousLevelData;
    [SerializeField] LevelDataSO levelData;

    [SerializeField] GameObject blockedGO, unlockedGO;
    [SerializeField] Image point;
    [SerializeField] Sprite available, notAvailable;
    [SerializeField] Button button;

    [SerializeField] List<GameObject> starsGO;

    DataManager dataManager;
    ScenesManager sceneManager;

    private void Start()
    {
        dataManager = ServiceLocator.GetService<DataManager>();
        sceneManager = ServiceLocator.GetService<ScenesManager>();

        button.onClick.AddListener(delegate
        {
            dataManager.SetLevel(levelData);
            sceneManager.ChangeScene(levelData.SceneName);
        });

        LoadLevelSelector();
    }
    public void LoadLevelSelector()
    {
        bool completed = dataManager.Data.GetLevelDataByID(levelData.LevelID).IsCompleted;

        blockedGO.SetActive(completed ? false : true);
        unlockedGO.SetActive(completed ? true : false);

        if (previousLevelData == null)
        {
            button.interactable = true;
        }

        else
        {
            bool previousCompleted = dataManager.Data.GetLevelDataByID(previousLevelData.LevelID).IsCompleted;
            button.interactable = previousCompleted ? true : false;
        }

        point.sprite = button.interactable ? available : notAvailable;

        SetStars();
    }
    public void SetStars()
    {
        LevelData data = dataManager.Data.GetLevelDataByID(levelData.LevelID);
        int stars = data.CurrentScore;

        for (int i = 0; i < starsGO.Count; i++)
        {
            if (i <= stars - 1)
            {
                starsGO[i].SetActive(true);
            }
            else
            {
                starsGO[i].SetActive(false);
            }
        }
    }
}
