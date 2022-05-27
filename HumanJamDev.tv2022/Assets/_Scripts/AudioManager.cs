using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Reproduction Sounds")]
    [SerializeField] AudioClip newBornSound;
    [SerializeField] AudioClip makingNewBornSound;
    [SerializeField] AudioClip killSound;
    [SerializeField][Range(0f, 1f)] float reproductionVolume;



    AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
  
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }

    public void PlayNewBornSound()
    {   
        myAudioSource.PlayOneShot(newBornSound, reproductionVolume);
    }

       public void PlayReproductionSound()
    {   
        myAudioSource.PlayOneShot(makingNewBornSound, reproductionVolume);
    }

    public void PlayKillSound()
    {
        myAudioSource.PlayOneShot(killSound, reproductionVolume);
        print("playing kill sound");
    }
}
