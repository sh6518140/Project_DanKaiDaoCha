using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slate;
using UnityEngine.UI;

[System.Serializable]
public class StepScoreManager
{
    public float StepScore;
    private bool scoreadded;
    public bool ScoreAdded
    {
        set
        {
            scoreadded = value;
        }
        get
        {
            return scoreadded;
        }
    }
}

public class DanKai_ExamUserInterface : MonoBehaviour
{
    //记分提交面板
    public GameObject ResultPanel;
    //倒计时变量\1:当前时间Text\2:总共的时间
    public Text CurTimeText;
    public int TotalTime;
    //加分涉及变量\1：分数Text\2:提交面板对错符号\3：提交面板步骤名称Text\4:各步骤分数确定已经是否加过分数判断
    public Text ScoreText;
    public Text[] StepTip;
    public Image[] Signel;
    public StepScoreManager[] StepScoreInput;


    //倒计时函数----无外部调用函数
    //记分提交面板显示函数
    public void EU_ShowResult()
    {
        ShowResult();
    }
    //加分函数
    public void EU_AddScore(int sp)
    {
        AddScore(sp);
    }

    #region
    //提交面板显示后不再加分；
    private bool ProjectOver;

    /// <summary>
    /// 倒计时功能
    /// </summary>

    //public Text CurTimeText;
    //public int TotalTime;
    private float CurTime;
    //倒计时协程
    private IEnumerator startTime()
    {
        while (CurTime > 0)
        {
            yield return new WaitForSeconds(1);//由于开始倒计时，需要经过一秒才开始减去1秒，
            CurTime--;
            //TimeText.text = "Time:" + CurTime;      
            ChangeFormat();
        }
    }
    //更改时间显示格式
    private void ChangeFormat()
    {
        int mu = (int)CurTime / 60; //输出显示分
        int se = (int)CurTime % 60; //输出显示秒
        string length = mu.ToString();
        if (se >= 10)
        {
            if (mu >= 10)
                CurTimeText.text = "倒计时：    " + mu + ":" + se;
            else
                CurTimeText.text = "倒计时：    " + "0" + mu + ":" + se;
        }     //如果秒大于10的时候，就输出格式为 00：00
        else
        {
            if (mu >= 10)
                CurTimeText.text = "倒计时：    " + mu + ":0" + se;
            else
                CurTimeText.text = "倒计时：    " + "0" + mu + ":0" + se;      //如果秒小于10的时候，就输出格式为 00：00
        }
    }

    /// <summary>
    /// 提交考试结果（提交面板显示和提交按钮函数。提交面板包括分数显示，和对应步骤样式改变）
    /// </summary>

    // public GameObject ResultPanel;
    private void ShowResult()
    {
        ScoreText.text = "" + CurScore + "分";
        ResultPanel.SetActive(true);
        StopCoroutine("startTime");
        ProjectOver = true;
    }


    /* 加分功能 */
    //public StepScoreManager[] StepScoreInput;
    //public Text ScoreText;
    //public Image[] Signel;
    //public Text[] StepTip;

    private float CurScore;
    //步骤结束时加分
    public void AddScore(int sp)
    {
        if (!StepScoreInput[sp].ScoreAdded && !ProjectOver)
        {
            Signel[sp].sprite = Resources.Load("对号", typeof(Sprite)) as Sprite;
            StepTip[sp].color = Color.black;
            CurScore += StepScoreInput[sp].StepScore;
            StepScoreInput[sp].ScoreAdded = true; ;
        }
    }

    /// <summary>
    /// 生命周期函数
    /// </summary>

    // Use this for initialization

    private static DanKai_ExamUserInterface examuerinstance;

    public static DanKai_ExamUserInterface ExamUserInstance
    {
        get
        {
            return examuerinstance;
        }

        set
        {
            examuerinstance = value;
        }
    }

    private void Awake()
    {
        if (ExamUserInstance == null)
            ExamUserInstance = this;
    }


    void Start()
    {

        CurTime = TotalTime;
        ChangeFormat();
        ResultPanel.SetActive(false);
        StartCoroutine("startTime");
    }

    // Update is called once per frame
    void Update()
    {
        if (CurTime <= 0)
            ShowResult();
    }

    private void OnDestroy()
    {
        ExamUserInstance = null;
    }

    #endregion
}
