using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class KouchoBe : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cBullet;
    public int kouchoHealth = 12;
    void Start()
    {
        state = Status.nomal;

    }

    private float currentTime = 0;
    private int id = 0;
    // Update is called once per frame
    void Update()
    {
        if(kouchoHealth==0){
            Destroy(this.gameObject);
            SceneManager.LoadScene("ClearScene");
        }
        float span = (kouchoHealth/3==3) ? 1.0f:
                     (kouchoHealth/3==2) ? 2f/3:
                     (kouchoHealth/3==1) ? 1.0f:
                                           0.25f;
        currentTime += Time.deltaTime;
        if(currentTime > span){
            attackCtrl();
            currentTime = 0f;
        }
    }
    private void attackCtrl(){
        switch(kouchoHealth/3){
            case 3:{
                attackA();
                break;
            }
            case 2:{
                StartCoroutine("attackB");
                break;
            }
            case 1:{
                if(id==0)StartCoroutine("attackC");
                break;
            }
            case 0:{
                attackD();
                break;
            }
        }
    }

    private void attackA(){
        Instantiate(cBullet,transform.position,Quaternion.identity);
    }


    [SerializeField]
	private TextAsset textAsset;
    private string loadText;
    public GameObject atkLetter;
    private byte[] spawnAble = new byte[4] {0,1,1,1};
    private int count = 0;
    private IEnumerator attackB(){
        if(count>=3&&spawnAble[0]==1){
            spawnAble[0] = 0;
            count = 0;
            yield break;
        }
        spawnAble[0]=1;
        loadText = textAsset.text;
        string[] loadTextAry = loadText.Split("\n");
        int r = Random.Range(0,loadTextAry.Length -1);
        int rPos = Random.Range(1,4);
        char[] letterAry = loadTextAry[r].ToCharArray();
        count++;

        if(spawnAble[rPos]==0){
            StartCoroutine("attackB");
            yield break;
        };
        spawnAble[rPos] = 0;
        for(int i=0;i<letterAry.Length-1;i++){
            if(kouchoHealth<6)yield break;
            GameObject shot = Instantiate(atkLetter,new Vector2(15,-2*rPos),Quaternion.identity);
            shot.GetComponent<TextMeshPro>().text = letterAry[i].ToString();
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(-10.0f,0.0f);
            yield return new WaitForSeconds(1.5f);
        }
        spawnAble[rPos] = 1;
    }


    public GameObject bsBall;
    private GameObject[] bsBarAry = new GameObject[12];
    private IEnumerator attackC(){
        id = 1;
        for(int i=-5;i<5;i++){
            Vector2 spawnPos = new Vector2(3*i,3);
            bsBarAry[i+5] = Instantiate(bsBall,spawnPos,Quaternion.identity);
        }
        for(float i=0;i>-10000;i--){
            if(kouchoHealth/3!=1){
                foreach(GameObject ballObj in bsBarAry){
                    Destroy(ballObj);
                }
                spawnAble[0] = 0;
                count = 0;
                yield break;
            }
            for(int j=-5;j<5;j++){
                bsBarAry[j+5].transform.position = new Vector2((3*j+1)*Mathf.Cos(Mathf.PI*i/200),(3*j+1)*Mathf.Sin(Mathf.PI*i/500)+3);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public GameObject kochi;
    private void attackD(){
        int rD = Random.Range(0,4);
        float r = (rD==1||rD==3) ? Random.Range(-13f,13f):Random.Range(-6f,6f);
        Vector2 kVector = new Vector2(Mathf.Cos(Mathf.PI/2*rD),Mathf.Sin(Mathf.PI/2*rD));
        Vector2 kPos =(rD==0) ? new Vector2(-13,r):
                      (rD==1) ? new Vector2(r,-6):
                      (rD==2) ? new Vector2(13,r):
                                new Vector2(r,6);
        GameObject shot = Instantiate(kochi,kPos,Quaternion.Euler(0,0,rD*90));
        if(rD==2)shot.transform.localScale = new Vector3(-0.3f,-0.3f,0f);
        shot.GetComponent<Rigidbody2D>().velocity = kVector*15;
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
        kouchoHealth--;
        state = Status.hitted;
        myRender = GetComponent<SpriteRenderer>();
        for(int i=0;i<10;i++){
            yield return new WaitForSeconds(0.15f);
            myRender.material.color = myRender.material.color - new Color32(0,0,0,255);
            yield return new WaitForSeconds(0.15f);
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
