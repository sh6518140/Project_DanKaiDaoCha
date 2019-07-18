using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTrigger_TwoLevel1 : MonoBehaviour
{

    public Text[] tex;
    public Sprite spr;
    public Sprite spr1;
    public string[] str;
    public Button[] but;
    public void ShowStr(int i)
    {
        tex[i].text = str[i];
        //  but[i].GetComponent<Image>().sprite = spr;

    }
    public void HideStr(int i)
    {
        tex[i].text = "  ";
        //   but[i].GetComponent<Image>().sprite = spr1;
    }

}
