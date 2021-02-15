using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraWebCam : MonoBehaviour
{
    public RawImage m_RawImage;
    private string orbecCamera = "";
    private int cameraIndex = 0;
    static WebCamTexture backCam;
    public static bool cameraCheck = false;
    WebCamTexture webcamTexture;
    // Start is called before the first frame update
    void Start()
    {

        /*
        if(backCam == null)
        {
            backCam = new WebCamTexture();
        }

        GetComponent<Renderer>().material.mainTexture = backCam;
        if (!backCam.isPlaying)
        {
            backCam.Play();
        }

        */
        /*
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("CameraCount: "+devices[i].name);
            if ("USB2.0 HD UVC WebCam" == devices[i].name)
            {
                cameraIndex = i;
                orbecCamera = devices[i].name;


                
            }

            
        }
        */

        /*
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();

        if (devices.Length > 0)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                Debug.Log("CameraCount: " + devices[i].name);
                if ("USB2.0 HD UVC WebCam" == devices[i].name)
                {
                    cameraIndex = i;
                    orbecCamera = devices[i].name;
                    webcamTexture.deviceName = devices[i].name;
                    Debug.Log("CameraCount 222: " + webcamTexture.deviceName);
                    webcamTexture.Play();

                }


            }
        }*/

        WebCamDevice[] devices = WebCamTexture.devices;
        webcamTexture = new WebCamTexture();
        //m_RawImage.texture = webcamTexture;
        //m_RawImage.material.mainTexture = webcamTexture;
        //webcamTexture.Play();

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log("CameraCount: " + devices[0].name);
            //if ("USB Camera" == devices[i].name)
            if (devices[0].name == devices[i].name)//"USB Camera"
            {
                
                cameraIndex = i;
                orbecCamera = devices[i].name;
                webcamTexture.deviceName = devices[i].name;
                Debug.Log("CameraCount: " + devices[i].name);
                m_RawImage.texture = webcamTexture;
                m_RawImage.material.mainTexture = webcamTexture;
                

            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        if (Global_GamePause.isPause == false)
        {
            //All process needs to be inside the Pause Arguments 
            webcamTexture.Play();
        }
        else
        {
            webcamTexture.Stop();
        }

    }
}
