using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIInstallerTest : MonoBehaviour
{
    [SerializeField]Button testButton;

    private void Start()
    {
        testButton.onClick.AddListener(ReproduceSound);
    }
    public void ReproduceSound()
    {
        ServiceLocator.GetService<IAudioManager>().PlaySound();
    }
}
