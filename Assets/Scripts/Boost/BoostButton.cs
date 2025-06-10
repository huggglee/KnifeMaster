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
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = Data.Instance.GetLevelBoost(key);
        coin = GameManager.Instance.GetCoin();
        currentPrice = price * currentLevel;
        if (coin >= currentPrice)
        {
            coinText.color = Color.white;
        }
        else coinText.color = Color.red;
        coinText.SetText(currentPrice.ToString());
        levelText.SetText("Level " + currentLevel.ToString());
    }

    void LevelUp(string key)
    {
        if (coin >= currentPrice)
        {
            Data.Instance.SetLevelBoost(key, currentLevel + 1);
            Data.Instance.SetCoins(-currentPrice);
        }

    }
}
