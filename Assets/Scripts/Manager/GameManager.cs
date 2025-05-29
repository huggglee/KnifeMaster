using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum gameState { Playing,Pause,Win,Lose};
    public gameState state;
    public float time=30f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = gameState.Playing;
        LevelManager.instance.loadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == gameState.Playing)
        {
            time -= Time.deltaTime;
        }
        else if (state == gameState.Pause)
        {
        }
        else if(state == gameState.Win)
        {
            onWin();
        }
        else if (state == gameState.Lose)
        {
            onLose();
        };

        if (time < 0)
        {
            setState(gameState.Lose);
        }
    }

    public void setState(gameState gamestate)
    {
        state = gamestate;
    }
    public void onWin()
    {
        Debug.Log("win");
    }
    public void onLose()
    {
        Debug.Log("Lose");
    }
}
