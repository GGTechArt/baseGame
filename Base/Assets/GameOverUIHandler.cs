using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIHandler : MonoBehaviour
{
    GameManager manager;
    CanvasGroup group;

    [SerializeField] List<GameObject> starsGO;

    private void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();
        group = GetComponent<CanvasGroup>();

        manager.GameStateChanged += GameStateChanged;
    }

    public void GameStateChanged(GameState currentState)
    {
        if (currentState == GameState.FINISHED)
        {
            Show();
        }
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
