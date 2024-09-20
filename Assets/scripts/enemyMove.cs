using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public GameObject EnemyBullet;
    // Update is called once per frame
    private int hitcount = 0;
    void Start() {
        InvokeRepeating("summon",0.0f,0.5f);
    }
    void Update()
    {
        Vector3 v = new Vector3(10*Mathf.Sin(Time.time),5f,0f);
        this.gameObject.transform.position = v;
        if(hitcount==10)this.gameObject.SetActive(false);
    }

    private void summon(){
        if(hitcount<10)Instantiate(EnemyBullet,transform.position,Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("playerBullet"))hitcount++;
    }
}
