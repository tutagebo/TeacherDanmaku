using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    GameObject player;
    public void shot(){
        player = GameObject.Find("Player");
        Vector2 pPos = player.transform.position;
        Vector2 myPos = gameObject.transform.position;
        gameObject.GetComponent<Rigidbody2D>().velocity = (pPos-myPos).normalized*10;
    }
    void Update()
    {
        if(Mathf.Abs(this.transform.position.y)>8||Mathf.Abs(this.transform.position.x)>14)Destroy(gameObject);
    }
}
