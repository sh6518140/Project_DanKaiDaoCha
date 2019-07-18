using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoPointController : MonoBehaviour,IDragHandler, IEndDragHandler,IPointerDownHandler,IPointerUpHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        VideoPlaySwitch = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Invoke("Fuc_Setfalse",0.8f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        HandleisDown = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Invoke("Fuc_Setfalse", 0.8f);
    }

    private void Start()
    {
        VideoPlaySwitch = false;
        HandleisDown = false;

    }
    public static bool VideoPlaySwitch;
    public static bool HandleisDown;


    public void Fuc_Setfalse()
    {
        HandleisDown = false;
        VideoPlaySwitch = false;
    }

}
