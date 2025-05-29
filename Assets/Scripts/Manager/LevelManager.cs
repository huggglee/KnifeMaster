using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private string levelPath = "Levels";
    public Dictionary<int, LevelData> levelDatas = new Dictionary<int, LevelData>();

    [SerializeField] GameObject tower;
    private Tower towerScript;

    [SerializeField] GameObject finish;
    private Finish finishScript;

    [SerializeField] GameObject ball;
    private BallController ballScript;

    private GameObject[] knifes;

    public int currentLevel = 1;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
        towerScript = tower.GetComponent<Tower>();
        finishScript = finish.GetComponent<Finish>();
        ballScript = ball.GetComponent<BallController>();
        loadData();
        //Invoke("loadNextLevel", 4f);
    }

    public void loadData()
    {
        List<LevelData> leveldatas = Resources.LoadAll<LevelData>(levelPath).ToList();
        levelDatas = leveldatas.ToDictionary(i => i.level);
        //Debug.Log(levelDatas.Count);
    }

    public void loadLevel()
    {
        towerScript.Spawn(levelDatas[currentLevel].towerHeight);
        finishScript.Spawn(levelDatas[currentLevel].towerHeight);
        knifes = GameObject.FindGameObjectsWithTag("Knife");
        foreach (GameObject knife in knifes)
        {
            Knife knifeScript = knife.GetComponent<Knife>();
            knifeScript.UndonoForce();
        }
        KnifeThrower.instance.resetHeight();
        //ObjectPooler.instance.ReturnAllToPool();
        //ballScript.Respawn(new Vector3(ball.transform.position.x, 5f, ball.transform.position.z));
        //ballScript.Respawn(new Vector3(0f, 5f, 2f));
    }

    public void loadNextLevel()
    {
        currentLevel += 1;
        loadLevel();
    }

    void Update()
    {
        
    }
}
