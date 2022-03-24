﻿using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;

    // Audio files
    public AudioClip jump;
    public AudioClip collect;
    public AudioClip click;
    public AudioClip cantBuy;
    public AudioClip buy;

    private AudioSource soundEffectAudio;

    private void Awake () {

        Instance = this;

    }

    private void Start () {

        AudioSource theSource = GetComponent<AudioSource> ();
        soundEffectAudio = theSource;

    }

    /// <summary>
    /// Plays the audio
    /// </summary>
    /// <param name="clip">Audio to be played</param>
    public void PlayOneShot (AudioClip clip) {

        soundEffectAudio.PlayOneShot (clip);

    }

}
