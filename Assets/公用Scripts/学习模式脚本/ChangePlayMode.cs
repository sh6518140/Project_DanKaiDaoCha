using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangePlayMode : MonoBehaviour
{
    public static ChangePlayMode _instans;
    public GameObject[] Int_Interface;

    public GameObject VideoControllBar;
    public GameObject[] logo;
    // Use this for initialization
    private void Awake()
    {
        _instans = this;
    }
    void Start()
    {
        ClickTimes = 0;
        SelfRect = GetComponent<RectTransform>();
        InsAnchorPos = SelfRect.anchorMin;
        SelfRect.anchorMin = InsAnchorPos;
        SelfRect.anchorMax = InsAnchorPos;
        SelfRect.localScale = Vector2.one * 0.88f;
        SelfRect.sizeDelta = new Vector2(1920, 1080);
        foreach (var i in Int_Interface)
        {
            i.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickTimes = 1;
            ChangeinterFace();
        }
    }
    //0:min 1:max
    private RectTransform SelfRect;
    private Vector2 InsAnchorPos;
    public static int ClickTimes;

    public void ChangeinterFace()
    {
        ++ClickTimes;
        if (ClickTimes % 2 == 1)
        {
            //全屏模式
            SelfRect.pivot = Vector2.one * 0.5f;
            SelfRect.anchorMin = Vector2.zero;
            SelfRect.anchorMax = Vector2.one;
            SelfRect.offsetMax = Vector2.zero;
            SelfRect.offsetMin = Vector2.zero;
            SelfRect.localScale = Vector2.one;

            foreach (var i in Int_Interface)
            {
                i.SetActive(false);
            }
            VideoControllBar.SetActive(true);
            for (int i = 0; i < logo.Length; i++)
            {
                if (logo[i].activeSelf)
                {
                    if (logo.Length == 2)
                    {
                        logo[0].SetActive(false);
                        logo[1].SetActive(true);
                    }
                    else
                    {
                        logo[0].SetActive(false);
                        logo[1].SetActive(false);
                        logo[2].SetActive(true);
                        logo[3].SetActive(true);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        else
        {
            //小屏幕模式
            SelfRect.pivot = Vector2.one;
            SelfRect.anchorMin = InsAnchorPos;
            SelfRect.anchorMax = InsAnchorPos;
            SelfRect.localScale = Vector2.one * 0.88f;
            SelfRect.sizeDelta = new Vector2(1920, 1080);

            foreach (var i in Int_Interface)
            {
                i.SetActive(true);
            }
            VideoControllBar.SetActive(false);
            for (int i = 0; i < logo.Length; i++)
            {
                if (logo[i].activeSelf)
                {
                    if (logo.Length == 2)
                    {
                        logo[0].SetActive(true);
                        logo[1].SetActive(false);
                    }
                    else
                    {
                        logo[0].SetActive(true);
                        logo[1].SetActive(true);
                        logo[2].SetActive(false);
                        logo[3].SetActive(false);
                    }
                }
                else
                {
                    //return;
                }
            }
        }

    }

}
