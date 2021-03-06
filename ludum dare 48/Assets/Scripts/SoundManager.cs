using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private List<AudioClip> shotClips = new List<AudioClip>();
    [SerializeField] private float shotVolume = .15f;
    
    [SerializeField] private List<AudioClip> karateClips = new List<AudioClip>();
    [SerializeField] private float karateVolume = .15f;
    
    [SerializeField] private List<AudioClip> hitClips = new List<AudioClip>();
    [SerializeField] private float hitVolume = .15f;
    
    [SerializeField] private List<AudioClip> wooshClips = new List<AudioClip>();
    [SerializeField] private float wooshVolume = .15f;

    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("SOMETHING IS BAAAD!!!!");
            return;
        }
        
        Instance = this;
    }

    public void PlayShotSound()
    {
        int index = Random.Range(0, shotClips.Count);
        PlaySound(shotClips[index], shotVolume);
    }
    
    public void PlayHitSound()
    {
        int index = Random.Range(0, hitClips.Count);
        PlaySound(hitClips[index], hitVolume);
    }
    
    public void PlayKarateSound()
    {
        int index = Random.Range(0, karateClips.Count);
        PlaySound(karateClips[index], karateVolume);
    }
    
    public void PlayWooshSound()
    {
        int index = Random.Range(0, wooshClips.Count);
        PlaySound(wooshClips[index], wooshVolume);
    }
    
    public void PlaySound(AudioClip clipToPlay, float volume)
    {
        AudioSource audioSource = GetAudioSource();
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.clip = clipToPlay;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private AudioSource GetAudioSource()
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
                return audioSource;
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        audioSources.Add(newSource);
        return newSource;
    }
}
