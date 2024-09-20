using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class UchizumiBe : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;
    public GameObject Bullet;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        myRender = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        state = Status.nomal;

        InvokeRepeating("attack",2.0f,2.0f);
        myRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(5,Random.value*10-5);
        myRigidbody.AddForce(force.normalized*500);
    }

    void Update()
    {
        if(health<=0)Destroy(gameObject);
    }
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
        if((other.gameObject.CompareTag("playerBullet")) && state!=Status.hitted){
            Destroy(other.gameObject);
            StartCoroutine("hitAnim");
        }
        if(other.gameObject.CompareTag("wall"))myCollider.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        myCollider.isTrigger = true;
    }
}
