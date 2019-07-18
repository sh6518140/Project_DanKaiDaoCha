using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ControllerWindows : MonoBehaviour
{


    public Button[] Bts;


    private void BtsOnClick()
    {
        for (int i = 0; i < Bts.Length; i++)
        {
            //Bts[i].onClick.AddListener(() => { CallApplication.Instance().MonitorWindows(); });
        }
    }


    private void Awake()
    {
        BtsOnClick();
    }


    //float keepTime;
    void Update()
    {
        //keepTime += Time.deltaTime;
        //if (keepTime >= 10)
        //{
        //    CallApplication.Instance().MonitorWindows();
        //    keepTime = 0;
        //}
    }
}
