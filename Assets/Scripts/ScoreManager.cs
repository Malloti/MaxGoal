using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int coins;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void StartGameScore()
    {
        coins = PlayerPrefs.HasKey("coinsSave") ? PlayerPrefs.GetInt("coinsSave") : 100;
        SaveCoins();
    }

    public void UpdateScore()
    {
        coins = PlayerPrefs.GetInt("coinsSave");
    }

    public void CollectCoins(int coin)
    {
        coins += coin;
        SaveCoins();
    }

    public void LoseCoins(int coin)
    {
        coins -= coin;
        SaveCoins();
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("coinsSave", coins);
    }
}
