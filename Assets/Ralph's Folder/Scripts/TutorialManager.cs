using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject HandClickGesture;
    public GameObject GrabGesture;
    public GameObject SwipeGesture;
    public GameObject FootGesture;

    public bool tutorialDone = false;

    private SceneAdder SceneAdder;

    void Start()
    {
        SceneAdder = FindObjectOfType<SceneAdder>();
    }

    public void SkipTutorial()
    {
        tutorialDone = true;
    }

    IEnumerator StopTutorial()
    {
        yield return new WaitForSeconds(10);
        tutorialDone = true;
    }

    public void StartTutorial()
    {
        
        switch(SceneAdder.Game)
        {
            case Game.MopItUp:
                StartCoroutine(LoadMopTutorial());
                break;
            case Game.JigsawPuzzle:
                StartCoroutine(LoadJigsawTutorial());
                break;
        }
    }

    //MopItUp Tutorial
    IEnumerator LoadMopTutorial()
    {
        yield return new WaitForSeconds(0);

        HandClickGesture.SetActive(true);
        StartCoroutine(LoadFootGesture());
    }

    IEnumerator LoadFootGesture()
    {
        yield return new WaitForSeconds(10);
        HandClickGesture.SetActive(false);
        FootGesture.SetActive(true);
        StartCoroutine(StopTutorial());
    }


    //Jigsaw Tutorial
    IEnumerator LoadJigsawTutorial()
    {
        yield return new WaitForSeconds(0);

        HandClickGesture.SetActive(true);
        StartCoroutine(LoadGrabGesture());
    }

    IEnumerator LoadGrabGesture()
    {
        yield return new WaitForSeconds(10);
        HandClickGesture.SetActive(false);
        GrabGesture.SetActive(true);
        StartCoroutine(LoadSwipeGesture());
    }

    IEnumerator LoadSwipeGesture()
    {
        yield return new WaitForSeconds(10);
        GrabGesture.SetActive(false);
        SwipeGesture.SetActive(true);
        StartCoroutine(StopTutorial());
    }
}
