using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Update()
    {
        if(Mathf.Abs(this.transform.position.y)>8||Mathf.Abs(this.transform.position.x)>18)Destroy(gameObject);
    }
}
