using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI coin_txt;
    public TextMeshProUGUI level_txt;
    public Button menuBtn;
    public Button knifeSkinBtn;
    void Start()
    {
        SetCoins();
        SetLevelText();
        Data.Instance.RegisterOnChange(SetCoins);
        Data.Instance.RegisterOnChange(SetLevelText);
        menuBtn.onClick.AddListener(() => OnClickMenu());
        knifeSkinBtn.onClick.AddListener(() => OnClickKnifeSkin());
    }

    
    void Update()
    {
        
    }
    void SetCoins()
    {
        coin_txt.text = Data.Instance.GetCoins().ToString();
    }
    
    void SetLevelText()
    {
        level_txt.text = "LEVEL "+ Data.Instance.GetCurrentLevel().ToString();
    }
    void OnClickMenu()
    {
        ScreenManager.Instance.ActiveScreen("MenuScreen");
        //ScreenManager.Instance.ActiveScreen("BlurPanel");
        GameManager.Instance.OnOpenMenu();
    }

    void OnClickKnifeSkin()
    {
        ScreenManager.Instance.InactiveScreen("StartScreen");
        ScreenManager.Instance.ActiveScreen("ChooseSkinScreen");
    }
}
