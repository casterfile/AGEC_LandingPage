using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    // int OIL = 1;

    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Debug.Log("adasd");

            // RaycastHit hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);

            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, 0, Input.mousePosition.y));
            Vector3 direction = worldMousePosition - Camera.main.transform.position;

            if(Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f))
            {
                if(hit.transform.CompareTag("Puzzle"))
                {
                    if(!hit.transform.GetComponent<PieceScript>().InRightPosition)
                    {
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<PieceScript>().Selected = true;
                        // SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
                        // OIL++;
                    }
                }
            }

            // if(hit.transform.CompareTag("Puzzle"))
            // {
            //     if(!hit.transform.GetComponent<PieceScript>().InRightPosition)
            //     {
            //         SelectedPiece = hit.transform.gameObject;
            //         SelectedPiece.GetComponent<PieceScript>().Selected = true;
            //         // SelectedPiece.GetComponent<SortingGroup>().sortingOrder = OIL;
            //         // OIL++;
            //     }
            // }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(SelectedPiece != null)
            {
                SelectedPiece.GetComponent<PieceScript>().Selected = false;
                SelectedPiece = null;
            }
        }

        if(SelectedPiece != null)
        {
            Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            SelectedPiece.transform.position = new Vector3(MousePoint.x, 0, MousePoint.y);
        }
    }
}
