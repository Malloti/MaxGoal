using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    [SerializeField]
    private Transform objLeft, objRight;
    private Transform ball;
    private float t = 1;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameInit) {
            return;
        }

        if (transform.position.x != objLeft.position.x) {
            t -= .08f * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.SmoothStep(objLeft.position.x, Camera.main.transform.position.x, t),
                this.transform.position.y,
                this.transform.position.z
            );
        } 

        if (GameManager.instance.sceneBalls <= 0) {
            return;
        }

        if (ball == null) {
            ball = GameObject.Find("Ball(Clone)").GetComponent<Transform>();
        }

        Vector3 newPosition = transform.position;
        newPosition.x = ball.position.x;
        newPosition.x = Mathf.Clamp(newPosition.x, objLeft.position.x, objRight.position.x);

        transform.position = newPosition;
    }
}
