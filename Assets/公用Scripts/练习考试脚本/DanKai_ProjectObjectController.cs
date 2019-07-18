using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightingSystem;
using UnityEngine.EventSystems;
using Slate;

public delegate void CutSceneDeLegate(int index);

public class DanKai_ProjectObjectController : MonoBehaviour {

    //场景中用到的工具
    public GameObject[] Instruments;
    // 带高亮碰撞的物体
    public GameObject[] FlashCollisionObjects;
    //没有对应操作动画的工具

    public GameObject[] InstrumentsWithOutCut;

    //场景中用到的工具函数
    public void ShowInstrument(int n)
    {
        Instruments[n].SetActive(true);
    }
    public void HideInstrument(int n)
    {
        Instruments[n].SetActive(false);
    }
    // 带高亮碰撞的物体函数
    public void PO_CollisionEnable(int n)
    {
        CollisionEnable(n);
    }
    public void PO_CollisionDisable()
    {
        CollisionDisable();
    }
    public void PO_FlashActive(int n)
    {
        FlashActive(n);
    }
    public void PO_FlashActiveDisActive()
    {
        FlashActiveDisActive();
    }
    public void PO_FlashColliderenable(int i)
    {
       CollisionEnable(i);
       FlashActive(i);
    }
    public void PO_FlashColliderDisable()
    {
        FlashActiveDisActive();
        CollisionDisable();
    }

    //没有对应操作动画的工具
    public void PO_ShowInstrumentsWithOutCut(int i)
    {
        InstrumentsWithOutCut[i].SetActive(true);
    }
    public void PO_HideInstrumentsWithOutCut(int i)
    {
        InstrumentsWithOutCut[i].SetActive(false);
    }

    #region
    public static CutSceneDeLegate CutSceneEvent;
    public static CutSceneDeLegate GeneralUIEvent;

    /// <summary>
    /// 场景中用到的工具
    /// </summary>
    //public GameObject[] Instruments;
    //工具显示和隐藏
    //public void ShowInstrument(int n)
    //{
    //    Instruments[n].SetActive(true);
    //}
    //public void HideInstrument(int n)
    //{
    //    Instruments[n].SetActive(false);
    //}

    /// <summary>
    /// 带高亮碰撞的物体
    /// </summary>

    //public GameObject[] FlashCollisionObjects;
    private void CollisionEnable(int i)
    {
        Collider[] cods = FlashCollisionObjects[i].GetComponentsInChildren<Collider>();
        foreach (var cd in cods)
        {
            cd.enabled = true;
        }
        for (int n = 0; n < FlashCollisionObjects.Length; n++)
        {
            if (n != i)
            {
                cods = FlashCollisionObjects[n].GetComponentsInChildren<Collider>();
                foreach (var cd in cods)
                {
                    cd.enabled = false;
                }
            }
        }
    }
    //高亮闪烁
    private void FlashActive(int n)
    {
        Highlighter fs  = FlashCollisionObjects[n].GetComponent<Highlighter>();
        fs.FlashingOn(Color.red, Color.green, 1);
    }
    private void CollisionDisable()
    {
        for (int n = 0; n < FlashCollisionObjects.Length; n++)
        {
            Collider[] cods = FlashCollisionObjects[n].GetComponentsInChildren<Collider>();
            foreach (var cd in cods)
            {
                cd.enabled = false;
            }
        }
    }
    public void FlashActiveDisActive()
    {
        for (int i = 0; i < FlashCollisionObjects.Length; i++)
        {
            Highlighter fs = FlashCollisionObjects[i].GetComponent<Highlighter>();
            fs.FlashingOff();
        }
    }
    //开场动画结束执行的触发高亮碰撞事件
    private void FlashAndCollisionShowEvent(Cutscene sc)
    {
        int ptcontain = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_StartContainsOrNotInter(sc);
        if (ptcontain >= 0)
        {
            int wgc = 0;
            int bn = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber.Length;
            for (int i = 0;i < ptcontain; i++)
            {
                wgc += DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber[i];
            }
            CollisionEnable(wgc);
            if (DanKai_PracticeUserInterface.PracticeInstance == null)
                return;
            FlashActive(wgc);
        }    
    }

    //返回被射线检测触碰到的物体
    private GameObject GetCollisionBeShooted()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (hitInfo.collider != null)
            {
                return hitInfo.collider.gameObject;
            }
            else
                return null;
        }
        else
            return null;
    }
    //根据操作结果触发对应事件
    private void OperationCorrectOrNot()
    {
        GameObject rg = GetCollisionBeShooted();
        if (rg == null)
            return;
        for (int i = 0; i < FlashCollisionObjects.Length; i++)
        {
            if (rg.name == FlashCollisionObjects[i].name)
            {
                if (Instruments[i].name == GL_Static.textureName)
                {
                    CutSceneEvent(i);
                    CollisionDisable();
                    FlashActiveDisActive();
                }
                else if (DanKai_PracticeUserInterface.PracticeInstance != null)
                    GeneralUIEvent(0);
                break;
            }
        }
    }


    #endregion
    /// <summary>
    /// 生命周期函数
    /// </summary>
    #region
    private static DanKai_ProjectObjectController projectobjectcontrollerinstance;
    public static DanKai_ProjectObjectController ProjectObjectControllerInstance
    {
        get
        {
            return projectobjectcontrollerinstance;
        }

        set
        {
            projectobjectcontrollerinstance = value;
        }
    }

    private void Awake()
    {
        if (ProjectObjectControllerInstance == null)
            ProjectObjectControllerInstance = this;
    }

    // Use this for initialization
    void Start () {    
        Cutscene.OnCutsceneStopped += FlashAndCollisionShowEvent;     
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            OperationCorrectOrNot();
        }
    }

    private void OnDestroy()
    {
        Cutscene.OnCutsceneStopped -= FlashAndCollisionShowEvent;
        ProjectObjectControllerInstance = null;
    }

    #endregion
}
