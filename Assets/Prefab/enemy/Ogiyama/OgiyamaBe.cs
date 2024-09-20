using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgiyamaBe : MonoBehaviour
{
    public GameObject JaguchiA;
    public GameObject JaguchiB;
    public GameObject JaguchiC;
    string[] wordPAry = new string[] {"brick","execute","micro","replace","spectator","spread"};
    // Start is called before the first frame update
    void Start()
    {
        JaguchiA.SetActive(false);
        JaguchiB.SetActive(false);
        JaguchiC.SetActive(false);
        InvokeRepeating("attackA",7.0f,10.0f);
        InvokeRepeating("attackB",1.0f,0.4f);
        state = Status.nomal;
    }
    private float currentTime = 0f;
    private float span = 0.1f;
    private int counter = 0;
    // Update is called once per frame
    void Update(){
        if(health<=0){
            GameObject[] objAry = GameObject.FindGameObjectsWithTag("enemy");
            foreach(GameObject obj in objAry)Destroy(obj);
        }
        //timer
        currentTime += Time.deltaTime;
        if(currentTime > span){
            counter++;
            currentTime = 0f;
        }
        if(counter%10==1){
            
        }
    }
    private void attackA(){ //Jaguchi
        float weight = -12f;
        GameObject[] JagAry = new GameObject[] {JaguchiA,JaguchiB,JaguchiC};
        for(int i=0;i<3;i++){
            weight += Random.Range(4,8);
            JagAry[i].transform.position = new Vector3(weight,6,0);
            JagAry[i].SetActive(true);
        }
    }
    private void attackB(){
        int r = Random.Range(0,6);
        GameObject bullet = (GameObject)Resources.Load(wordPAry[r]);
        GameObject shot = Instantiate(bullet,transform.position,Quaternion.identity);
        Vector2 direction = Random.insideUnitCircle;
        direction.y = -Mathf.Abs(direction.y);
        Rigidbody2D buRigidbody = shot.GetComponent<Rigidbody2D>();
        buRigidbody.velocity = direction.normalized*10;
        buRigidbody.AddTorque(20.0f*Mathf.PI,ForceMode2D.Force);
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
