using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundTrackManager : MonoBehaviour
{
    [SerializeField] AudioClip trackNoGame;
    [SerializeField] AudioClip trackInGame;
    [SerializeField] AudioClip finalTrack;

    [SerializeField][Range(0, 1)] float maxvolumeValue;
    [SerializeField][Range(0, 1)] float inGameMaxvolumeValue;
    [SerializeField] float volumeRange;

    AudioSource trackManager;

    private void Start()
    {
        trackManager = GetComponent<AudioSource>();
    }

    public void PlayNoGameSound()
    {
        trackManager.clip = trackNoGame;
        trackManager.Play();
        trackManager.volume = Mathf.Lerp(0, maxvolumeValue, volumeRange);
    }

    public void PlayInGameSound()
    {
        trackManager.clip = trackInGame;
        trackManager.Play();
        trackManager.volume = inGameMaxvolumeValue;
    }

    public void finishingSound()
    {
        if (trackManager.volume > 0)
        {
            InvokeRepeating("BajaVolumen", 0.5f, 0.5f);
        }
           

    }

    private void BajaVolumen()
    {
        trackManager.volume = Mathf.Lerp(inGameMaxvolumeValue, 0, volumeRange);
    }

    public void FinalSong()
    {
        trackManager.clip = finalTrack;
        trackManager.Play();
        trackManager.volume = maxvolumeValue;
    }

    public void StopAllMusic()
    {
        trackManager.Stop();
    }

}
