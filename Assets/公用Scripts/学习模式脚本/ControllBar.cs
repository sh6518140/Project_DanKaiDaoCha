using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ControllBar : MonoBehaviour
{
    public Color Tran;


    // Use this for initialization
    void Start () {
        AllImages = GetComponentsInChildren<Image>();
        AllColors = new Color[AllImages.Length];
        for (int i = 0; i < AllColors.Length; i++)
        {
            AllColors[i] = AllImages[i].color;
            AllImages[i].color = Tran;
        }
    }

    // 0:鼠标最小 1鼠标大点
    public Vector2[] MousePos;

    // Update is called once per frame
    void Update()
    {
        if (ChangePlayMode.ClickTimes % 2 == 0)
            return;

        if (Input.mousePosition.x/1920.0 >= MousePos[0].x && Input.mousePosition.x / 1920.0 <= MousePos[1].x)
        {
            if (Input.mousePosition.y/1080.0 >= MousePos[0].y && Input.mousePosition.y / 1080.0 <= MousePos[1].y)
            {
                for (int j = 0; j < AllColors.Length; j++)
                {
                    AllImages[j].color = AllColors[j];
                }
            }
            else
            {
                for (int j = 0; j < AllColors.Length; j++)
                {
                    AllImages[j].color = Tran;
                }
            }
        }
        else
        {
            for (int j = 0; j < AllColors.Length; j++)
            {
                AllImages[j].color = Tran;
            }
        }


    }

    private Image[] AllImages;
    private Color[] AllColors;
}
