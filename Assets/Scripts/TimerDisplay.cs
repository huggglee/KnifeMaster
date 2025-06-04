using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public float time;
    private TextMeshProUGUI timeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
        //time = GameManager.instance.time;
    }

    // Update is called once per frame
    void Update()
    {
        time = GameManager.Instance.time;
        timeText.SetText(time.ToString("F2"));
        //if(GameManager.instance.state == GameManager.gameState.Playing)
        //{
        //    time -= Time.deltaTime;
        //} else if (GameManager.instance.state == GameManager.gameState.Pause)
        //{
            
        //}
    }
}
