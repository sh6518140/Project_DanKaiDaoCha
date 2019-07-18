using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Selfmagnification : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Mem_Magnification = Fuc_Magnification();
        Vector3 ls = GetComponent<RectTransform>().localScale;
        GetComponent<RectTransform>().localScale = (ls - new Vector3(0, 0, ls.z)) * Mem_Magnification + new Vector3(0, 0, ls.z);    
    }

    // Update is called once per frame
    void Update () {
		
	}


    private float Mem_Magnification;

    //返回缩放比
    private float Fuc_Magnification()
    {
        float mag = 0;

        float ins = 1920 * 1.0f / 1080;
        float cha = Screen.width * 1.0f / Screen.height;

        if (ins > cha)
        {
            mag = Screen.width * 1.0f / 1920;
        }
        else
        {
            mag = Screen.height * 1.0f / 1080;
        }
        return mag;
    }


}
