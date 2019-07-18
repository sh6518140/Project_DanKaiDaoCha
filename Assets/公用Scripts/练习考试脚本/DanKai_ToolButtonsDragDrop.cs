using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DanKai_ToolButtonsDragDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ToolsTip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolsTip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolsTip.SetActive(false);
    }

}
