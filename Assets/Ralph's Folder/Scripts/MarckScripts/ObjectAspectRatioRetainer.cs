using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAspectRatioRetainer : MonoBehaviour
{
    private Transform parent;
    public void Start()
    {
        parent = transform.parent;
        Debug.Log("Local Scale : " + transform.localScale);
        Debug.Log("Global Scale : " + transform.lossyScale);
    }

    public void RetainRelativeToParent()
    {
        if (parent == null) return;
       
    }
}
