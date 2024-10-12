using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorUIHandler : MonoBehaviour
{
    [SerializeField] string nameSceneReturn;
    [SerializeField] Button returnButton;

    void Start()
    {
        returnButton.onClick.AddListener(() => ServiceLocator.GetService<ScenesManager>().ChangeScene(nameSceneReturn));
    }
}
