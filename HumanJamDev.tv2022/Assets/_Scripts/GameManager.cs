using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CinematicBehaviour startPlayer = null;
    [SerializeField] float waitTimer = 5f;
    [SerializeField] float waitSecondTimer = 5f;

    [Header("GameOver")]
    [SerializeField] float maxElderlyPopulation = 10f;
    [SerializeField] float gameOverTime = 50f;
    [SerializeField] float maxHumans = 300;
    float maxScore;

    private void Start()
    {
        startPlayer = FindObjectOfType<CinematicBehaviour>();
    }
    public void LoadGame()
    {
        startPlayer = FindObjectOfType<CinematicBehaviour>();
        StartCoroutine(StartingGame());
    }

    IEnumerator StartingGame()
    {

        Debug.Log("Cargando1");
        startPlayer.FocusGreenScreen();
        yield return new WaitForSecondsRealtime(waitTimer);
        Debug.Log("Cargando2");
        startPlayer.FocusBlueScreen();
        yield return new WaitForSecondsRealtime(waitSecondTimer);
        startPlayer.FocusStartScreen();
        yield return new WaitForSecondsRealtime(waitSecondTimer);
        yield return SceneManager.LoadSceneAsync(1);
        maxScore = 0;

    }


    public IEnumerator FinishingGame(float value)
    {
        maxScore = value;
        yield return new WaitForSecondsRealtime(1f);
        FindObjectOfType<SoundTrackManager>().StopAllMusic();
        FindObjectOfType<PlayerController>().FinishingGamePlayer();
        FindObjectOfType<SoundTrackManager>().FinalSong();
        yield return new WaitForSecondsRealtime(2f);
        yield return SceneManager.LoadSceneAsync(2);




    }



    public void StartToMainMenu()
    {
        FindObjectOfType<SoundTrackManager>().StopAllMusic();
        SceneManager.LoadSceneAsync(0);
        StopAllCoroutines();
    }









    public float MaXElderly()
    {
        return maxElderlyPopulation;
    }
    public float TimeForGameOver()
    {
        return gameOverTime;
    }

    public float MaxHumans()
    {
        return maxHumans;
    }
    public float GetMaxScore()
    {
        return maxScore;
    }

}
