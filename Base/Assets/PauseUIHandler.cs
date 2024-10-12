using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIHandler : MonoBehaviour
{

    GameManager manager;
    ScenesManager scenes;
    CanvasGroup group;

    [SerializeField] Button exitButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button retryButton;

    private void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();
        scenes = ServiceLocator.GetService<ScenesManager>();

        group = GetComponent<CanvasGroup>();

        resumeButton.onClick.AddListener(() => manager.ResumeGame());
        exitButton.onClick.AddListener(() => Application.Quit());
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
