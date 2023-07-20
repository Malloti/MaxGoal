using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBuy : MonoBehaviour
{
    public int ballId;
    public Text btnText;
    
    private Animator bankrupt;

    public void Buy()
    {
        Ball ball = BallShop.instance.balls.Find(x => x.id == ballId);
        if (!ball.sold && PlayerPrefs.GetInt("coinsSave") >= ball.price) {
            ball.sold = true;
            ScoreManager.instance.LoseCoins(ball.price);
            GameObject.Find("CoinsValue").GetComponent<Text>().text = ScoreManager.instance.coins.ToString();
        } else if (!ball.sold && PlayerPrefs.GetInt("coinsSave") < ball.price) {
            GameObject.FindGameObjectWithTag("Bankrupt").GetComponent<Animator>().Play("bankrupt");
            return;
        }
        UpdateText();
        BallShop.instance.UpdateSprite(ballId);
    }

    void UpdateText()
    {
        btnText.text = "Using";
        WhereAmI.instance.usingBall = ballId;
        PlayerPrefs.SetInt("using_ball", ballId);

        foreach (GameObject button in BallShop.instance.buttons) {
            BallBuy ballBuy = button.GetComponent<BallBuy>();

            foreach (Ball ball in BallShop.instance.balls) {
                if (ball.id != ballBuy.ballId) {
                    continue;
                }

                if (ball.sold && ball.id != ballId) {
                    ballBuy.btnText.text = "Use";
                }

                PlayerPrefs.SetString("ball_btn" + ball.id, ballBuy.btnText.text);
            }
        }
    }
}
