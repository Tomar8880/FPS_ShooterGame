using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Vector3 startPos;
    Vector3 currentPos;

    public TouchField touchField;
    public int handleMaxPos=150;
    public Transform handle;
    public GameObject sprint;

    [HideInInspector]
    public float x,y;

    [HideInInspector]
    public bool isSprinting;
    public void OnDrag(PointerEventData eventData)
    {
        currentPos = Input.GetTouch(touchCount).position;
        handle.localPosition =currentPos-startPos;

        if (handle.localPosition.y>=handleMaxPos && handle.localPosition.x>-40 && handle.localPosition.x<60)
        {
            isSprinting = true;
            sprint.SetActive(true);
        }
        else
        {
            isSprinting = false;
            sprint.SetActive(false);
        }
    }

    int touchCount;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (touchField.touches.Count < 2)
        {
            if (touchField.touches.Count == 0)
                touchField.touches.Add(Input.GetTouch(0));
            else
                touchField.touches.Add(Input.GetTouch(1));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isSprinting)
            handle.localPosition = Vector3.zero;
        else
            handle.localPosition = new Vector3(0, handleMaxPos, 0);
       
        touchCount = 0;
    }

    void Update()
    {
        handle.localPosition = new Vector3(Mathf.Clamp(handle.localPosition.x, -handleMaxPos, handleMaxPos), 
            Mathf.Clamp(handle.localPosition.y, -handleMaxPos, handleMaxPos), 0);
    }

    private void FixedUpdate()
    {
        Vector3 axis = handle.localPosition.normalized;
        x = axis.x;
        y = axis.y;
    }





}
