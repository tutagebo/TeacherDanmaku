using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaguchi : MonoBehaviour
{
    public GameObject Water;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private float currentTime = 0f;
    private int counter = 0;
    void Update()
    {
        if(counter>30){
            gameObject.SetActive(false);
            counter = 0;
            return;
        }
        currentTime += Time.deltaTime;
        if(currentTime > 0.4f){
            GameObject shot = Instantiate(Water,transform.position,Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0,-1,0).normalized*10;
            currentTime = 0f;
            counter++;
        }
    }
}
