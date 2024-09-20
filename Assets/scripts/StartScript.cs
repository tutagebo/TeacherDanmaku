using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame

    string[] mainSceneAry = new string[] {"Aoki","Nishi","Shino"};
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            int r = Random.Range(0,3);
            SceneManager.LoadScene(mainSceneAry[r]+"Scene");
        }
        if(Input.GetKey(KeyCode.M)&&Input.GetKey(KeyCode.N)&&Input.GetKey(KeyCode.G)&&Input.GetKey(KeyCode.Return)){
            SceneManager.LoadScene("StartScene");
        }
    }
}
