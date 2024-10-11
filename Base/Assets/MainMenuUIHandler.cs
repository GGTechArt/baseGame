using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] Button exitButton;

    void Start()
    {
        exitButton.onClick.AddListener(() => Application.Quit());
    }

    public void Show()
    {

    }

    public void Hide()
    {

    }
}
