using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLife : MonoBehaviour
{
    private GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        bomb = GameObject.Find("TNT");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Life());
    }

    IEnumerator Life()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(bomb.gameObject);
        Destroy(this.gameObject);
    }
}
