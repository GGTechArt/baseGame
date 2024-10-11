using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIHandler : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Toggle fullscreenToggle;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = ServiceLocator.GetService<AudioManager>();

        musicSlider.onValueChanged.AddListener(delegate
        {
            audioManager.ChangeMusicVolume(musicSlider.value);
        });

        sfxSlider.onValueChanged.AddListener(delegate
        {
            audioManager.ChangeSfxVolume(sfxSlider.value);
        });

        fullscreenToggle.onValueChanged.AddListener(delegate
        {
            Screen.fullScreen = fullscreenToggle.isOn;
        });

        UpdateSfxSlider(audioManager.GetSfxVolume());
        UpdateMusicSlider(audioManager.GetMusicVolume());
    }

    public void UpdateSfxSlider(float volume)
    {
        Debug.Log(volume);
        sfxSlider.value = volume;
    }

    public void UpdateMusicSlider(float volume)
    {
        Debug.Log("Music "+volume);
        musicSlider.value = volume;
    }
}
