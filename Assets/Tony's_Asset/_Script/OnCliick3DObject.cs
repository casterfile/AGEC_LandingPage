using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCliick3DObject : MonoBehaviour
{
    public string AppName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseUp()
    {
        /*Do whatever here as per your need*/
        GameLuncher gl = new GameLuncher();
        gl.LunchNow(AppName);
        //SceneManager.LoadScene("Scene02_FaceRecognition");
    }


}
