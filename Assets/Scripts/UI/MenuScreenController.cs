using UnityEngine;
using UnityEngine.UI;

public class MenuScreenController : MonoBehaviour
{
    public Button turnoffBtn;
    public Button restartBtn;
    void Start()
    {
        restartBtn.onClick.AddListener(() => OnClickRestart());
        turnoffBtn.onClick.AddListener(() => OnClickTurnoff());
    }
    private void OnClickRestart()
    {
        ScreenManager.Instance.InactiveScreen("MenuScreen");
        //ScreenManager.Instance.InactiveScreen("BlurPanel");
        LevelManager.Instance.LoadCurrentLevel();
    }
    private void OnClickTurnoff()
    {
        GameManager.Instance.OnCloseMenu();
        ScreenManager.Instance.InactiveScreen("MenuScreen");
        //ScreenManager.Instance.InactiveScreen("BlurPanel");
    }
}
