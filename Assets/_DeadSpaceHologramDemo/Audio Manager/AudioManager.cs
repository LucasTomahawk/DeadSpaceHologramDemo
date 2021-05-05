using UnityEngine.Audio;
using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance = null;
    AudioSource _audioSource;

    private void Awake()
    {
        #region Singleton Pattern (Simple
        if (Instance == null)
        {
            // Doesnt Exist yet...
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Fill references
            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.SpatialBlend;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not Found!");
            return;
        }
        s.source.Play();
    }

    public void PlaySong(AudioClip Clip)
    {
        _audioSource.clip = Clip;
        _audioSource.Play();
    }
}