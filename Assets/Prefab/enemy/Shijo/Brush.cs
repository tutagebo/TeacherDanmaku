using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    // Start is called before the first frame update
    Color32 myColor = new Color32(0,0,0,0);
    void Start(){
        Vector2 direction = - gameObject.transform.position ;
        gameObject.GetComponent<Rigidbody2D>().velocity = direction.normalized*10;
        myColor = (gameObject.name=="brCy(Clone)") ? new Color32(0,255,255,255): //Cy
                  (gameObject.name=="brMg(Clone)") ? new Color32(255,0,255,255): //Mg
                                                     new Color32(255,255,0,255); //Ye
    }

    // Update is called once per frame
    private float currentTime =0;
    SpriteRenderer paintRender;
    void Update()
    {
        if(Mathf.Abs(this.transform.position.y)>12||Mathf.Abs(this.transform.position.x)>22)Destroy(gameObject);

        currentTime += Time.deltaTime;
        if(currentTime > 0.1f){
            GameObject paintObj = (GameObject)Resources.Load("paint");
            GameObject paintPut = Instantiate(paintObj,transform.position,Quaternion.identity);
            paintRender = paintPut.GetComponent<SpriteRenderer>();
            paintRender.material.color = myColor;
            currentTime = 0f;
        }
    }
    void OnDestroy()
    {
        GameObject[] paintAry = GameObject.FindGameObjectsWithTag("paint");
        foreach(GameObject paintObj in paintAry){
            paintObj.AddComponent<Paint>().shot();
        }
    }
}