using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class SimpleTween : MonoBehaviour
{
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private float fadeInSpeed = 0.8f;
    [SerializeField]
    private float fadeOutSpeed = 0.2f;
    [SerializeField]
    private bool isVisible;
    [SerializeField]
    public bool FadeInOnEnable;

    public void FadeOut()
    {
        StartCoroutine(IFadeOut(targetImage, fadeOutSpeed));
    }

    public void FadeIn()
    {   
        StartCoroutine(IFadeIn(targetImage, fadeInSpeed));
    }

    IEnumerator IFadeOut(Image tImage, float speed)
    {
        for (float i = speed; i >= 0; i -= Time.deltaTime)
        {
            tImage.color = new Color(1, 1, 1, i);
            yield return null;
        }
        isVisible = false;
        gameObject.SetActive(false);
        yield break;
    }

    IEnumerator IFadeIn(Image tImage, float speed)
    {
        gameObject.SetActive(true);
        // loop over 1 second
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            tImage.color = new Color(1, 1, 1, i);
            yield return null;
        }
        isVisible = true;
        yield break;
    }

    public void FadeOutUI()
    {
        StartCoroutine(IFadeOutUI(gameObject, fadeOutSpeed));
    }

    public void FadeInUI()
    {
        StartCoroutine(IFadeInUI(gameObject, fadeInSpeed));
    }


    IEnumerator IFadeOutUI(GameObject uiElement, float speed)
    {
        for (float i = speed; i >= 0; i -= Time.deltaTime)
        {
            uiElement.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        isVisible = false;
        gameObject.SetActive(false);
        yield break;
    }

    IEnumerator IFadeInUI(GameObject uiElement, float speed)
    {
        gameObject.SetActive(true);
        // loop over 1 second
        for (float i = speed; i <= 1; i += Time.deltaTime)
        {
            uiElement.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        isVisible = true;
        yield break;
    }

    public void Start()
    {

    }

    public void OnEnable()
    {
        if (FadeInOnEnable)
        {
            FadeInUI();
        }
    }
    public void OnDisable()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        isVisible = false;
    }


}
