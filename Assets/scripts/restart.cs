using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene("StartScene");
            GameManager.level = 1;
        }
        if(Input.GetKey(KeyCode.M)&&Input.GetKey(KeyCode.N)&&Input.GetKey(KeyCode.G)&&Input.GetKey(KeyCode.Return)){
            SceneManager.LoadScene("StartScene");
        }
    }
}
