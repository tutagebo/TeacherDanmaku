using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float currentTime;
    private byte able = 0;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > 5){
            able = 1;
        }
        GameObject Koucho = GameObject.Find("Koucho");
        GameObject Player = GameObject.Find("Player");
        Vector2 direct = Player.transform.position - gameObject.transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = direct.normalized*4;
        int kHealth = Koucho.GetComponent<KouchoBe>().kouchoHealth;
        if(kHealth<9)Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")||(other.gameObject.CompareTag("playerBullet")&&able==1)){
            Destroy(gameObject);
        }
    }
}
