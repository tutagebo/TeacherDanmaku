using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatsuBe : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bullet;
    public GameObject Isu;
    public GameObject Suika;
    public GameObject player;
    public int health = 12;
    void Start()
    {
        state = Status.nomal;
        myRender = GetComponent<SpriteRenderer>();
    }
    private float currentTime = 0f;
    private float span = 3f;
    // Update is called once per frame
    void Update()
    {
        if(health<=0)Destroy(gameObject);

        span = (health/3==3) ? 0.3f:
               (health/3==2) ? 0.5f:
               (health/3==1) ? 0.7f:
                               1.0f;
        currentTime += Time.deltaTime;
        if(currentTime > span){
            attackCtrl();
            currentTime = 0f;
        }
    }
    private void attackCtrl(){
        switch(health/3){
            case 3:{
                attackA();
                break;
            }
            case 2:{
                attackB();
                break;
            }
            case 1:{
                attackC();
                break;
            }
            case 0:{
                attackD();
                break;
            }
        }
    }
    private void attackA(){     //0
        GameObject shot = Instantiate(Bullet,transform.position,Quaternion.identity);
        Vector2 direction = new Vector2(Mathf.Cos(Time.time),-Mathf.Abs(Mathf.Sin(Time.time)));
        changeTex(shot);
        shot.GetComponent<Rigidbody2D>().velocity = direction.normalized*10;
    }
    private void attackB(){     //1
        for (float i = -1; i < 2; i++){
            Vector3 offset = new Vector3(i,0,0);
            GameObject shot = Instantiate(Bullet,transform.position + offset,Quaternion.identity);
            changeTex(shot);
            Vector2 direction = Random.insideUnitCircle;
            direction.y = -Mathf.Abs(direction.y);
            shot.GetComponent<Rigidbody2D>().velocity = direction.normalized*10;
        }
    }
    private void attackC(){     //2 isu
        GameObject shot = Instantiate(Isu,transform.position,Quaternion.identity);
        Vector2 direction = player.transform.position - gameObject.transform.position ;
        shot.GetComponent<Rigidbody2D>().velocity = direction.normalized*15;
    }
    private void attackD(){     //3 suika
        if(GameObject.Find("Suika"))return;
        GameObject shot = Instantiate(Suika,transform.position,Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized*500);
    }

    public Sprite[] zuAry;
    private SpriteRenderer zuRender;

    private void changeTex(GameObject zu){
        zuRender = zu.GetComponent<SpriteRenderer>();
        int r = Random.Range(0,7);
        zuRender.sprite = zuAry[r];
    }

    //hitted program
    SpriteRenderer myRender;
    enum Status{
        hitted,
        nomal
    }
    Status state;

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