using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Timer
{
    Countdown,
    Stopwatch
}

public class GenericTimer : MonoBehaviour
{
    [SerializeField]
    private Timer Type;
    [SerializeField]
    private float duration;
    [SerializeField]
    private bool isFinished;

    public float TimeElapsed;
    public float TimeRemaining;
    [SerializeField]
    private bool isInProgress;
    [SerializeField]
    private bool isAutoResetOnFinish;

    public void Start()
    {
        
    }

    public void SetTimer(Timer type, float Duration, bool autoResetOnFinish = false)
    {
        Type = type;
        duration = Duration;
        isAutoResetOnFinish = autoResetOnFinish;
    }

    public bool GetTimerStatus()
    {
        bool iF = isFinished;
        return iF;
    }

    public void SetTimerDuration(float d)
    {
        duration = d;
    }

    public void SetTimerStatus(bool s)
    {
        isFinished = s;
    }

    public void SetTimerType(Timer type)
    {
        Type = type;
    }
    
    public Timer GetTimerType()
    {
        Timer t = Type;
        return t;
    }

    private IEnumerator IStartCountdown(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        isFinished = true;
        yield break;
    }

    public void ResetTimer()
    {
        isInProgress = false;
        isFinished = false;
        TimeRemaining = duration;
        TimeElapsed = 0;
    }

    public void RestartTimer()
    {
        isFinished = false;
        TimeRemaining = duration;
        TimeElapsed = 0;
        isInProgress = true;
    }

    public void StartTimer()
    {
        isInProgress = true;
    } 
    
    public void PauseTimer()
    {
        isInProgress = false;
    }

    public void StopTimer()
    {
        isInProgress = false;
        isFinished = true;
    }

    public void Update()
    {
        if (Type == Timer.Countdown && duration > 0)
        {
            if (isInProgress)
            {
                TimeRemaining -= Time.deltaTime;
                TimeElapsed += Time.deltaTime;
                if (TimeElapsed >= duration)
                {
                    isInProgress = false;
                    isFinished = true;
                    if (isAutoResetOnFinish) { ResetTimer(); }
                }
            }
            /*
            if (Input.GetKeyDown(KeyCode.G))
            {
                RestartTimer();
            }
            */
        }
        else if(Type == Timer.Stopwatch)
        {
            if (isInProgress)
            {
                TimeElapsed += Time.deltaTime;
            }

            if(TimeElapsed == 9999)
            {
                StopTimer();
            }
            /*
            if (Input.GetKeyDown(KeyCode.G))
            {
                RestartTimer();
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                StopTimer();
            }
            */
        }
    }




}
