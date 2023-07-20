using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int shot = 0;
    public int sceneBalls = 0;
    public int attemps = 2;
    public bool win = false;
    public bool gameInit;

    [SerializeField]private GameObject[] balls;
    private Transform respawn;

    private bool adsPlayed = false;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;
        respawn = GameObject.Find("InitialPosition").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();

        if (attemps > 0 && sceneBalls == 0 && Camera.main.transform.position.x <= 0.05f) {
            SpawnBall();
        }

        if (attemps <= 0) {
            GameOver();
        }

        if (win) {
            WinGame();
        }
    }

    void SpawnBall()
    {
        //, , Quaternion.identity
        Instantiate(balls[WhereAmI.instance.usingBall], respawn.transform.position, Quaternion.identity);
        sceneBalls += 1;
        shot = 0;
    }

    void Load(Scene scene, LoadSceneMode mode)
    {
        if (WhereAmI.instance.level > 2) {
            respawn = GameObject.Find("InitialPosition").GetComponent<Transform>();
            StartGame();
            ScoreManager.instance.StartGameScore();
        }
    }

    void GameOver()
    {
        UIManager.instance.ShowGameOver();
        gameInit = false;

        if (!adsPlayed) {
            UnityAds.instance.Show();
            adsPlayed = true;
        }
    }

    void WinGame()
    {
        UIManager.instance.ShowYouWin();
        gameInit = false;
    }

    void StartGame()
    {
        gameInit = true;
        attemps = 2;
        sceneBalls = 0;
        win = false;
        UIManager.instance.StartUI();
        adsPlayed = false;
    }
}
