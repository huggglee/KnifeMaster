using UnityEngine;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Button restartBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartBtn.onClick.AddListener(() => OnClickRestart());
    }
    private void OnClickRestart()
    {
        ScreenManager.Instance.InactiveScreen("LoseScreen");
        //ScreenManager.Instance.InactiveScreen("BlurPanel");
        ScreenManager.Instance.ActiveScreen("StartScreen");
        LevelManager.Instance.LoadCurrentLevel();
    }
}
