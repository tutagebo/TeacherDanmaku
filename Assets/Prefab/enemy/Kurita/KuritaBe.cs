using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuritaBe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        state = Status.nomal;
        StartCoroutine("attackB");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Shinai;
    private IEnumerator attackA(){
        float j = 0;
        while(j<6){
            GameObject[] shinaiAry = new GameObject[13];
            for(int i=0;i<=12;i++){
                Vector2 spawnPos = new Vector2(5*Mathf.Cos(Mathf.PI*(i+(j%3)/2)/12),-5*Mathf.Sin(Mathf.PI*(i+(j%3)/2)/12)+4f);
                shinaiAry[i] = Instantiate(Shinai,spawnPos,Quaternion.Euler(0.0f, 0.0f, 450-(i+(j%3)/2)*15));
            }
            yield return new WaitForSeconds(1.0f);
            for(int i=0;i<=12;i++){
                shinaiAry[i].GetComponent<Rigidbody2D>().velocity = new Vector2(6*Mathf.Cos(Mathf.PI*(i+(j%3)/2)/12),-6*Mathf.Sin(Mathf.PI*(i+(j%3)/2)/12));
            }
            j++;
            yield return new WaitForSeconds(1.0f);
        }
        StartCoroutine("attackB");
    }
    private IEnumerator attackB(){
        for(int i=0;i<10;i++){
            GameObject sMove = Instantiate(Shinai,new Vector2(-14f,-1.5f),Quaternion.Euler(0,0,0));
            sMove.transform.localScale = new Vector3(0.2f,0.2f,1.0f);
            sMove.GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f,0f);
            GameObject tMove = Instantiate(Shinai,new Vector2(14f,-4.5f),Quaternion.Euler(0,0,0));
            //tMove.transform.localScale = new Vector3(0.2f,0.2f,1.0f);
            tMove.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.0f,0f);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(5.0f);
        StartCoroutine("attackA");
    }


    //hitted program
    SpriteRenderer myRender;
    private int health = 7;
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
