using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MopMover : MonoBehaviour
{
    public bool movableObjectGrabbed = false;

    Ray moRay;
    public Transform moTransform;
    public LayerMask whatIsMovableObject;
    RaycastHit moHit;

    public LayerMask whatIsGround;
    public Transform ground;
    RaycastHit groundHit;
    RaycastHit mousePosHit;
    public float mousePosYOffsetFromGround = 0f;
    public Vector3 mouseRelToGround;

    // Start is called before the first frame update
    void Start()
    {
        moHit = new RaycastHit();
        groundHit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {
        moRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0))
        {
            FindAndGrabMovableObject();
        }
        if(Input.GetMouseButtonUp(0))
        {
            DropMovableObject();
        }

        movableObjectGrabbed = moTransform != null;

        if(movableObjectGrabbed)
        {
            TraceMousePosRelativeToGround();
        }
    }

    void FindAndGrabMovableObject()
    {
        if(Physics.Raycast(
            moRay,
            out moHit,
            Mathf.Infinity,
            whatIsMovableObject))
        {
            moTransform = moHit.transform;
            FindGroundBelowMovableObject();
        }
    }

    void FindGroundBelowMovableObject()
    {
        if(Physics.Raycast(
            moTransform.position,
            Vector3.down,
            out groundHit,
            Mathf.Infinity,
            whatIsGround))
        {
            ground = groundHit.transform;
        }
    }
    void TraceMousePosRelativeToGround()
    {
        if(Physics.Raycast(
            moRay,
            out mousePosHit,
            Mathf.Infinity,
            whatIsGround))
        {
            mouseRelToGround = mousePosHit.point;

            moTransform.position = new Vector3(
                    mouseRelToGround.x,
                    mouseRelToGround.y,
                    mouseRelToGround.z);
        }
    }

    void DropMovableObject()
    {
        if(moTransform != null)
            moTransform.GetComponent<Rigidbody>().isKinematic = false;
        moTransform = null;
        ground = null;
    }
}
