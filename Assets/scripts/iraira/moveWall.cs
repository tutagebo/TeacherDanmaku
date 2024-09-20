using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 myPos = gameObject.transform.position;
        myPos.x = 2*Mathf.Sin(Time.time)+14;
        if(gameObject.name=="moveWall2")myPos.x = -Mathf.Sin(Time.time);
    }
}
