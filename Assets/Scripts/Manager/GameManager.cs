using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum gameState { Playing,Pause,Win,Lose,onLoad,Waiting};
    public gameState state;
    public float time;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = gameState.onLoad;
        //if(LevelManager.Instance != null)
        //{
        //    LevelManager.Instance.DataLoaded += () =>
        //    {
        //        Debug.Log("DataLoaded");
        //    };
        //}
        //LevelManager.instance.loadLevel();
        //Invoke("SetPause",3f);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(time);
        //Debug.Log(state);
        if (state == gameState.onLoad)
        {
            ScreenManager.Instance.InactiveScreen("TimerPanel");
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(SetState(gameState.Playing,0.3f));
                ScreenManager.Instance.InactiveScreen("StartPanel");
            }
        }
        else if (state == gameState.Playing )
        {
            ScreenManager.Instance.ActiveScreen("TimerPanel");
            Time.timeScale = 1f;
            time -= Time.deltaTime;
        }
        else if (state == gameState.Pause)
        {
            Time.timeScale = 0f;
        }
        else if(state == gameState.Win)
        {
            OnWin();
        }
        else if (state == gameState.Lose)
        {
            OnLose();
        };

        if (time < 0)
        {
            StartCoroutine(SetState(gameState.Lose,0f));
            time = 0f;
        }
    }

    public IEnumerator SetState(gameState gamestate,float time)
    {
        yield return new WaitForSeconds(time);
        state = gamestate;
    }
    public void SetPause()
    {
        state = gameState.Playing;
    }
    public void OnWin()
    {
        ScreenManager.Instance.ActiveScreen("BlurPanel");
        ScreenManager.Instance.ActiveScreen("WinPanel");
        StartCoroutine(SetState(gameState.Waiting, 0f));
    }
    public void OnLose()
    {
        ScreenManager.Instance.ActiveScreen("BlurPanel");
        ScreenManager.Instance.ActiveScreen("LosePanel");
        StartCoroutine(SetState(gameState.Waiting, 0f));
    }
}
