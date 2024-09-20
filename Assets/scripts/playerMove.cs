using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    public GameObject Bullet;
    private Rigidbody2D myRigidbody;
    public float playerSpeed = 10;
    public GameObject collision;
    private SpriteRenderer collRenderer;
    // Update is called once per frame
    void Start()
    {
        myRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        collRenderer = collision.GetComponent<SpriteRenderer>();
        collRenderer.material.color = new Color32(255,255,255,0);
    }
    void Update()
    {

        collision.transform.position = this.gameObject.transform.position;

        Vector2 force = Vector2.zero;
        
        if(Input.GetKey(KeyCode.A)&&this.transform.position.x>-12.75){
            force = new Vector2(playerSpeed*-1,0);
        }
        if(Input.GetKey(KeyCode.D)&&this.transform.position.x<12.75){
            force = new Vector2(playerSpeed*1,0);
        }
        if(Input.GetKey(KeyCode.W)&&this.transform.position.y<6.5){
            force = new Vector2(0,playerSpeed*1);
        }
        if(Input.GetKey(KeyCode.S)&&this.transform.position.y>-6.5){
            force = new Vector2(0,playerSpeed*-1);
        }
        if(Input.GetKeyDown(KeyCode.K)){
            GameObject shot = Instantiate(Bullet,transform.position,Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,1.0f).normalized*5;
        }
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)){
            collRenderer.material.color = new Color32(255,255,255,128);
        }else{
            collRenderer.material.color = new Color32(255,255,255,0);
        }
        myRigidbody.velocity = force;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("enemy")||other.gameObject.CompareTag("paint")){
            SceneManager.LoadScene("GOverScene");
            gameObject.SetActive(false);
        }
    }
}
