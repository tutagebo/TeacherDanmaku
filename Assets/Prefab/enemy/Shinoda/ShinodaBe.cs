using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinodaBe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("attack");
        state = Status.nomal;
    }


    float currentTime = 0;
    // Update is called once per frame
    void Update()
    {
        if(health<=0)Destroy(gameObject);
        
        currentTime += Time.deltaTime;
        if(currentTime > 3.0f){
            StartCoroutine("attack");
            currentTime = 0f;
        }
    }

    public GameObject tri;

    private IEnumerator attack(){
        yield return new WaitForSeconds(0.3f);
        float dis = Random.Range(3.0f,6.0f);
        float cou = 26/dis;
        for(int i=0;i<Mathf.Floor(cou)+1;i++){
            Instantiate(tri,new Vector2(-13+i*dis,5.5f),Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
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