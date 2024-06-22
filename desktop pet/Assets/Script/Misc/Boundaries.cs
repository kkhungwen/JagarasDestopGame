using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(EdgeCollider2D))]
[DisallowMultipleComponent]
public class Boundaries : MonoBehaviour
{
    public EdgeCollider2D boundsCollider;

    private Vector2 workingSpaceLowerBoundPosition;
    private Vector2 workingSpaceUpperBoundPosition;

    private void Awake()
    {
        System.Drawing.Point screenPoint = new System.Drawing.Point(0, 0);
        System.Drawing.Rectangle workingRectangle = System.Windows.Forms.Screen.GetWorkingArea(screenPoint);

        workingSpaceLowerBoundPosition = WindowFormsDllUtils.GetWorkingSpaceLowerBoundPosition();
        workingSpaceUpperBoundPosition = WindowFormsDllUtils.GetWorkingSpaceUpperBoundPosition();

        boundsCollider = GetComponent<EdgeCollider2D>();
        boundsCollider.points = new Vector2[5] { workingSpaceLowerBoundPosition, new Vector2(workingSpaceUpperBoundPosition.x, workingSpaceLowerBoundPosition.y), workingSpaceUpperBoundPosition,
            new Vector2(workingSpaceLowerBoundPosition.x, workingSpaceUpperBoundPosition.y), workingSpaceLowerBoundPosition };
    }
}
