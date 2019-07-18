
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleBtn : MonoBehaviour {
    
    class MyRectTransform
    {
        public Vector3 localPosition = Vector3.zero;
        public Vector2 sizeDelta = Vector2.zero;
    }

    Canvas canvas;
    
    List<RectTransform> btnRects;
    List<MyRectTransform> initRects;

    float btnActiveWidth = 128;
    float unActiveKeepTime = 2;
    float unActiveTime = 2;
    float btnScaleRatio = 0.51f;

    float lerpSizeSpeed = 10;
    float lerpPosSpeed = 20;
    float btnSpace = 3;
    public int hh;
    RectTransform rectTransform;

    void Start ()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();

        rectTransform = GetComponent<RectTransform>();

        btnRects = new List<RectTransform>();
        initRects = new List<MyRectTransform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Button>() != null)
            {
                RectTransform rectTrans = transform.GetChild(i).GetComponent<RectTransform>();
                MyRectTransform rectTransInit = new MyRectTransform();
                rectTransInit.localPosition = rectTrans.localPosition;
                rectTransInit.sizeDelta = rectTrans.sizeDelta;
                btnRects.Add(rectTrans);
                initRects.Add(rectTransInit);
            }
        }
    }
	
	void Update () {

        AppleMotion();
    }
    void AppleMotion()
    {
        Vector2 posMouseOnBottom;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, canvas.GetComponent<Camera>(), out posMouseOnBottom);
        if (posMouseOnBottom.y > -540 && posMouseOnBottom.y < -440)
        {
            for (int i = 0; i < btnRects.Count; i++)
            {
                float distMS2Btn = Mathf.Abs(posMouseOnBottom.x - initRects[i].localPosition.x+hh);
                if (distMS2Btn < btnActiveWidth)
                {
                    Vector2 sizeTarget = initRects[i].sizeDelta + Vector2.one * (btnActiveWidth - distMS2Btn) * btnScaleRatio;
                    btnRects[i].sizeDelta = Vector2.Lerp(btnRects[i].sizeDelta, sizeTarget, lerpSizeSpeed * Time.deltaTime);
                }
                else
                {
                    btnRects[i].sizeDelta = initRects[i].sizeDelta;
                }
            }
            float totalWidth = 0;
            for (int i = 0; i < btnRects.Count; i++)
            {
                totalWidth += btnRects[i].sizeDelta.x + btnSpace;
            }
            totalWidth -= btnSpace;

            float btnLeftStart = -totalWidth * 0.5f;
            btnRects[0].localPosition = new Vector2(btnLeftStart + btnRects[0].sizeDelta.x * 0.5f, initRects[0].localPosition.y);
            for (int i = 1; i < btnRects.Count; i++)
            {
                float posXTarget = btnRects[i - 1].localPosition.x + btnSpace + (btnRects[i].sizeDelta.x + btnRects[i - 1].sizeDelta.x) * 0.5f;
                Vector2 pos = btnRects[i].localPosition;
                pos.x = Mathf.Lerp(pos.x, posXTarget, lerpPosSpeed * Time.deltaTime);
                btnRects[i].localPosition = pos;
            }
        }
        else
        {
            for (int i = 0; i < btnRects.Count; i++)
            {
                btnRects[i].sizeDelta = initRects[i].sizeDelta;
            }
            float totalWidth = 0;
            for (int i = 0; i < btnRects.Count; i++)
            {
                totalWidth += btnRects[i].sizeDelta.x + btnSpace;
            }
            totalWidth -= btnSpace;
            float btnLeftStart = -totalWidth * 0.5f;
            btnRects[0].localPosition = new Vector2(btnLeftStart + btnRects[0].sizeDelta.x * 0.5f, initRects[0].localPosition.y);
          //  Debug.Log(" btnRects[0].sizeDelta.x * 0.5f = " + btnRects[0].sizeDelta.x * 0.5f);
           // Debug.Log("btnLeftStart" + btnLeftStart);
           // Debug.Log("btnLeftStart + btnRects[0].sizeDelta.x * 0.5f = " + (btnLeftStart + btnRects[0].sizeDelta.x * 0.5f));
            for (int i = 1; i < btnRects.Count; i++)
            {
                float posXTarget = btnRects[i - 1].localPosition.x + btnSpace + (btnRects[i].sizeDelta.x + btnRects[i - 1].sizeDelta.x) * 0.5f;
                Vector2 pos = btnRects[i].localPosition;
                pos.x = Mathf.Lerp(pos.x, posXTarget, lerpPosSpeed * Time.deltaTime);
                btnRects[i].localPosition = pos;
            }

        }
        
    }
}
