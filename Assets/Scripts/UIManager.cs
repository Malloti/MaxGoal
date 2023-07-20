using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public int beforeCoins, afterCoins, result;

    private Text pointsUI, attempsUI;
    private GameObject losePanel;
    private GameObject winPanel;
    private GameObject pausePanel;
    private Button btnPause, btnResume, btnReplay, btnMenu, btnForward;
    
    public void UpdateUI()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
        attempsUI.text = GameManager.instance.attemps.ToString();
        afterCoins = PlayerPrefs.GetInt("coinsSave");
    }

    public void ShowGameOver()
    {
        losePanel.SetActive(true);
    }

    public void ShowYouWin()
    {
        winPanel.SetActive(true);
    }

    public void StartUI()
    {
        OffPanel();
    }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Load;

        FindGameObjects();
        OffPanel();
    }

    void Load(Scene scene, LoadSceneMode mode)
    {
        if (WhereAmI.instance.level > 2) {
            return;
        }

        FindGameObjects();
    }

    void FindGameObjects()
    {
        pointsUI = GameObject.Find("CoinsValue").GetComponent<Text>();
        attempsUI = GameObject.Find("Attemps").GetComponent<Text>();
        losePanel = GameObject.Find("LosePanel");
        winPanel = GameObject.Find("WinPanel");
        pausePanel = GameObject.Find("PausePanel");
        btnPause = GameObject.Find("btnPause").GetComponent<Button>();
        btnPause.onClick.AddListener(Pause);
        btnResume = GameObject.Find("btnPlay").GetComponent<Button>();
        btnResume.onClick.AddListener(Resume);
        btnForward = GameObject.Find("btnForward").GetComponent<Button>();
        btnForward.onClick.AddListener(Forward);

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("ButtonReplay")) {
            btnReplay = gameObject.GetComponent<Button>();
            btnReplay.onClick.AddListener(Replay);
        }

        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("ButtonMenu")) {
            btnMenu = gameObject.GetComponent<Button>();
            btnMenu.onClick.AddListener(Menu);
        }

        beforeCoins = PlayerPrefs.GetInt("coinsSave");
    }

    void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().Play("pausePanelShow");
    }

    void Resume()
    {
        pausePanel.GetComponent<Animator>().Play("pausePanelReturn");
        Time.timeScale = 1;
        StartCoroutine(WaitResume());
    }

    void Replay()
    {
        LevelManager.LevelLoad(WhereAmI.instance.level);
        Time.timeScale = 1;

        if (!GameManager.instance.win) {
            result = afterCoins - beforeCoins;
            ScoreManager.instance.LoseCoins(result);
            result = 0;
        } 
    }

    void Menu()
    {
        LevelManager.LevelLoad(1);

        if (!GameManager.instance.win) {
            result = afterCoins - beforeCoins;
            ScoreManager.instance.LoseCoins(result);
            result = 0;
        }
    }

    void Forward()
    {
        if (!GameManager.instance.win) {
            return;
        }

        LevelManager.LevelLoad(WhereAmI.instance.level + 1);
    }

    void OffPanel()
    {
        StartCoroutine(WaitOffPanels());
    }

    IEnumerator WaitResume()
    {
        yield return new WaitForSeconds(0.5f);
        pausePanel.SetActive(false);
    }

    IEnumerator WaitOffPanels()
    {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}
