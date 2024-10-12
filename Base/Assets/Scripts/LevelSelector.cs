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
            ServiceLocator.GetService<DataManager>().SetLevel(levelData);
            sceneManager.ChangeScene(levelData.SceneName);
        });

        LoadLevelSelector();
    }
    public void LoadLevelSelector()
    {
        SetStars();

        if (previousLevelData == null)
        {
            blockedGO.SetActive(false);
            button.interactable = true;
        }

        else
        {
            if (dataManager.Data.GetLevelDataByID(previousLevelData.LevelID).IsCompleted)
            {
                blockedGO.SetActive(false);
                button.interactable = true;
            }

            else
            {
                blockedGO.SetActive(true);
                button.interactable = false;
            }
        }
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
