using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToCursor : MonoBehaviour
{
    /// <summary>
    /// Raycasts from playerPosition to mouse position and outputs data to hit.
    /// 
    /// Usage: 
    /// RaycastHit hit;
    /// RaycastToCursor(transform.position, out hit);
    /// 
    /// hit now has the data from the raycasts.
    /// </summary>
    /// <param name="playerPosition">The position to start the raycast from</param>
    /// <param name="hit"> The raycast data</param>
    void RaycastToCursor(Vector3 playerPosition, out RaycastHit hit)
    {
        hit = new RaycastHit();
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.down, playerPosition.y);

        if (groundPlane.Raycast(mouseRay, out float distance))
        {
            var CursorWorldPosition = mouseRay.GetPoint(distance);
            var CursorDirection = (CursorWorldPosition - playerPosition).normalized;

            Physics.Raycast(playerPosition, CursorDirection, out hit);

        }
    }
}
