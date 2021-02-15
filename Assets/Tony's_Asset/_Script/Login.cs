using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Login : MonoBehaviour
{
    public InputField username, password;
    public Animator anim, camera , WebCamera, SidePanel, charaterAnim;
    // Start is called before the first frame update
    void Start()
    {


        Text goOutput = GameObject.Find("OUTPUT").gameObject.GetComponent<Text>();
        HandleTextFile fileRead = new HandleTextFile();

        fileRead.SetFileVariable("GameLibrary", "1");
        fileRead.SetFileVariable("faceDetect", "0");
        fileRead.SetFileVariable("GameName", "None");
        //goOutput.text = ""+fileRead.ReturnRead("GameLibrary");


    }

    // Update is called once per frame
    public void submit()
    {
        print("Login successful");
        anim.SetBool("isLoginSeccesful", true);
        camera.SetBool("goinside", true);
        StartCoroutine("WebCameraStat", 1.0f);

        if (username.text == "pass" && password.text == "pass")
        {
            

        }
        else
        {
            print("Login Failed");
        }
    }

    IEnumerator WebCameraStat(float delay)
    {
        yield return new WaitForSeconds(delay); 
        WebCamera.SetBool("cameraStart", true);
        SidePanel.SetBool("SidePanelStart", true);
        
        //This is the code where the calibration will start
    }

    //execute after after calibration
    public void ChooseAGame()
    {
        camera.SetBool("GameStart", true);
        SidePanel.SetBool("SidePanelOff", true);
        charaterAnim.GetComponent<Animator>().enabled = true;
        charaterAnim.SetTrigger("bowTrigger");
    }


    public void FaceDetect()
    {
        SceneManager.LoadScene("Scene02_FaceRecognition", LoadSceneMode.Single);
    }

    private void Update()
    {
        
    }
}
