using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIHandler : MonoBehaviour
{
    ScenesManager scenes;
    CanvasGroup group;

    [SerializeField] Button retryButton;

    private void Start()
    {
        scenes = ServiceLocator.GetService<ScenesManager>();

        group = GetComponent<CanvasGroup>();

        retryButton.onClick.AddListener(() => scenes.ReloadScene());
    }

    public void Show()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
    }

    public void Hide()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
    }
}
