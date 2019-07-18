using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slate;

//鼠标光标
public class DanKai_MouseController : MonoBehaviour {
    //初始默认光标样式
    public string DefaultStyle;
    //光标样式列表
    public string[] CursorStyle;

    //锁定光标 -- 无 
    //改变鼠标光标样式
    private int MouseStyleInte;
    private int LastMouseStyleIndex;

    public void MC_ChangeCursorStyle(int sn)
    {
        if (sn == LastMouseStyleIndex)
            MouseStyleInte++;

        if (MouseStyleInte % 2 == 0)
        {
            CursorTexture = Resources.Load(CursorStyle[sn], typeof(Texture2D)) as Texture2D;
            Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
            GL_Static.textureName = CursorStyle[sn];
            LastMouseStyleIndex = sn;
            if (DanKai_UserInterfaceController.UIInstance != null)
                DanKai_UserInterfaceController.UIInstance.CurrentToolTextChange(CursorStyle[sn]);
        }
        else
            MC_ChangeCursorStyle("无");
    }
    //改变鼠标光标样式-重载
    public void MC_ChangeCursorStyle(string cn)
    {
        LastMouseStyleIndex = -1;
        MouseStyleInte = 0;
        CursorTexture = Resources.Load(cn, typeof(Texture2D)) as Texture2D;
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        GL_Static.textureName = cn;
        if (DanKai_UserInterfaceController.UIInstance != null)
            DanKai_UserInterfaceController.UIInstance.CurrentToolTextChange(cn);
    }
    #region
    private Texture2D CursorTexture;
    //鼠标光标显示隐藏
    public void MouseVisuality(bool iv)
    {
        Cursor.visible = iv;
        Cursor.lockState = iv ? CursorLockMode.None : CursorLockMode.Locked;      
    }
    private void MouseShowOut()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MC_ChangeCursorStyle(DefaultStyle);
    }
    private void MouseEnableEvent(Cutscene ct)
    {
        MouseVisuality(false);
    }
    //项目开始，步骤开始动画的结束直接显示鼠标；操作动画结束2s后显示鼠标；
    private void MouseUnenableEvent(Cutscene ct)
    {
        bool ptcontain = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_ProjectStartContainsOrNot(ct);
        bool stcontain = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_StartContainsOrNot(ct);

        int ptcontains = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_OperationContainsOrNotInter(ct);

        if (stcontain || ptcontain)
            MouseVisuality(true);
        else
        {
            int bn = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber.Length;
            int insnum = 0;
            for (int i = 0; i < bn; i++)
            {
                insnum += DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber[i];
                if (ptcontains == (insnum - 1))
                {
                    Invoke("MouseShowOut", 2);
                    break;
                }
                if (i == (bn - 1))
                    MouseVisuality(true);
            }
        }
        MC_ChangeCursorStyle(DefaultStyle);
    }
    #endregion
    /// <summary>
    /// 生命周期函数
    /// </summary>
    private static DanKai_MouseController mousecontrollerinstance;
    public static DanKai_MouseController Mousecontrollerinstance
    {
        get
        {
            return mousecontrollerinstance;
        }

        set
        {
            mousecontrollerinstance = value;
        }
    }

    private void Awake()
    {
        Cutscene.OnCutsceneStarted += MouseEnableEvent;
        if (mousecontrollerinstance == null)
            mousecontrollerinstance = this;
    }

    void Start()
    {       
        Cutscene.OnCutsceneStopped += MouseUnenableEvent;
        MC_ChangeCursorStyle(DefaultStyle);
    }
    void Update()
    {
        //鼠标右键解锁鼠标
        if (Input.GetMouseButtonDown(1))
        {
            MC_ChangeCursorStyle(DefaultStyle);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnDestroy()
    {
        Cutscene.OnCutsceneStarted -= MouseEnableEvent;
        Cutscene.OnCutsceneStopped -= MouseUnenableEvent;
        mousecontrollerinstance = null;
    }
}
