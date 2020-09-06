using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchField : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    Vector3 startPos;
    Vector3 currentPos;

    [HideInInspector]
    public Vector3 touchRotation=Vector3.zero;

    Vector3 newPos;
    Vector3 previousPos;

    public List<Touch> touches;
    public void OnDrag(PointerEventData eventData)
    {


        newPos = currentPos - startPos;
        touchRotation = new Vector3(newPos.x+previousPos.x,Mathf.Clamp(newPos.y+previousPos.y,-200,190),newPos.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(touches.Count<2)
        {
            if (touches.Count == 0)
                touches.Add(Input.GetTouch(0));
            else
                touches.Add(Input.GetTouch(1));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        previousPos = touchRotation;
    }
}
