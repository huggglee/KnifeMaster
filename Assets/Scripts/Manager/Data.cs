using UnityEngine;
using UnityEngine.Events;

public class Data : MonoBehaviour
{
    public static Data Instance;
    public string Level_Key = "K_Level";
    public string Knife_Speed_Key = "K_Knife_Speed";
    public string Easy_Fever_Key = "K_Easy_Fever";
    public string Fever_Bounce_Key = "K_Fever_Bounce";
    public string Coin_Key = "K_Coin";
    public UnityAction OnChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetLevel(int level)
    {
        int currentLevel = GetCurrentLevel();
        if (level > currentLevel)
        {
            PlayerPrefs.SetInt(Level_Key, level);
            PlayerPrefs.Save();
        }
        OnChange?.Invoke();
    }
    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(Level_Key, 1);
    }

    public void SetCoins(int coins)
    {
        int currentCoins = GetCoins();
        PlayerPrefs.SetInt(Coin_Key, currentCoins + coins);
        PlayerPrefs.Save();
        OnChange?.Invoke();
    }
    public int GetCoins()
    {
        return PlayerPrefs.GetInt(Coin_Key, 0);
    }

    public void SetLevelBoost(string key,int level)
    {
        PlayerPrefs.SetInt(key, level);
        PlayerPrefs.Save();
        OnChange?.Invoke();
    }

    public int GetLevelBoost(string key)
    {
        return PlayerPrefs.GetInt(key, 1);
    }

    public void RegisterOnChange(UnityAction callback)
    {
        OnChange += callback;
    }
}
