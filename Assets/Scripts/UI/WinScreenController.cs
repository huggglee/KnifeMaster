using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenController : MonoBehaviour
{
    public TextMeshProUGUI coinTxt;
    public Button claimBtn;
    void Start()
    {
        GameManager.Instance.RegisterOnWin(SetCoin);
        claimBtn.onClick.AddListener(() => OnClickClaim());
    }
    private void SetCoin()
    {
        int coins = Data.Instance.GetCurrentLevel()*10 +100;
        coinTxt.SetText(coins.ToString());
    }

    private void OnClickClaim()
    {
        Data.Instance.SetCoins(100 + Data.Instance.GetCurrentLevel() * 10);
        //ScreenManager.Instance.InactiveScreen("BlurPanel");
        ScreenManager.Instance.InactiveScreen("WinScreen");
        ScreenManager.Instance.ActiveScreen("StartScreen");
        LevelManager.Instance.LoadNextLevel();
    }
}
