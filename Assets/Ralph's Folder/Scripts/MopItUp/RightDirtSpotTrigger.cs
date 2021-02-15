using UnityEngine;
using System.Collections;

public class RightDirtSpotTrigger : MonoBehaviour 
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] _rightDirtSpots;

    private MopManager MopManager;
    private Color color;
    private bool rightMopTriggered = false;
    private int DirtPermanence;
    private bool correctMop;

    void Start()
    {
        MopManager = FindObjectOfType<MopManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ScaleDirt();

        spriteRenderer.sprite = _rightDirtSpots[Random.Range(0, _rightDirtSpots.Length)]; 

        color = GetComponent<SpriteRenderer>().color;

        MopManager.SpawnZone.RightLargeSpawnPoint();
        transform.localPosition = MopManager.SpawnZone.SpawnPoint;

        DirtPermanence = MopManager.MopGameConfig.DirtPermanence;
    }

	void OnTriggerEnter(Collider other)
	{
        OnSwipeDirt(other);

        if(correctMop){
            MopManager.sVisualFeedback.ShowPositiveFeedback();
            MopManager.CorrectPosSound.Play();
            MopManager.rightCorrectAction++;
        } else {
            MopManager.sVisualFeedback.ShowNegativeFeedback();
            MopManager.WrongPosSound.Play();
            MopManager.rightWrongAction++;
        }

        if(DirtPermanence == 1)
        {
            if(color.a > 0)
            {
                rightMopTriggered = true;

                GetComponent<Collider>().enabled = false;

                StartCoroutine(newRightDirtSpot());
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
        if (rightMopTriggered)
        {
            CleanRightDirt();
        }
    }

    public void OnSwipeDirt(Collider other)
    {
        if (other.gameObject.CompareTag("RightMop"))
        {
            correctMop = true;
        }
        else if(other.gameObject.CompareTag("LeftMop"))
        {
            correctMop = false;
        }
    }

    void CleanRightDirt ()
    {
        color.a -= .03f;
        GetComponent<SpriteRenderer>().color = color;
    }

    IEnumerator newRightDirtSpot()
    {
        yield return new WaitForSeconds(1);

        GetComponent<Collider>().enabled = true;

        spriteRenderer.sprite = _rightDirtSpots[Random.Range(0, _rightDirtSpots.Length)]; 

        rightMopTriggered = false;
        
        MopManager.SpawnZone.RightLargeSpawnPoint();
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
