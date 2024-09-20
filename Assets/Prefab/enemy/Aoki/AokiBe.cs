using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AokiBe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        state = Status.nomal;
        StartCoroutine("attackSDGs");
    }

    private float currentTime = 0;
    // Update is called once per frame
    void Update()
    {
        if(health<=0){
            GameObject[] sdgAry = GameObject.FindGameObjectsWithTag("enemy");
            foreach(GameObject sdgObj in sdgAry){
                if(sdgObj!=gameObject)Destroy(sdgObj);
            }
            Destroy(gameObject);
        }
        
        currentTime += Time.deltaTime;
        if(currentTime > 5.0f){
            StartCoroutine("attackSDGs");
            currentTime = 0f;
        }
    }

    public GameObject sdgPart;
    private Color32[] colorAry = new Color32[]{
        new Color32(229,36,59,255),
        new Color32(221,166,58,255),
        new Color32(76,159,56,255),
        new Color32(197,25,45,255),
        new Color32(255,58,33,255),
        new Color32(38,189,226,255),
        new Color32(252,195,11,255),
        new Color32(162,25,66,255),
        new Color32(253,105,37,255),
        new Color32(221,19,103,255),
        new Color32(253,157,36,255),
        new Color32(191,139,46,255),
        new Color32(63,126,68,255),
        new Color32(10,151,217,255),
        new Color32(86,192,43,255),
        new Color32(0,104,157,255),
        new Color32(25,72,106,255)
    };
    private IEnumerator attackSDGs(){
        GameObject[] objAry = new GameObject[17];
        float r = Random.Range(0f,1f);
        for(int i=0;i<17;i++){
            Quaternion rotate = Quaternion.Euler(0f,0f,(i+r)*21.2f);
            objAry[i] = Instantiate(sdgPart,new Vector2(2*Mathf.Cos(Mathf.PI/2+Mathf.PI*2*(i+r)/17),2*Mathf.Sin(Mathf.PI/2+Mathf.PI*2*(i+r)/17)+4),rotate);
            objAry[i].GetComponent<SpriteRenderer>().color = colorAry[i];
        }
        yield return new WaitForSeconds(1.0f);
        for(int i=0;i<17;i++){
            objAry[i].GetComponent<Rigidbody2D>().velocity = new Vector2(2*Mathf.Cos(Mathf.PI/2+Mathf.PI*2*(i+r)/17),2*Mathf.Sin(Mathf.PI/2+Mathf.PI*2*(i+r)/17)).normalized*5;
        }
    }
    //hitted program
    SpriteRenderer myRender;
    private int health = 5;
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
