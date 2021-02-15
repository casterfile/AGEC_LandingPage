using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopManager : MonoBehaviour
{
    public SpawnZone SpawnZone;
    public MopGameConfig MopGameConfig;
    public ScreenVisualFeedback sVisualFeedback;
    public MopMenu MopMenu;
    public AudioSource CorrectPosSound;
	public AudioSource WrongPosSound;
    public int rightWrongAction;
    public int rightCorrectAction;
    public int leftWrongAction;
    public int leftCorrectAction;
    

    [SerializeField]
    private GenericTimer GameTimer;
    private RecordBodyController RecordBodyController;

    private void Awake()
    {
        GameTimer.SetTimer(Timer.Countdown, MopGameConfig.TimeLimit);
    }

    void Start()
    {
        SpawnZone = FindObjectOfType<SpawnZone>();
        RecordBodyController = FindObjectOfType<RecordBodyController>();

        if (GameTimer != null)
        {
            StartCoroutine(IStartTimer(0.5f));
        }
    }

    void Update()
    {
        if(GameTimer.GetTimerStatus())
        {
            OnTimerComplete();
        }
    }

    public IEnumerator IStartTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameTimer.SetTimer(Timer.Countdown, MopGameConfig.TimeLimit);
        GameTimer.RestartTimer();
        yield break;
    }

    public void OnTimerComplete()
    {
        if(RecordBodyController)
        {
            RecordBodyController.StopRecord();
        }

        GameTimer.StopTimer();
        MopMenu.GameOverPanel.SetActive(true);

        MopMenu.rightFootCorrectText.text = ("Right Foot Correct Actions: " + rightCorrectAction.ToString());
        MopMenu.rightFootWrongText.text = ("Right Foot Wrong Actions: " + rightWrongAction.ToString());

        MopMenu.leftFootCorrectText.text = ("Left Foot Correct Actions: " + leftCorrectAction.ToString());
        MopMenu.leftFootWrongText.text = ("Left Foot Wrong Actions: " + leftWrongAction.ToString());

        MopMenu.UI.SetActive(false);
    }
}
