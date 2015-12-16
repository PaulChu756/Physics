using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private void OnMouseDown()
    {
        //translate the particle position from the world to Screen Point
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //calculate any difference between the particle world position and the mouses Screen position converted to a world point  
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        //keep track of the mouse position
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        //convert the screen mouse position to world point and adjust with offset
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        //update the position of the object in the world
        transform.position = currentPosition;
    }
}
