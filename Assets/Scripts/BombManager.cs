using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    [SerializeField]private GameObject bombFx;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")) {
            Instantiate(bombFx, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
    }
}
