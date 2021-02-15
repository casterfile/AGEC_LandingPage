using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenericTimerUI : MonoBehaviour
{
    public GenericTimer GenericTimer;
    [SerializeField]
    private string label;
    private TMP_Text TimerText;

    public void Start()
    {
        TimerText = GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {
        if(GenericTimer != null && TimerText != null)
        {
            if (GenericTimer.GetTimerType() == Timer.Countdown)
            {
                int intTime = Mathf.RoundToInt(GenericTimer.TimeRemaining);
                TimerText.text = label + intTime.ToString();
            }
            else if (GenericTimer.GetTimerType() == Timer.Stopwatch)
            {
                int intTime = Mathf.RoundToInt(GenericTimer.TimeElapsed);
                TimerText.text = label + intTime.ToString();
            }
        }
    }

}
