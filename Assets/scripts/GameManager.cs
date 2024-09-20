using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int level = 1;
    // Start is called before the first frame update
    void Start()
    {
    }
    

    string[,] mainSceneAry = new string[,] {
        {"Aoki","Nishi","Shino"},
        {"Saito","Uchi","Shijo"},
        {"Matu","Ogi","Kuri"},
        {"Koucho","Koucho","Koucho"}
    };
    // Update is called once per frame
    private bool con = true;
    void Update()
    {
        if(!GameObject.FindGameObjectWithTag("enemy")&&con){
            con = false;
            StartCoroutine("stageChange");
        }
        
        /*  //debug//
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene (SceneManager.GetActiveScene().name);
        }
        */
        if(Input.GetKey(KeyCode.M)&&Input.GetKey(KeyCode.N)&&Input.GetKey(KeyCode.G)&&Input.GetKey(KeyCode.Return)){
            SceneManager.LoadScene("StartScene");
        }
    }
    private IEnumerator stageChange(){
        yield return new WaitForSeconds(1.0f);
        int r = Random.Range(0,3);
        Debug.Log(r);
        SceneManager.LoadScene(mainSceneAry[level,r]+"Scene");
        level++;
    }
}
