using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] string nameSceneStart;
    [SerializeField] float timeTransition;

    [Space]
    [SerializeField] GameObject optionsMenuPanel;

    [Space]
    [SerializeField] Button startButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button resetButton;

    void Start()
    {
        ServiceLocator.GetService<AudioManager>().PlayMainMusic("Musica 1");
        startButton.onClick.AddListener(() => ServiceLocator.GetService<ScenesManager>().ChangeScene(nameSceneStart));
        optionsButton.onClick.AddListener(() => ChangePanel(optionsMenuPanel));
        exitButton.onClick.AddListener(() => Application.Quit());
        resetButton.onClick.AddListener(() => ResetGame());
    }

    public void ChangePanel(GameObject newPanel)
    {
        optionsMenuPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, timeTransition);
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, timeTransition).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void ResetGame()
    {
        ServiceLocator.GetService<DataManager>().DeleteAllData();
        ServiceLocator.GetService<ScenesManager>().ChangeScene(nameSceneStart);
    }
}