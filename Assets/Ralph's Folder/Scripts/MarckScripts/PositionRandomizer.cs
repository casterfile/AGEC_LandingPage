using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRandomizer : MonoBehaviour
{
    public Transform Parent;
    public bool IsRestrictedToParentPos;
    public bool RandomizeOnStart;

    [Space(7)]
    [Header("Min and Max Range")]
    public Vector2 XRange = new Vector2(-0.5f , 0.5f); //x : minimmun y : maximum
    public Vector2 YRange = new Vector2(-0.5f, 0.5f); //x : minimmun y : maximum
    public Vector2 ZRange = new Vector2(-0.5f, 0.5f); //x : minimmun y : maximum

    [Space(7)]
    [Header("Offset from Boundaries")]
    public Vector2 XBoundaryOffset = new Vector2(0.1f,0.1f);
    public Vector2 YBoundaryOffset = new Vector2(0.1f, 0.1f);
    public Vector2 ZBoundaryOffset = new Vector2(0.1f, 0.1f);

    [Space(7)]
    [Header("Static Positions")]
    public bool IsXAxisFixed;
    public float FixedXPos;
    public bool IsYAxisFixed;
    public float FixedYPos;
    public bool IsZAxisFixed;
    public float FixedZPos;

    private Transform targetTransform;

    private Vector3 basePos;

    public void Awake()
    {
        targetTransform = GetComponent<Transform>();
        Parent = GetComponent<Transform>().parent;
    }

    public void Start()
    {
        basePos = transform.position;
       
        if (IsRestrictedToParentPos)
        {
            XRange = new Vector2(-0.5f - XBoundaryOffset.x, 0.5f - XBoundaryOffset.y);
            YRange = new Vector2(-0.5f - YBoundaryOffset.x, 0.5f - YBoundaryOffset.y);
            ZRange = new Vector2(-0.5f - ZBoundaryOffset.x, 0.5f - ZBoundaryOffset.y);
        }


    }

    public Vector3 RandomizePosition(Transform targetObject)
    {
        basePos = targetObject.transform.localPosition;
        //Vector3 randPos = new Vector3(RandomizeXPos(), RandomizeYPos(), RandomizeZPos());
        Vector3 randPos = new Vector3(RandomizePos(basePos.x, XRange.x , XRange.y,  IsXAxisFixed), RandomizePos(basePos.y, YRange.x, YRange.y, IsYAxisFixed), RandomizePos(basePos.z, ZRange.x, ZRange.y, IsZAxisFixed));
        targetObject.localPosition = randPos;
        return randPos;
    }

    public void Update()
    {
    }

    public float RandomizePos(float basePosition, float minRange, float maxRange, bool valFixed)
    {
        float val = 0.5f;

        if (valFixed)
        {
            val = basePosition;
            return val;
        }
        else
        {
            val = Random.Range(minRange, maxRange);
            return val;
        }
    }
    /*
    public float RandomizeXPos()
    {
        float val = 0.5f;

        if (IsXAxisFixed)
        {
            val = basePos.x;
            FixedXPos = val;
            return val;
        }
        else
        {
            val = Random.Range(XRange.x, XRange.y);
            FixedXPos = 0;
            return val;
        }
        
    }
    public float RandomizeYPos()
    {
        float val = 0.5f;

        if (IsYAxisFixed)
        {
            val = basePos.y;
            FixedYPos = val;
            return val;
        }
        else
        {
            val = Random.Range(YRange.x, YRange.y);
            FixedYPos = 0;
            return val;
        }
    }
    public float RandomizeZPos()
    {
        float val = 0.5f;

        if (IsZAxisFixed)
        {
            val = basePos.z;
            FixedZPos = val;
            return val;
        }
        else
        {
            val = Random.Range(ZRange.x, ZRange.y);
            FixedZPos = 0;
            return val;
        }
    }
    */
}
