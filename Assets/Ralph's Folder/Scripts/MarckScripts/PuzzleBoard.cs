using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    public string BoardName;
    public Transform PileContainer;
    public Transform DefPileContainer;
    public Transform AltPileContainer;
    public List<Transform> PuzzlePieces = new List<Transform>(); 

    public void GetPuzzlePieces()
    {

    }
}
