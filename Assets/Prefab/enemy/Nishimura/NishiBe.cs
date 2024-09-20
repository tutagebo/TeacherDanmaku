using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NishiBe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        state = Status.nomal;
        InvokeRepeating("attack",2.0f,2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)Destroy(gameObject);
        gameObject.transform.position = new Vector2(Mathf.Sin(Time.time)*10,4);
    }

    public GameObject Bullet;
    public GameObject player;

    private void attack(){
        GameObject shot = Instantiate(Bullet,transform.position,Quaternion.identity);
        Vector2 direction = player.transform.position - gameObject.transform.position ;
        shot.GetComponent<Rigidbody2D>().velocity = direction.normalized*10;
    }
    //hitted program
    SpriteRenderer myRender;
    enum Status{
        hitted,
        nomal
    }
    Status state;
    int health = 5;

    private IEnumerator hitAnim()
    {
        health--;
        state = Status.hitted;
        myRender = GetComponent<SpriteRenderer>();
        for(int i=0;i<10;i++){
            yield return new WaitForSeconds(0.1f);
            myRender.material.color = myRender.material.color - new Color32(0,0,0,255);
            yield return new WaitForSeconds(0.1f);
            myRender.material.color = myRender.material.color + new Color32(0,0,0,255);
        }
        state = Status.nomal;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("playerBullet") && state!=Status.hitted){
            Destroy(other.gameObject);
            StartCoroutine("hitAnim");
        }
    }
}
