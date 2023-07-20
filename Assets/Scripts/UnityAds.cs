using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnityAds : MonoBehaviour
{
    public static UnityAds instance;
    public bool adsReward = false;

    private Button btnAds;

    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += GetBtn;
    }

    void GetBtn(Scene scene, LoadSceneMode mode) 
    {
        if (scene.buildIndex != 1) {
            return;
        }

        btnAds = GameObject.Find("BtnAds").GetComponent<Button>();
        btnAds.onClick.AddListener(ShowRewarded);
    }

    void ShowRewarded()
    {
        if (Advertisement.IsReady("rewardedVideo")) {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = AnaliseResult });
        }
    }

    void AnaliseResult(ShowResult result)
    {
        if (result == ShowResult.Finished) {
            ScoreManager.instance.CollectCoins(100);
            LevelManager.LevelLoad(WhereAmI.instance.level);
        }
    }
    
    public void Show()
    {
        if (!PlayerPrefs.HasKey("AdsUnity")) {
            PlayerPrefs.SetInt("AdsUnity", 1);
            return;
        }
        if (PlayerPrefs.GetInt("AdsUnity") != 3) {
            PlayerPrefs.SetInt("AdsUnity", PlayerPrefs.GetInt("AdsUnity") + 1);
            return;
        }

        if (Advertisement.IsReady("video")) {
            Advertisement.Show("video");
        }

        PlayerPrefs.SetInt("AdsUnity", 1);
    }
}
