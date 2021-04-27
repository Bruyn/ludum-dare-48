using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    
    [SerializeField] private List<AudioClip> musicClips = new List<AudioClip>();
    [SerializeField] private float musicVolume = .15f;

    private AudioSource audioSource;
    private int lastPlayedIndex = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("hyeta");
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        PlayMusic(0);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
            PlayMusic(lastPlayedIndex == 1 ? 2 : 1);
    }

    public void PlayMusic(int index)
    {
        lastPlayedIndex = index;
        StartCoroutine(FadeMusic(musicClips[index]));
    }

    private IEnumerator FadeMusic(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= Time.deltaTime;
                yield return null;
            }
            audioSource.Stop();
        }
        audioSource.clip = clip;
        audioSource.Play();
        while (audioSource.volume < musicVolume)
        {
            audioSource.volume += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
