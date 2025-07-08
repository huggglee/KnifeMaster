using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Scripts")]
    [SerializeField] Tower towerScript;
    [SerializeField] Finish finishScript;
    [SerializeField] BallController ballScript;
    [SerializeField] Target targetScript;
    public UnityAction DataLoaded;
    public Dictionary<int, LevelData> levelDatas = new Dictionary<int, LevelData>();

    private int currentLevel;
    private string levelPath = "Levels";
    private GameObject[] knifes;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        DataLoaded += LoadCurrentLevel;
        currentLevel = Data.Instance.GetCurrentLevel();
        LoadData();
        //LoadCurrentLevel();
    }

    public void LoadData()
    {
        ScreenManager.Instance.LoadData();
        List<LevelData> leveldatas = Resources.LoadAll<LevelData>(levelPath).ToList();
        levelDatas = leveldatas.ToDictionary(i => i.level);
        Debug.Log("loaddata");
        DataLoaded?.Invoke();
    }

    public void LoadLevel(int level)
    {
        GameManager.Instance.SetState(GameManager.gameState.onLoad);
        KnifeThrower.Instance.ResetState();
        KnifeThrower.Instance._currentKnife = null;
        KnifeThrower.Instance.ResetCurrentHeight();
        if (!levelDatas.ContainsKey(level))
        {
            Debug.LogError($"Level {level} not found in levelDatas dictionary.");
            return;
        }
        knifes = GameObject.FindGameObjectsWithTag("Knife");
        foreach (GameObject knife in knifes)
        {
            Knife knifeScript = knife.GetComponent<Knife>();
            knifeScript.ReturnPool();
        }
        //GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.onLoad, 0.2f));
        towerScript.Spawn(levelDatas[level].towerHeight);
        finishScript.Spawn(levelDatas[level].towerHeight);
        targetScript.Spawn(levelDatas[level].towerHeight + 0.5f);
        //int count = levelDatas[level].obstacle;
        //for (int i = 0; i < count; i++)
        //{
        //    int value = Random.Range(5, levelDatas[level].towerHeight);
        //    Vector3 spawnPos = new Vector3(2, value, 0.55f);
        //    ObjectPooler.Instance.SpawnFromPool("Obstacle", spawnPos, Quaternion.Euler(0f, 0f, 0f));
        //}
        if(levelDatas[level].spawnPosObstacle != null)
        {
            ObstacleManager.Instance.Spawn(levelDatas[level].spawnPosObstacle);
        }
        if (ballScript != null)
        {
            ballScript.Respawn(new Vector3(0f, 5.5f, 2f));
        }
        else
        {
            Debug.LogError("ballScript is null. Please assign it in the inspector or initialize it in the code.");
        }
        GameManager.Instance.time = levelDatas[level].timer;
    }
    public void LoadCurrentLevel()
    {
        Debug.Log("Load level" + currentLevel);
        LoadLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        currentLevel += 1;
        Data.Instance.SetLevel(currentLevel);
        LoadCurrentLevel();
    }
}
