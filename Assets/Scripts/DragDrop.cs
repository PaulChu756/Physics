using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offSet;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offSet = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        Debug.Log("do something");
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offSet;
        transform.position = currentPosition;
        Debug.Log("Blah");
    }
}
