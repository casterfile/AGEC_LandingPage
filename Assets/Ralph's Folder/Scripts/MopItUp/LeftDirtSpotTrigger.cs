using UnityEngine;
using System.Collections;

public class LeftDirtSpotTrigger : MonoBehaviour 
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] _leftDirtSpots;

    private MopManager MopManager;
    private Color color;
    private bool leftMopTriggered = false;
    private int DirtPermanence;
    private bool correctMop;

    void Start()
    {
        MopManager = FindObjectOfType<MopManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ScaleDirt();

        spriteRenderer.sprite = _leftDirtSpots[Random.Range(0, _leftDirtSpots.Length)]; 

        color = GetComponent<SpriteRenderer>().color;

        MopManager.SpawnZone.LeftLargeSpawnPoint();
        transform.localPosition = MopManager.SpawnZone.SpawnPoint;

        DirtPermanence = MopManager.MopGameConfig.DirtPermanence;
    }

	void OnTriggerEnter(Collider other)
	{
        OnSwipeDirt(other);

        if(correctMop){
            MopManager.sVisualFeedback.ShowPositiveFeedback();
            MopManager.CorrectPosSound.Play();
            MopManager.leftCorrectAction++;
        } else {
            MopManager.sVisualFeedback.ShowNegativeFeedback();
            MopManager.WrongPosSound.Play();
            MopManager.leftWrongAction++;
        }

        if(DirtPermanence == 1)
        {
            if(color.a > 0)
            {
                leftMopTriggered = true;

                GetComponent<Collider>().enabled = false;

                StartCoroutine(newLeftDirtSpot());
            }
        }

        switch(MopManager.MopGameConfig.DirtPermanence)
        {
            case 2:
                DirtPermanence--;
                color.a -= .3f;
                GetComponent<SpriteRenderer>().color = color;
                break;
            case 3:
                DirtPermanence--;
                color.a -= .3f;
                GetComponent<SpriteRenderer>().color = color;
                break;
        }
	}

    void Update () 
    {   
        if (leftMopTriggered)
        {
            CleanLeftDirt();
        }
    }

    public void OnSwipeDirt(Collider other)
    {
        if (other.gameObject.CompareTag("LeftMop"))
        {
            correctMop = true;
        }
        else if(other.gameObject.CompareTag("RightMop"))
        {
            correctMop = false;
        }
    }

    void CleanLeftDirt ()
    {
        color.a -= .03f;
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator newLeftDirtSpot()
    {
        yield return new WaitForSeconds(1);

        GetComponent<Collider>().enabled = true;

        spriteRenderer.sprite = _leftDirtSpots[Random.Range(0, _leftDirtSpots.Length)]; 

        leftMopTriggered = false;
        
        MopManager.SpawnZone.LeftLargeSpawnPoint();
        transform.position = MopManager.SpawnZone.SpawnPoint;

        DirtPermanence = MopManager.MopGameConfig.DirtPermanence;

        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
    }

    public void ScaleDirt()
    {
        switch(MopManager.MopGameConfig.DirTriggerSize)
        {
            case AreaSize.Small:
                transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                break;
            case AreaSize.Medium:
                transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
                break;
            case AreaSize.Large:
                transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                break;
        }
    }
}
