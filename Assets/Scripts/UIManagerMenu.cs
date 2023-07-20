using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMenu : MonoBehaviour
{
    [SerializeField]
    private Text coins;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.UpdateScore();
        coins.text = PlayerPrefs.GetInt("coinsSave").ToString();
    }
}
