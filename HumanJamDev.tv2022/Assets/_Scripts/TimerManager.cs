using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerDisplay = null;
    [SerializeField] float minTiredTime = 5f; // para probar
    [SerializeField] float tiredRange = 2;


    public float timer;
    bool isTimerActive = false;
    float maxTime;

    PlayerController player;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        maxTime = FindObjectOfType<GameManager>().TimeForGameOver();
    }

    void Update()
    {
        if (!isTimerActive) return;
        timer += Time.deltaTime;
        if (timerDisplay == null) return;
        timerDisplay.text = "Year " + Mathf.RoundToInt(2022 + timer);
        if(timer > maxTime)
        {
            Debug.Log("Time Finished");
            StartCoroutine(FindObjectOfType<GameManager>().FinishingGame(FindObjectOfType<Spawner>().totalHumansCount()));
        }
        if (timer >= minTiredTime)
        {
            StartCoroutine(player.SleepActions());
            minTiredTime = timer + Random.Range(minTiredTime, minTiredTime * tiredRange);
        }

    }
    public void IsTimerActive(bool value)
    {
        isTimerActive = value;
    }





}
