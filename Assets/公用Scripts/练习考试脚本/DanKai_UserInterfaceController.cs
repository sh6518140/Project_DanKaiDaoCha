using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Slate;
using UnityEngine.EventSystems;


[System.Serializable]
public class StepNameAndLength
{   
    public string ModuleStepName;
    public float StepNameHideTime;
}

[System.Serializable]
public class ProjectStep
{
    public string[] StepContentString;
}

//练习模式和考试模式共同的UI逻辑
public class DanKai_UserInterfaceController : MonoBehaviour {

    //模块名称变量\1.模块名称对象\2.模块名称消失时间(根据音频时长确定)
    public GameObject ModuleNameTip;
    public float ModuleNameHideTime;
    //步骤名称变量\1.步骤名称对象(居中)\2.步骤名称对象(居左)\3.步骤名称输入及隐藏时间
    public GameObject ModuleStepNameTip;
    public GameObject ModuleStepNameLeftTip;
    public StepNameAndLength[] StepNameAndHideTime;
    //步骤内容变量\1.步骤内容Text\2.左进按钮\3.右进按钮\4.当前步骤标号Text\5.步骤内容输入
    public Text StepContentGrid;
    public Button LeftButton;
    public Button RightButton;
    public Text CurrentStepNumberTip;
    public ProjectStep[] ProjectSteps;
    //UI提示对象(0:操作错误。1：操作完成.2:小地图)
    public GameObject[] PromptMessageTip;
    //步骤控制按钮
    public Transform[] StepButtons;
    //工具控制按钮
    public Button[] ToolButtons;
    //小窗口文字显示内容
   // public Text LittleWindows;
    //public string[] WindowsContext;
    //当前工具Text提示
    public Text CurrentToolText;

    //重载场景
    public void RoadScene(string sc)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sc);
    }

    public void ChangeContext(int i)
    {
        //LittleWindows.text = WindowsContext[i];
    }

    //模块名称函数-无外部调用函数
    //步骤名称函数
    public void UI_StepNameTipShow(int sn)
    {
        StepNameTipShow(sn);
    }
    //步骤内容函数\1.显示某步骤第一小步内容\2.按钮上一小步\3.按钮下一小步
    public void UI_ShowStepContent(int bs)
    {
        ShowStepCotent(bs);
    }
    public void UI_TheLastStep()
    {
        TheLastStep();
    }
    public void UI_TheNextStep()
    {
        TheNextStep();
    }

    //UI提示对象函数
    public void UI_PromptMessageShow(int n)
    {
        PromptMessageShow(n);
        if(n==0)
            Invoke("ErrorMessageHide", 2);
    }
    public void UI_PromptMessageHide(int n)
    {
        PromptMessageHide(n);
    }
    //步骤控制按钮函数
    //工具控制函数
    //当前工具Text提示函数
    public void  CurrentToolTextChange(string tl)
    {
        CurrentToolText.text = "当前工具： " + tl;
    }
    #region
    /// <summary>
    /// 模块名称(不需要外部控制)
    /// </summary>
    //public GameObject ModuleNameTip;
    //public int ModuleNameHideTime;
    //UI界面初始化
    private void InterfaceInitialization(float ht)
    {
        //模块名称初始化显示，2s后隐藏
        ModuleNameTip.SetActive(true);
        Invoke("ModuleNameTipHide", ht);
    }
    //模块名称标题隐藏
    private void ModuleNameTipHide()
    {
        ModuleNameTip.SetActive(false);
    }
    /// <summary>
    /// 模块步骤名称(隐藏时间根据音频时长输入)
    /// </summary>
    //public GameObject ModuleStepNameTip;
    //public GameObject ModuleStepNameLeftTip;
    //public StepNameAndLength[] StepNameAndHideTime;
    private void StepNameTipShow(int sn)
    {
        string tc = StepNameAndHideTime[sn].ModuleStepName;
        float nt = StepNameAndHideTime[sn].StepNameHideTime;
       
        if (ModuleStepNameTip == null)
        {
            Debug.Log("errerrererererererererererererererer");
            return;
        }
         
        ModuleStepNameTip.SetActive(true);
        ModuleStepNameTip.GetComponentInChildren<Text>().text = tc;
        ModuleStepNameLeftTip.GetComponentInChildren<Text>().text = tc;
        Invoke("StepNameTipHide", nt); 
    }
    private void StepNameTipHide()
    {
        ModuleStepNameTip.SetActive(false);
    }
    private void PromptMessageTipHide()
    {
        PromptMessageTip[0].SetActive(false);
        PromptMessageTip[1].SetActive(false);
    }
    private void StepStartShowEvent(Cutscene cs)
    {
        int ptcontain = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_StartContainsOrNotInter(cs);
        if (ptcontain >= 0)
        {
            CancelInvoke();
            PromptMessageTipHide();
            ModuleNameTip.SetActive(false);
            StepNameTipShow(ptcontain);
            if (DanKai_PracticeUserInterface.PracticeInstance == null)
                return;
            ShowStepCotent(ptcontain);
            return;
        }
        int opcontain = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_OperationContainsOrNotInter(cs);
        if (opcontain >= 0)
        {
            int bn = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber.Length;
            int insnum = 0;
            for (int i = 0; i < bn; i++)
            {
                int mn = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber[i];
                insnum += mn;
                if (opcontain < insnum)
                {
                    StepContentGrid.text = ProjectSteps[i].StepContentString[opcontain - insnum + mn];
                    CurrentStepNumberTip.text = (opcontain - insnum + mn + 1) + "/" + ProjectSteps[i].StepContentString.Length;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// 步骤内容(隐藏时间根据音频时长输入)
    /// </summary>
    //UI步骤内容文本框
    //public Text StepContentGrid;
    //UI步骤控制按钮
    //public Button LeftButton;
    //public Button RightButton;
    //UI当前步骤数
    //public Text CurrentStepNumberTip;

    //UI多步骤内容；
    //public ProjectStep[] ProjectSteps;

    private int ProjectStepsNumber;
    private int DuttarNumber;

    private void ShowStepCotent(int bs)
    {
        StepContentGrid.text = ProjectSteps[bs].StepContentString[0];
        ProjectStepsNumber = bs;
        DuttarNumber = 0;
        StepNumberFormat();
    }
    private void TheLastStep()
    {
        if (DuttarNumber != 0)
        {
            string ls = ProjectSteps[ProjectStepsNumber].StepContentString[--DuttarNumber];
            StepContentGrid.text = ls;
        }
        StepNumberFormat();
    }
    private void TheNextStep()
    {
        int tn = ProjectSteps[ProjectStepsNumber].StepContentString.Length - 1;
        if (DuttarNumber != tn)
        {
            string ls = ProjectSteps[ProjectStepsNumber].StepContentString[++DuttarNumber];
            StepContentGrid.text = ls;
        }
        StepNumberFormat();
    }
    //步骤内容数输出格式
    private void StepNumberFormat()
    {
        int cn = DuttarNumber + 1;
        int tn = ProjectSteps[ProjectStepsNumber].StepContentString.Length;
        CurrentStepNumberTip.text = cn + "/" + tn;
    }

    /// <summary>
    /// UI提示内容(错误提示、正确提示、小地图提示)
    /// </summary>
    //public GameObject[] PromptMessageTip;
    private void PromptMessageShow(int n)
    {
        PromptMessageTip[n].SetActive(true);
    }
    private void PromptMessageHide(int n)
    {
        PromptMessageTip[n].SetActive(false);
    }
    private void ErrorMessageHide()
    {
        PromptMessageTip[0].SetActive(false);
    }
    private void PromptMessageHide()
    {
        PromptMessageTip[1].SetActive(false);
    }
    //操作步骤小提示完成挂载事件
    private void OperationSussessShowEvent(Cutscene sc)
    {
        int ptcontain = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.CC_OperationContainsOrNotInter(sc);
        if (ptcontain < 0)
            return;
        int bn = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber.Length;
        int insnum = 0;
        for (int i = 0; i < bn; i++)
        {       
            int mn = DanKai_CutSceneController.DanKai_CutSceneControllerInstance.SmallStepNumber[i];
            insnum += mn;
            if (ptcontain == (insnum - 1))
            {
                if (DanKai_ExamUserInterface.ExamUserInstance != null)
                    DanKai_ExamUserInterface.ExamUserInstance.AddScore(i);

                if (DanKai_PracticeUserInterface.PracticeInstance == null)//考试模式不显示步骤完成提示。
                    return;
                PromptMessageShow(1);
                Invoke("PromptMessageHide", 2);
                if (i == (bn - 1))
                    Invoke("ShowProjectOverPanel", 2);
                return;
            }
            else if(ptcontain < (insnum - 1))//考试模式不显示高亮
            {
                DanKai_ProjectObjectController.ProjectObjectControllerInstance.PO_CollisionEnable(ptcontain + 1);
                if (DanKai_PracticeUserInterface.PracticeInstance == null)//练习、考试模式状态都会显示碰撞信息。
                    return;
                DanKai_ProjectObjectController.ProjectObjectControllerInstance.PO_FlashActive(ptcontain + 1);
                //小步骤结束显示下一小步骤提示。
                StepContentGrid.text = ProjectSteps[ProjectStepsNumber].StepContentString[ptcontain + 1 - insnum + mn];
                CurrentStepNumberTip.text = (ptcontain + 2 - insnum + mn) + "/" + ProjectSteps[ProjectStepsNumber].StepContentString.Length;
                DuttarNumber = ptcontain + 1 - insnum + mn;
                return;
            }
        }
    }

    private void ShowProjectOverPanel()
    {
        DanKai_PracticeUserInterface.PracticeInstance.PU_ShowProjectOverPanel();
    }

    /// <summary>
    /// 步骤按钮形态控制
    /// </summary>
    //public Transform[] StepButtons;
    //保存按钮信息的字典
    private Dictionary<Button, int> ButtonMessage = new Dictionary<Button, int>();
    //将按钮信息保存到一个字典
    private void SaveButtonDictionary()
    {
        for(int i= 0;i< StepButtons.Length;i++)
        {
            Button bt = StepButtons[i].GetComponent<Button>();
            ButtonMessage.Add(bt,i);
        }
    }
    private void ButtonFormChange(int bn)
    {
        StepButtons[bn].GetChild(0).gameObject.SetActive(true);
        for (int i = 0; i < StepButtons.Length; i++)
        {
            if (i != bn)
                StepButtons[i].GetChild(0).gameObject.SetActive(false);
        }
    }
    private void ButtonFormChangeEventAdd()
    {
        SaveButtonDictionary();
        for (int i = 0; i < StepButtons.Length; i++)
        {
            Button bt = StepButtons[i].GetComponent<Button>();
            bt.onClick.AddListener(() => { ButtonFormChange(ButtonMessage[bt]); });
            //bt.onClick.AddListener //(ProjectObjectController_07.ProjectObjectControllerInstance.PO_FlashColliderDisable);
             bt.onClick.AddListener(() => { DanKai_CutSceneController.DanKai_CutSceneControllerInstance.StopAndPlayCutscene(ButtonMessage[bt]); });
        }
    }
    /// <summary>
    /// 工具按钮光标控制
    /// </summary>
    private Dictionary<Button, int> ToolsButtonDictronary = new Dictionary<Button, int>();
    private void SaveDictronaryCache()
    {
        for(int i = 0;i<ToolButtons.Length;i++)
        {
            ToolsButtonDictronary.Add(ToolButtons[i],i);
        }
    }
    private void ToolsButtonEvent()
    {
        SaveDictronaryCache();
        foreach(var i in ToolButtons)
        {        
            i.onClick.AddListener(()=> DanKai_MouseController.Mousecontrollerinstance.MC_ChangeCursorStyle(ToolsButtonDictronary[i]));
        }
    }


    #endregion




    /// <summary>
    /// 生命周期函数
    /// </summary>

    private static DanKai_UserInterfaceController uiinstance;

    public static DanKai_UserInterfaceController UIInstance
    {
        get
        {
            return uiinstance;
        }

        set
        {
            uiinstance = value;
        }
    }

    private void Awake()
    {
        if (uiinstance == null)
            uiinstance = this;
    }

    void Start()
    {      
        //步骤开始标题显示
        Cutscene.OnCutsceneStarted += StepStartShowEvent;
        //操作步骤完成挂载事件
        Cutscene.OnCutsceneStopped += OperationSussessShowEvent;
        //工具选择错误点击后显示错误提示。
        DanKai_ProjectObjectController.GeneralUIEvent += UI_PromptMessageShow;
        ButtonFormChangeEventAdd();
        ToolsButtonEvent();
        InterfaceInitialization(ModuleNameHideTime);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy()
    {
        Cutscene.OnCutsceneStarted -= StepStartShowEvent;
        //操作步骤完成挂载事件
        Cutscene.OnCutsceneStopped -= OperationSussessShowEvent;
        DanKai_ProjectObjectController.GeneralUIEvent -= UI_PromptMessageShow;
        uiinstance = null;
    }

}
