using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager Instance;

    private GameObject[] obstacles;
    private List<int> availableNumbers = new List<int>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //void Shuffle(List<int> list)
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        int randomIndex = Random.Range(i, list.Count);
    //        int temp = list[i];
    //        list[i] = list[randomIndex];
    //        list[randomIndex] = temp;
    //    }
    //}

    //public void Spawn(int quantity,int height)
    //{
    //    for (int i = 5; i < height-3; i++)
    //    {
    //        availableNumbers.Add(i);
    //    }
    //    Shuffle(availableNumbers);

    //    obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    //    foreach (GameObject obstacle in obstacles)
    //    {
    //        Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
    //        obstacleScript.ReturnToPool();
    //    }
    //    for (int i = 0; i < quantity; i++)
    //    {
    //        int value = availableNumbers[i];
    //        Vector3 spawnPos = new Vector3(2, value, 0.55f);
    //        GameObject obstacleObj = ObjectPooler.Instance.SpawnFromPool("Obstacle", spawnPos, Quaternion.identity);
    //        Obstacle obstacleScript = obstacleObj.GetComponent<Obstacle>();
    //        obstacleScript.Init(); 
    //    }

    //}

    public void Spawn(Vector3[] spawnPosition)
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
            obstacleScript.ReturnToPool();
        }
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            Vector3 spawnPos = spawnPosition[i];
            GameObject obstacleObj = ObjectPooler.Instance.SpawnFromPool("Obstacle", spawnPos, Quaternion.identity);
            Obstacle obstacleScript = obstacleObj.GetComponent<Obstacle>();
            obstacleScript.Init();
        }
    }
}
