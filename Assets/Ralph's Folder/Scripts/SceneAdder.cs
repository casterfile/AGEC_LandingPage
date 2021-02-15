using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Game
{
    JigsawPuzzle,
    MopItUp
}

public class SceneAdder : MonoBehaviour
{
    public GameObject Menu;
    private TutorialManager TutorialManager;

    public Game Game;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadTutorial());
    }

    void Update()
    {
        if(TutorialManager)
        {
            if(TutorialManager.tutorialDone)
            {
                SceneManager.UnloadSceneAsync("Tutorials", UnloadSceneOptions.None);
                Menu.SetActive(true);
            }
        }
        
    }
    
    IEnumerator LoadTutorial()
    {
        AsyncOperation asyncLoadScene = SceneManager.LoadSceneAsync("Tutorials", LoadSceneMode.Additive);
		while (!asyncLoadScene.isDone)
		{
			yield return new WaitForSeconds(0.05f);
		}

        TutorialManager = FindObjectOfType<TutorialManager>();
        
        TutorialManager.StartTutorial();
    }
}
