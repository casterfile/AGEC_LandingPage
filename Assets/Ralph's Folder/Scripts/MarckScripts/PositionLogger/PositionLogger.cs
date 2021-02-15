using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLogger : MonoBehaviour
{
    public PositionLogs PositionLogs;
    public KeyCode keyCode;

    public void LogTransformPosition()
    {
        if (PositionLogs == null) return;
        PositionLogs.BoardPosition newTPos = new PositionLogs.BoardPosition();
        newTPos.Id = gameObject.name;
        newTPos.Position = transform.position;
        newTPos.Rotation = transform.rotation;
        newTPos.Scale = transform.localScale;
        PositionLogs.BoardPositions.Add(newTPos);
    }

    public void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            LogTransformPosition();
        }
    }
}
