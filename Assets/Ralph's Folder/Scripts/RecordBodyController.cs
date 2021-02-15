using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordBodyController : MonoBehaviour
{
    private MopManager MopManager;
    private KinectRecorderPlayer saverPlayer;
    public Text RecordInfo;

    void Start()
	{
		saverPlayer = KinectRecorderPlayer.Instance;
        MopManager = FindObjectOfType<MopManager>();
	}

    public void RecordBody()
    {
        RecordInfo.text = ("Recording.....");
        if(saverPlayer)
        {
            saverPlayer.StartRecording();
        }
    }

    public void StopRecord()
    {
        saverPlayer.StopRecordingOrPlaying();
    }

    public void PlayRecordedBody()
    {
        RecordInfo.text = ("Playing.....");
        if(saverPlayer)
        {
            if(!saverPlayer.IsPlaying())
            {
                saverPlayer.StartPlaying();
            }
            else
            {
                saverPlayer.StopRecordingOrPlaying();
            }
        }
    }
}
