//------------------------------
//  MenuUI : 组件基本模块
//  Editor ：jhj
//  Data : 2016-5-25
//------------------------------

using UnityEngine;
using System.Collections;
//using HighlightingSystem;


/*
 * 组件基类 
 *
 */

public class CElement : MonoBehaviour {

    private string strName;
    //public ArrayType TYPE;


    public bool bShowHighlight = false;
    public Transform transCenter;
    public Vector3 vCenter = Vector3.zero;
    public bool seeThrough = true;
    protected bool _seeThrough = true;
    //protected Highlighter h;

    // 
    protected void Awake()
    {
        //h = GetComponent<Highlighter>();
        //if (h == null) { h = gameObject.AddComponent<Highlighter>(); }
            
        Transform transCenter;
        transCenter = this.gameObject.transform.Find("center");
        if (transCenter != null)
            vCenter = transCenter.localPosition;

        //Debug.Log(this.gameObject.name  + transCenter.localPosition);

    }



    /// <summary>
    /// 初始化名字
    /// </summary>
    /// <param name="strObjectName"></param>
    public void InitName(string strObjectName)
    {  

    }


    // 
    void OnEnable()
    {
        //if (seeThrough) { h.SeeThroughOn(); }
        //else { h.SeeThroughOff(); }
    }

    // 
    protected void Start() { }

    // 
    protected void LateUpdate()
    {
        //if (_seeThrough != seeThrough)
        //{
        //    _seeThrough = seeThrough;
        //    if (_seeThrough) { h.SeeThroughOn(); }
        //    else { h.SeeThroughOff(); }
        //}

        //// Fade in/out constant highlighting with button '1'
        //if (Input.GetKeyDown(KeyCode.Alpha1)) { h.ConstantSwitch(); }

        //// Turn on/off constant highlighting with button '2'
        //else if (Input.GetKeyDown(KeyCode.Alpha2)) { h.ConstantSwitchImmediate(); }

        //// Turn off all highlighting modes with button '3'
        //if (Input.GetKeyDown(KeyCode.Alpha3)) { h.Off(); }


        //if(bShowHighlight)
        //    h.On(Color.yellow);

    }

    // 
    public void MouseOver()
    {
        // Highlight object for one frame in case MouseOver event has arrived
        //h.On(Color.blue);
    }

    // 
    public void ShowHighLight()
    {
        bShowHighlight = true; 
    }

    // 
    public void HideHighLight()
    {
        bShowHighlight = false;
    }
}
