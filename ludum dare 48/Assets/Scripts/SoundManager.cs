using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public CharAnimEventReceiver AnimEventReceiver;
    
    public AudioClip shotSound;
    public AudioClip footStep;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        AnimEventReceiver.OnStep.AddListener(StepHappend);
    }

    void StepHappend(bool _)
    {
        _audioSource.PlayOneShot(footStep);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
