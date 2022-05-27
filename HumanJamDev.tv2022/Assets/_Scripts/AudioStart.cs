using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStart : MonoBehaviour
{
    [Header("StartGame Sounds")]
    [SerializeField] AudioClip clickSound;
    [SerializeField] AudioClip presSound;
    [SerializeField] AudioClip fastKeyPress;
    [SerializeField][Range(0f, 1f)] float startVolume;


    SoundTrackManager soundTrackManager;
    AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = FindObjectOfType<AudioSource>();
    }

    private void Start()
    {
        soundTrackManager = FindObjectOfType<SoundTrackManager>();
    }

    void Update()
    {
        transform.position = Camera.main.transform.position;
    }

    public void PlayClick()
    {
        myAudioSource.PlayOneShot(clickSound, startVolume);
    }

    public void PlayNormalKeySound()
    {
        myAudioSource.clip = presSound;
        myAudioSource.Play();
        myAudioSource.volume = startVolume;
    }
    public void PlayFastKeySound()
    {
        myAudioSource.clip = fastKeyPress;
        myAudioSource.Play();
        myAudioSource.volume = startVolume;
    }

    public void StopSound()
    {
        myAudioSource.Stop();

    }

    public void StartNoGameSound()
    {
        soundTrackManager.PlayNoGameSound();
    }

}
