using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> levelConfigs;
    [SerializeField] List<GameObject> humansSpawned;
    [SerializeField] float timeForNextHuman = 2f;
    [SerializeField] float totalHumanSpawned = 10f;


    WaveConfigSO currentLevel;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }




    public WaveConfigSO GetCurrentLevel()
    {
        return currentLevel;
    }

    public void SpawnHumans()
    {
        foreach (WaveConfigSO level in levelConfigs)
        {
            currentLevel = level;


            InvokeRepeating(nameof(StarterHumanSpawn), 1f, timeForNextHuman);
            FindObjectOfType<SoundTrackManager>().PlayInGameSound();
            FindObjectOfType<PlayerController>().ActivateControls(true);
            FindObjectOfType<TimerManager>().IsTimerActive(true);

        }
    }

    private void StarterHumanSpawn()
    {
        if (humansSpawned.Count > totalHumanSpawned) return;
        GameObject newHuman = Instantiate(currentLevel.GetRandomHumanPrefab(),
                                     currentLevel.GetRandomWayPoint().position,
                                     Quaternion.identity,
                                     transform);
        humansSpawned.Add(newHuman);
    }


    public void SpawnNewBorn()
    {
        GameObject newHuman = Instantiate(currentLevel.GetRandomHumanPrefab(),
                                     currentLevel.GetRandomWayPoint().position,
                                     Quaternion.identity,
                                     transform);

        humansSpawned.Add(newHuman);
    }


    float GetCurrentWaypoints()
    {
        int currentWaypoints = currentLevel.GetWaypoinstCount();
        return currentWaypoints;
    }



    public void SpawnSameHuman()
    {
        foreach (GameObject human in humansSpawned)
        {
            if (human.activeInHierarchy == false)
            {
                human.SetActive(true);

            }

        }
    }
    public float totalHumansCount()
    {
        if (humansSpawned.Count > gameManager.MaxHumans())
        {
            Debug.Log("You Win");
            FinishWithCount();
        }
        return humansSpawned.Count;


    }

    public void FinishWithCount()
    {
        StartCoroutine(gameManager.FinishingGame(humansSpawned.Count));
    }
}
