using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingProcess : MonoBehaviour
{
    public Transform prefab;
    private Transform cl;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePauseScriptTest.isPause == false)
        {
            Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
