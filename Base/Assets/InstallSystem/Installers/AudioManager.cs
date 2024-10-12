using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Rendering;
using NUnit.Framework;

public class AudioManager : ServiceInstallerBase<AudioManager>
{
    public delegate void ChangeSoundVolume(float value);
    public event ChangeSoundVolume musicVolumeIsChanged;
    public event ChangeSoundVolume sfxVolumeIsChanged;

    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioClipsSO clipsData;
    AudioMixerGroup musicMixerGroup, sfxMixerGroup;

    float musicVolume;
    float sfxVolume;

    bool musicCrossfade = false;
    float musicCrossfadeTime = 1;
    float musicTargetVolume = 1;
    int musicSourceIndex = 0;
    int sfxSourceIndex = 0;

    AudioSource mainMusicSource, secondMusicSource;
    List<AudioSource> musicSources = new List<AudioSource>();
    List<AudioSource> sfxSources = new List<AudioSource>();

    private void Awake()
    {
        musicMixerGroup = mixer.FindMatchingGroups("Music")[0];
        sfxMixerGroup = mixer.FindMatchingGroups("Sfx")[0];

        for (int i = 0; i < 2; i++)
        {
            AudioSource musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.outputAudioMixerGroup = musicMixerGroup;
            musicSource.loop = true;
            musicSources.Add(musicSource);

            if (i == 1)
            {
                musicSource.volume = 0;
            }
        }

        mainMusicSource = musicSources[0];

        for (int i = 0; i < 5; i++)
        {
            AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = sfxMixerGroup;
            sfxSources.Add(sfxSource);
        }


        ChangeMusicVolume(GetMusicVolume());
        ChangeSfxVolume(GetSfxVolume());

        //PlayMainMusic("Musica 1");
    }
    private void Update()
    {
        if (musicCrossfade)
        {
            if (mainMusicSource.volume != musicTargetVolume || secondMusicSource.volume != 0)
            {
                mainMusicSource.volume = Mathf.MoveTowards(mainMusicSource.volume, musicTargetVolume, musicCrossfadeTime * Time.deltaTime);
                secondMusicSource.volume = Mathf.MoveTowards(secondMusicSource.volume, 0, musicCrossfadeTime * Time.deltaTime);
            }

            else
            {
                if (musicTargetVolume == 0)
                {
                    mainMusicSource.Stop();
                    secondMusicSource.Stop();
                }

                musicCrossfade = false;
            }
        }
    }

    public void StopMainMusic()
    {
        musicTargetVolume = 0;
        musicCrossfade = true;
    }

    public void PlayMainMusic(string _clipName)
    {
        if (mainMusicSource.clip != clipsData.GetAudioClipByID(_clipName) || musicTargetVolume == 0)
        {
            MusicCrossfade();
            musicTargetVolume = 1;
            mainMusicSource.loop = true;
            mainMusicSource.clip = clipsData.GetAudioClipByID(_clipName);
            mainMusicSource.Play();
        }
    }

    public void PlayMainSfx(string _clipName)
    {
        sfxSourceIndex++;

        if (sfxSourceIndex > sfxSources.Count - 1)
            sfxSourceIndex = 0;

        sfxSources[sfxSourceIndex].PlayOneShot(clipsData.GetAudioClipByID(_clipName));
    }

    public void MusicCrossfade()
    {
        secondMusicSource = musicSources[musicSourceIndex];

        if (secondMusicSource.isPlaying || musicTargetVolume == 0)
        {
            musicSourceIndex++;

            if (musicSourceIndex > musicSources.Count - 1)
                musicSourceIndex = 0;

            musicCrossfade = true;
        }

        mainMusicSource = musicSources[musicSourceIndex];
    }

    public void ChangeSfxVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("SfxVolume", newVolume); // Guarda el valor lineal
        sfxVolume = Mathf.Log10(Mathf.Clamp(newVolume, 0.01f, 1f)) * 20; // Aplica la conversión logarítmica para el AudioMixer
        mixer.SetFloat("SfxVolume", sfxVolume); // Ajusta el mezclador
        sfxVolumeIsChanged?.Invoke(newVolume); // Llama al evento con el valor lineal
    }

    public void ChangeMusicVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("MusicVolume", newVolume); // Guarda el valor lineal
        musicVolume = Mathf.Log10(Mathf.Clamp(newVolume, 0.01f, 1f)) * 20; // Aplica la conversión logarítmica para el AudioMixer
        mixer.SetFloat("MusicVolume", musicVolume); // Ajusta el mezclador
        musicVolumeIsChanged?.Invoke(newVolume); // Llama al evento con el valor lineal
    }

    public float GetSfxVolume()
    {
        if (PlayerPrefs.HasKey("SfxVolume"))
            return PlayerPrefs.GetFloat("SfxVolume");
        return 1; // Valor por defecto
    }

    public float GetMusicVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
            return PlayerPrefs.GetFloat("MusicVolume");
        return 1;
    }

    protected override AudioManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }
}
