using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : ServiceInstallerBase<AudioManager>, IAudioManager
{
    [SerializeField] AudioMixer mixer;

    public void PlaySound()
    {
        Debug.Log("Reproduice sound");
    }

    protected override AudioManager CreateService()
    {
        ServiceLocator.RegisterService<IAudioManager>(this);
        return this;
    }
}
