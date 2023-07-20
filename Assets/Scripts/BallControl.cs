using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    // rotate
    public GameObject arrowGo;
    public float zRotate;
    public bool allowRotate = false;
    public bool allowShoot = false;

    // force
    public GameObject fillableArrow;
    private Rigidbody2D ball;
    private float force = 0;
    private bool ballAwake = false;

    // walls
    private Transform wallLeft;
    private Transform wallRight;

    // death
    [SerializeField]
    private GameObject deathAnim;

    public void SetBallDinamic()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    private void Awake()
    {
        arrowGo = GameObject.Find("Arrow");
        fillableArrow = arrowGo.transform.GetChild(0).gameObject;
        arrowGo.GetComponent<Image>().enabled = false;
        fillableArrow.GetComponent<Image>().enabled = false;
        wallLeft = GameObject.Find("WallLeft").GetComponent<Transform>();
        wallRight = GameObject.Find("WallRight").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetArrowPosition(transform.position);
        ControlRotate();

        ApplyForce();
        ControlForce();

        CheckLimit();
    }

    void SetArrowPosition(Vector3 position)
    {
        arrowGo.GetComponent<Image>().rectTransform.position = position;
    }

    void SetArrowAngle(Vector3 angle)
    {
        arrowGo.GetComponent<Image>().rectTransform.eulerAngles = angle;
    }

    void ControlRotate()
    {
        if (!allowRotate) {
            return;
        }

        float moveY = Input.GetAxis("Mouse Y");

        if (zRotate < 90 && moveY > 0) {
            zRotate += 1.5f;
        }

        if (zRotate > 0 && moveY < 0) {
            zRotate -= 1.5f;
        }

        if (zRotate >= 90) {
            zRotate = 90;
        }

        if (zRotate <= 0) {
            zRotate = 0;
        }

        SetArrowAngle(new Vector3(0, 0, zRotate));
    }

    void ApplyForce()
    {
        float x = force * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = force * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (allowShoot) {
            ball.AddForce(new Vector2(x, y));
            StartCoroutine(SetBallAwake());
            allowShoot = false;
        }
    }

    void ControlForce()
    {
        if (!allowRotate) {
            return;
        }

        float moveX = Input.GetAxis("Mouse X");
        if (moveX < 0) {
            fillableArrow.GetComponent<Image>().fillAmount += 1.8f * Time.deltaTime;
        }

        if (moveX > 0) {
            fillableArrow.GetComponent<Image>().fillAmount -= 1.8f * Time.deltaTime;
        }

        force = fillableArrow.GetComponent<Image>().fillAmount * 1000;
    }

    void CheckLimit()
    {
        if (this.gameObject.transform.position.x > wallRight.position.x) {
            Destroy();
        }

        if (this.gameObject.transform.position.x < wallLeft.position.x) {
            Destroy();
        }

        if (ballAwake && ball.velocity.magnitude <= .2f) {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
        GameManager.instance.sceneBalls -= 1;
        GameManager.instance.attemps -= 1;
    }

    void OnMouseDown()
    {
        if (GameManager.instance.shot == 0) {
            allowRotate = true;
            allowShoot = false;
            arrowGo.GetComponent<Image>().enabled = true;
            fillableArrow.GetComponent<Image>().enabled = true;
        }
    }

    void OnMouseUp()
    {
        allowRotate = false;
        arrowGo.GetComponent<Image>().enabled = false;
        fillableArrow.GetComponent<Image>().enabled = false;
        if (GameManager.instance.shot == 0 && force > 0) {
            allowShoot = true;
            AudioManager.instance.PlaySoundEffect(1);
            GameManager.instance.shot = 1;
            fillableArrow.GetComponent<Image>().fillAmount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Die")) {
            Instantiate(deathAnim, transform.position, Quaternion.identity);
            Destroy();
        }

        if (collision.gameObject.CompareTag("Finish")) {
            GameManager.instance.win = true;
            int nextLevel = WhereAmI.instance.level + 1;
            PlayerPrefs.SetInt(LevelManager.GetLevelName(nextLevel), 1);
        }
    }

    IEnumerator SetBallAwake()
    {
        yield return new WaitForSeconds(.1f);
        ballAwake = true;
    }
}
