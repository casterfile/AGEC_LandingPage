using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpriteChanger : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private PuzzleMenu PuzzleMenu;

    void Start()
    {
        PuzzleMenu = FindObjectOfType<PuzzleMenu>();

        SpriteRenderer = GetComponent<SpriteRenderer>();

        SpriteRenderer.sprite = PuzzleMenu.SelectedPuzzleBackground;
    }
}
