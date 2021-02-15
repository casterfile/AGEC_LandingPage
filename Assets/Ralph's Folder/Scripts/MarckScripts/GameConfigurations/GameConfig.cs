using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameInput
{
    KeyboardMouse,
    MotionCamera
}

public class GameConfig : ScriptableObject
{
    [Space(7)]
    public GameInput GameInput;
    [Header("Default Config")]
    public float TimeLimit;
    public float MaxScore;
}
