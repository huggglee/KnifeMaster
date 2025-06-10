using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public float time;
    private TextMeshProUGUI timeText;
    void Start()
    {
        timeText = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        time = GameManager.Instance.time;
        timeText.SetText(time.ToString("F2"));
    }
}
