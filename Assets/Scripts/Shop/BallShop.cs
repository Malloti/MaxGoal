using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShop : MonoBehaviour
{
    public static BallShop instance;

    public List<Ball> balls = new List<Ball>();
    public List<GameObject> ballPanels = new List<GameObject>();
    public List<GameObject> buttons = new List<GameObject>();

    public GameObject ballPanelGO;
    public Transform content;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FillList();
    }

    void FillList()
    {
        foreach (Ball ball in balls) {
            GameObject item = Instantiate(ballPanelGO);
            item.transform.SetParent(content, false);
            BallPanel panel = item.GetComponent<BallPanel>();

            panel.id = ball.id;
            panel.ballPrice.text = ball.price.ToString();
            panel.btnBuy.GetComponent<BallBuy>().ballId = ball.id;

            if (PlayerPrefs.GetInt("ball_sold" + ball.id) == 1) {
                ball.sold = true;
            }

            if (PlayerPrefs.HasKey("ball_btn" + ball.id) && ball.sold) {
                panel.btnBuy.GetComponent<BallBuy>().btnText.text = PlayerPrefs.GetString("ball_btn" + ball.id);
            }

            if (ball.sold) {
                panel.ballSprite.sprite = Resources.Load<Sprite>("Sprites/" + ball.name);
                panel.ballPrice.text = "SOLD";

                if (!PlayerPrefs.HasKey("ball_btn" + ball.id)) {
                    panel.btnBuy.GetComponent<BallBuy>().btnText.text = "Using";
                }
            } else {
                panel.ballSprite.sprite = Resources.Load<Sprite>("Sprites/" + ball.name + "_cinza");
            }

            buttons.Add(panel.btnBuy);
            ballPanels.Add(item);
        }
    }

    public void UpdateSprite(int ballId)
    {
        GameObject ballPanelGO = ballPanels.Find(x => x.GetComponent<BallPanel>().id == ballId);
        Ball ball = balls.Find(x => x.id == ballId);

        BallPanel panel = ballPanelGO.GetComponent<BallPanel>();
        if (ball.sold) {
            panel.ballSprite.sprite = Resources.Load<Sprite>("Sprites/" + ball.name);
            panel.ballPrice.text = "SOLD";
            PlayerPrefs.SetInt("ball_sold" + ball.id, 1);
        } else {
            panel.ballSprite.sprite = Resources.Load<Sprite>("Sprites/" + ball.name + "_cinza");
        }
    }

    public void BankruptConfirm()
    {
        GameObject.FindGameObjectWithTag("Bankrupt").GetComponent<Animator>().Play("bankruptInverse");
    }
}
