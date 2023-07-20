using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool active;
        public int unlocked;
    }

    public GameObject button;
    public Transform buttonPlace;
    public List<Level> levelList;

    public static void LevelLoad(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        Time.timeScale = 1;
    }

    public static string GetLevelName(int buildIndex)
    {
        if (buildIndex < 3) {
            return null;
        }

        buildIndex -= 2;

        return "Level" + buildIndex;
    }

    void ListAdd()
    {
        foreach (Level level in levelList) {
            GameObject newButton = Instantiate(button);
            LevelButton newBtnLevel = newButton.GetComponent<LevelButton>();

            newBtnLevel.levelTxt.text = level.levelText;

            if (PlayerPrefs.GetInt("Level" + level.levelText) == 1) {
                level.active = true;
                level.unlocked = 1;
            }

            newBtnLevel.unlocked = level.unlocked;
            newBtnLevel.GetComponent<Button>().interactable = level.active;

            newBtnLevel.GetComponent<Button>().onClick.AddListener(() => LevelClick("Level" + level.levelText));

            newButton.transform.SetParent(buttonPlace, false);
        }
    }

    private void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    // Start is called before the first frame update
    void Start()
    {
        ListAdd();
    }


    void LevelClick(string level)
    {
        SceneManager.LoadScene(level);
    }
}
