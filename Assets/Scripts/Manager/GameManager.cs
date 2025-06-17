using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum gameState { Playing, Pause, Win, Lose, onLoad, Waiting };
    public gameState state;
    public float time;
    public UnityAction onWin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Data.Instance.SetCoins(1000);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        state = gameState.onLoad;
        //if (LevelManager.Instance != null)
        //{
        //    LevelManager.Instance.DataLoaded += LevelManager.Instance.LoadCurrentLevel;
        //}
    }

    void Update()
    {
        //Debug.Log(state);
        if (state == gameState.onLoad)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Start Game");
                StartCoroutine(SetState(gameState.Playing, 0.3f));
                ScreenManager.Instance.InactiveScreen("StartPanel");
            }
        }

        if (state == gameState.Playing)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                time = 0;
                StartCoroutine(SetState(gameState.Lose, 0f));
            }
        }
    }

    public IEnumerator SetState(gameState newState, float delay)
    {
        yield return new WaitForSeconds(delay);
        state = newState;

        switch (state)
        {
            case gameState.Playing:
                Time.timeScale = 1f;
                ScreenManager.Instance.ActiveScreen("TimerPanel");
                break;

            case gameState.Pause:
                Time.timeScale = 0f;
                break;

            case gameState.Win:
                OnWin();
                break;

            case gameState.Lose:
                OnLose();
                break;

            case gameState.onLoad:
                Time.timeScale = 1f;
                ScreenManager.Instance.InactiveScreen("TimerPanel");
                ScreenManager.Instance.ActiveScreen("StartPanel");
                break;

            //case gameState.Waiting:
            //    Time.timeScale = 0f;
            //    break;
        }
    }

    public void OnWin()
    {
        onWin?.Invoke();
        StartCoroutine(SetState(gameState.Waiting, 0.2f));
        Data.Instance.SetCoins(100+Data.Instance.GetCurrentLevel()*10);
        StartCoroutine(ScreenManager.Instance.ActiveScreen("BlurPanel", 1f));
        StartCoroutine(ScreenManager.Instance.ActiveScreen("WinPanel", 1f));
        //ScreenManager.Instance.ActiveScreen("BlurPanel",0.2f);
        //ScreenManager.Instance.ActiveScreen("WinPanel",0.2f);
    }
    public void OnLose()
    {
        StartCoroutine(SetState(gameState.Waiting, 0f));
        ScreenManager.Instance.ActiveScreen("BlurPanel");
        ScreenManager.Instance.ActiveScreen("LosePanel");
    }


    public void OnCloseMenu()
    {
        //ScreenManager.Instance.InactiveScreen("MenuPanel");
        if (state == gameState.Pause)
        {
            SetPlaying();
        }
    }

    public void OnOpenMenu()
    {
        if(state == gameState.Playing)
        {
            SetPause();
        }
    }

    public void SetPlaying()
    {
        StartCoroutine(SetState(gameState.Playing, 0f));
    }

    public void SetPause()
    {
        StartCoroutine(SetState(gameState.Pause, 0f));
    }
    public void SetOnLoad()
    {
        StartCoroutine(SetState(gameState.onLoad, 0f));
    }

    public void RegisterOnWin(UnityAction callback)
    {
        onWin += callback;
    }
}
