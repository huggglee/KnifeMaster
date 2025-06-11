using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoostButton : MonoBehaviour
{
    public string key;
    public int price;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI levelText;

    private Button btn;
    private int currentLevel;
    private int currentPrice;
    private int coin;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() => LevelUp(key));
        SetData();
        Data.Instance.RegisterOnChange(SetData);    
    }
    void LevelUp(string key)
    {
        if (coin >= currentPrice)
        {
            Data.Instance.SetCoins(-currentPrice);
            Data.Instance.SetLevelBoost(key, currentLevel + 1);
        }
    }
    void SetData()
    {
        currentLevel = Data.Instance.GetLevelBoost(key);
        coin = Data.Instance.GetCoins();
        currentPrice = price * currentLevel;
        if (coin >= currentPrice)
        {
            coinText.color = Color.white;
        }
        else coinText.color = Color.red;
        coinText.SetText(currentPrice.ToString());
        levelText.SetText("Level " + currentLevel.ToString());
    }
}
