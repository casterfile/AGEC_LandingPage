using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenVisualFeedback : MonoBehaviour
{
    public Image PositiveImg;
    public Image NegativeImg;

    public void ShowPositiveFeedback()
    {
        if (PositiveImg.GetComponent<SimpleTween>() == null) 
        {
            Debug.Log("ScreenVisualFeedback Positive Feedback does not have a SimpleTween component!");
        }
        else if (PositiveImg.GetComponent<SimpleTween>() != null)
        {
            PositiveImg.gameObject.SetActive(true);
            PositiveImg.GetComponent<CanvasGroup>().alpha = 1;
            PositiveImg.GetComponent<SimpleTween>().FadeOutUI();
        }
    } 
    
    public void ShowNegativeFeedback()
    {
        if (PositiveImg.GetComponent<SimpleTween>() == null)
        {
            Debug.Log("ScreenVisualFeedback Positive Feedback does not have a SimpleTween component!");
        }
        else if (PositiveImg.GetComponent<SimpleTween>() != null)
        {
            NegativeImg.gameObject.SetActive(true);
            PositiveImg.GetComponent<CanvasGroup>().alpha = 1;
            NegativeImg.GetComponent<SimpleTween>().FadeOutUI();
        }
    }
}
