using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPositionLogs", menuName = "PositionLogs")]
public class PositionLogs : ScriptableObject
{
    public List<BoardPosition> BoardPositions = new List<BoardPosition>();


    [System.Serializable]
    public class TransformPosition
    {
        public string Id;
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
    }

    [System.Serializable]
    public class BoardPosition : TransformPosition
    {
        public ScreenPosition ScreenBoardPosition;
    }
}
