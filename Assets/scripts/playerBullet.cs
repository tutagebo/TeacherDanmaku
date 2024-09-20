using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    void Update()
    {
        Vector3 objVector = new Vector3(0f,0.2f,0f);
        transform.position += objVector;
        if(transform.position.y >8){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name=="Suika(Clone)"){
            Destroy(other.gameObject);
        }
    }
}
