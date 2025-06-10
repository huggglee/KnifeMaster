using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Scripts")]
    [SerializeField] Tower towerScript;
    [SerializeField] Finish finishScript;
    [SerializeField] BallController ballScript;
    public int currentLevel;
    public UnityAction DataLoaded;
    public Dictionary<int, LevelData> levelDatas = new Dictionary<int, LevelData>();

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
        currentLevel = Data.Instance.GetCurrentLevel();
        LoadData();
        //Invoke("LoadNextLevel", 4f);
        LoadCurrentLevel();
    }

    public void LoadData()
    {
        List<LevelData> leveldatas = Resources.LoadAll<LevelData>(levelPath).ToList();
        levelDatas = leveldatas.ToDictionary(i => i.level);
        //DataLoaded.Invoke();
    }

    public void LoadLevel(int level)
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.SetState(GameManager.gameState.onLoad, 0.2f));
        //Debug.Log(levelDatas[level].timer);
        KnifeThrower.Instance._currentKnife = null;
        //KnifeThrower.Instance.ResetHeight();
        if (!levelDatas.ContainsKey(level))
        {
            Debug.LogError($"Level {level} not found in levelDatas dictionary.");
            return;
        }
        knifes = GameObject.FindGameObjectsWithTag("Knife");
        foreach (GameObject knife in knifes)
        {
            Knife knifeScript = knife.GetComponent<Knife>();
            knifeScript.UndoNoForce();
        }
        towerScript.Spawn(levelDatas[level].towerHeight);
        finishScript.Spawn( levelDatas[level].towerHeight);
        if (ballScript != null)
        {
        }
        else
        {
            Debug.LogError("ballScript is null. Please assign it in the inspector or initialize it in the code.");
        }
        GameManager.Instance.time = levelDatas[level].timer;
        //Debug.Log(GameManager.Instance.time);

    }
    public void LoadCurrentLevel()
    {
        LoadLevel(currentLevel);
    }

    public void LoadNextLevel()
    {
        Data.Instance.SetLevel(currentLevel);
        currentLevel += 1;
        LoadCurrentLevel();
        ballScript.Respawn(new Vector3(0f, 8f, 2f));
    }

    void Update()
    {
        
    }
}
