using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextScroller : MonoBehaviour
{
    public TMP_Text Label;
    public Button scrollLeftBtn;
    public Button scrollRightBtn;
    public bool loopOnEnd;

    public List<Button> Buttons = new List<Button>();

    [Space(7)]
    [Header("Intergral Values")]
    public bool IncrementalScroller = false;
    public int StartValue = 60;
    public int MaxValue = 999;
    public int MinValue = 0;
    public int StepValue = 1;
    [SerializeField]
    private int currentIntegralValue;

    [Space(7)]
    [Header("String Values")]
    public List<string> Entrees = new List<string>();

    [SerializeField]
    private int currentEntryIndex;

    private string lastLabelContent;

    public void Start()
    {

        if(Entrees.Count > 0 && !IncrementalScroller)
        {
            Label.text = Entrees[0];
        }
        else
        {
            Label.text = "N/A";
        }

        if (IncrementalScroller)
        {
            Label.text = StartValue.ToString();
            ResetIntegralValue();
        }
    }

    public void ScrollRight()
    {
        if(Entrees.Count > 0)
        {
            if(currentEntryIndex < Entrees.Count - 1)
            {
                currentEntryIndex++;
                UpdateScrollLabel(currentEntryIndex);
            }
            else if(currentEntryIndex == Entrees.Count - 1 && loopOnEnd)
            {
                currentEntryIndex = 0;
                UpdateScrollLabel(currentEntryIndex);
            }
        }
    } 
    
    public void ScrollLeft()
    {
        if(Entrees.Count > 0)
        {
            if(currentEntryIndex > 0)
            {
                currentEntryIndex--;
                UpdateScrollLabel(currentEntryIndex);
            }
            else if (currentEntryIndex == 0 && loopOnEnd)
            {
                currentEntryIndex = Entrees.Count - 1;
                UpdateScrollLabel(currentEntryIndex);
            }
        }
    }

    public void Increment()
    {
        if (currentIntegralValue < MaxValue)
        {
            currentIntegralValue += StepValue;
        }
        else if(currentIntegralValue >= MaxValue)
        {
            currentIntegralValue = MaxValue;
        }
        UpdateIntegerLabel(currentIntegralValue);
    }
     public void Decrement()
    {
        if (currentIntegralValue > MinValue)
        {
            currentIntegralValue -= StepValue;
        }
        else if(currentIntegralValue <= 0)
        {
            currentIntegralValue = MinValue;
        }
        UpdateIntegerLabel(currentIntegralValue);
    }

    public void ResetIntegralValue()
    {
        currentIntegralValue = StartValue;
    }

    public void SetIntegralValues(int min, int max, int value)
    {
        MinValue = min;
        MaxValue = max;
        currentIntegralValue = value;
        UpdateIntegerLabel(value);
    }

    public int GetCurrentIntegralValue()
    {
        int value;
        value = currentIntegralValue;
        return value;
    }

    public void UpdateScrollLabel(int value)
    {
        if(Label == null) { Debug.Log("Scroll Label not set in Inspector!"); return; }
        currentEntryIndex = value;
        Label.text = Entrees[value];
    }

    public void UpdateIntegerLabel(int value)
    {
        if(value > MaxValue)
        {
            value = MaxValue;
        }
        else if (value < MinValue)
        {
            value = MinValue;
        }
        Label.text = value.ToString();
    }

    public int GetCurrentIndex()
    {
        int index;

        index = currentEntryIndex;

        return index;
    }

    public void DisableButtons()
    {
        foreach(Button b in Buttons)
        {
            b.gameObject.SetActive(false);
        }
    }
    
    public void EnableButtons()
    {
        foreach(Button b in Buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void ToggleButtons(bool enabled)
    {
        if (enabled)
        {
            EnableButtons();
        }
        else if(!enabled)
        {
            DisableButtons();
        }
    }
}
