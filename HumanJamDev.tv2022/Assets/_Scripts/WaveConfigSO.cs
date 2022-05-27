using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> humansPrefabs;

    [SerializeField] Transform levelPath;


    public int GetHumansCount()
    {
        return humansPrefabs.Count;
    }

    public GameObject GetRandomHumanPrefab()
    {
        int indexHuman = Random.Range(0, humansPrefabs.Count);
        return humansPrefabs[indexHuman];
    }

    public List<Transform> GetWayPoints()  //new list for  obstacles childs
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in levelPath)
        {
            waypoints.Add(child);
        }
        return waypoints;

    }
    public int GetWaypoinstCount()
    {
        return GetWayPoints().Count;

    }

    public Transform GetRandomWayPoint()
    {
        return levelPath.GetChild(Random.Range(0, GetWaypoinstCount()));
    }

}
