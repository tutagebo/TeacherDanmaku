using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShijoBe : MonoBehaviour
{
    string[] brushAry = new string[] {"brCy","brMg","brYe"};
    // Start is called before the first frame update
    void Start()
    {
        state = Status.nomal;
        InvokeRepeating("attackA",2.0f,5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)Destroy(gameObject);
    }

    private void attackA(){
        int a = Random.Range(0,4);
        float b = Random.Range(-1f,1f);
        Vector2 c = new Vector2(0,0);
        switch(a){
            case 0:{
                c.x = b*20f;
                c.y = 10f;
                break;
            }
            case 1:{
                c.x = b*20f;
                c.y = -10f;
                break;
            }
            case 2:{
                c.x = 20f;
                c.y = b*10f;
                break;
            }
            case 3:{
                c.x = -20f;
                c.y = b*10f;
                break;
            }
        }
        int r = Random.Range(0,3);
        GameObject brush = (GameObject)Resources.Load(brushAry[r]);
        GameObject shot = Instantiate(brush,c,Quaternion.identity);
        Vector2 direction = new Vector2(Random.Range(-10f,10f),0) - c;
        shot.GetComponent<Rigidbody2D>().velocity = direction.normalized*20;
    }
    //hitted program
    SpriteRenderer myRender;
    enum Status{
        hitted,
        nomal
    }
    Status state;
    int health = 6;

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
