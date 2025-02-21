using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUIHandler : MonoBehaviour
{
    GameManager manager;
    ScenesManager scenes;
    CanvasGroup group;
    [SerializeField] string nextScene;
    [SerializeField] List<GameObject> starsGO;
    [SerializeField] Button retryButton;

    void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();
        scenes = ServiceLocator.GetService<ScenesManager>();

        group = GetComponent<CanvasGroup>();

        retryButton.onClick.AddListener(() => scenes.ChangeScene(nextScene));
    }

    public void Show()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;

        ShowStars();
    }

    public void Hide()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
    }

    public void ShowStars()
    {
        int stars = manager.Evaluator.GetStars();

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
