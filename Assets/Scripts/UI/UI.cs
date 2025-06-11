using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI coin_txt;
    public TextMeshProUGUI level_txt;
    void Start()
    {
        SetCoins();
        SetLevelText();
        Data.Instance.RegisterOnChange(SetCoins);
        Data.Instance.RegisterOnChange(SetLevelText);
    }

    // Update is called once per frame
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
}
