using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClips Data", menuName = "New AudioClips Data")]
public class AudioClipsSO : ScriptableObject
{
    [SerializeField] List<AudioClipBase> _clips = new List<AudioClipBase>();

    public List<AudioClipBase> Clips { get => _clips; set => _clips = value; }

    public AudioClip GetAudioClipByID(string id)
    {
        if (_clips.Exists(x => x.Id == id))
        {
            return _clips.Find(x => x.Id == id).Clip;
        }

        else
        {
            Debug.LogError("Clip not found");
            return null;
        }
    }
}

[Serializable]
public class AudioClipBase
{
    [SerializeField] string _id;
    [SerializeField] AudioClip _clip;

    public string Id { get => _id; set => _id = value; }
    public AudioClip Clip { get => _clip; set => _clip = value; }
}
