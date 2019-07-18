using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slate;
using UnityEngine.UI;

[System.Serializable]
public class OperationCutscenes
{
    public Cutscene[] OperationCutscene;
}

public class DanKai_CutSceneController : MonoBehaviour
{
    public static DanKai_CutSceneController DanKai_CutSceneControllerInstance
    {
        set
        {
            DanKai_cutSingleInstance = value;
        }

        get
        {
            return DanKai_cutSingleInstance;
        }
    }
    // 项目开始动画
    public Cutscene ProjectCutscene;
    // 各步骤开始动画
    public Cutscene[] CameraCutscene;
    // 各步骤操作动画
    public int[] SmallStepNumber;
    public Cutscene[] OperationCutscene;

    // 项目开始函数
    public bool CC_ProjectStartContainsOrNot(Cutscene cs)
    {
        return ProjectStartContainsOrNot(cs);
    }
    // 各步骤开始函数
    public void CC_CameraCutsceneStart(int index)
    {
        for (int i = 0; i<CameraCutscene.Length; i++)
        {
            CameraCutscene[i].Stop();
        }
        for (int j =0; j<OperationCutscene.Length; j++)
        {
            OperationCutscene[j].Stop();
        }
        CameraCutscene[index].Play();
    }
    public bool CC_StartContainsOrNot(Cutscene cs)
    {
        return StartContainsOrNot(cs);
    }
    public int CC_StartContainsOrNotInter(Cutscene cs)
    {
        return StartContainsOrNotInter(cs);
    }
    // 各步骤操作函数
    public void CC_OperationCutsceneStart(int index)
    {
        OperationCutsceneStart(index);
    }
    public bool CC_OperationContainsOrNot(Cutscene cs)
    {
        return OperationContainsOrNot(cs);
    }
    public int  CC_OperationContainsOrNotInter(Cutscene cs)
    {
        return OperationContainsOrNotInter(cs);
    }
    #region
    /// <summary>
    /// 项目开始动画
    /// </summary>
    //public Cutscene ProjectCutscene;
    private static DanKai_CutSceneController DanKai_cutSingleInstance;
    private bool ProjectStartContainsOrNot(Cutscene cs)
    {
        if (ProjectCutscene.Equals(cs))
            return true;
        else
            return false;
    }
    private void ProjectCutsceneStart()
    {
        ProjectCutscene.Play();
    }
    /// <summary>
    /// 步骤开始动画
    /// </summary>
    //public Cutscene[] CameraCutscene;

    //public void CameraCutsceneStart(int index)
    //{
    //    CameraCutscene[index].Play();
    //}
    
    private bool StartContainsOrNot(Cutscene cs)
    {
        foreach (var cc in CameraCutscene)
        {
            if (cc.Equals(cs))
                return true;
        }
        return false;
    }
    private int StartContainsOrNotInter(Cutscene cs)
    {
        int i = 0;
        foreach (var cc in CameraCutscene)
        {
            if (cc.Equals(cs))
                return i;
            i++;
        }
        return -1;
    }
    /// <summary>
    /// 步骤操作动画
    /// </summary>
    //public Cutscene[] OperationCutscene;
    private void OperationCutsceneStart(int index)
    {
        OperationCutscene[index].Play();
    }
    private bool OperationContainsOrNot(Cutscene cs)
    {
        foreach (var cc in OperationCutscene)
        {
            if (cc.Equals(cs))
                return true;
        }
        return false;
    }
    private int OperationContainsOrNotInter(Cutscene cs)
    {
        int i = 0;
        foreach (var cc in OperationCutscene)
        {
            if (cc.Equals(cs))
                return i;
            i++;
        }
        return -1;
    }


    #endregion

    /// <summary>
    /// 生命周期函数
    /// </summary>
    #region
    // Use this for initialization
    private void Awake()
    {
        if (DanKai_CutSceneControllerInstance == null)
        {
            DanKai_CutSceneControllerInstance = this;
        }
    }
    void Start()
    {

        //点击高亮开始操作动画
        DanKai_ProjectObjectController.CutSceneEvent += OperationCutsceneStart;
        Cutscene.OnCutsceneStarted += SetCurrentCutscene;
        ProjectCutsceneStart();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnDestroy()
    {
        DanKai_ProjectObjectController.CutSceneEvent -= OperationCutsceneStart;
        DanKai_CutSceneControllerInstance = null;
    }

    #endregion
    private Cutscene CurrentPlayingCutscene;
    private void SetCurrentCutscene(Cutscene cs)
    {
        CurrentPlayingCutscene = cs;
    }

    //public void StopAndPlayCutscene(int cs)
    //{
    //    if (CurrentPlayingCutscene != null)
    //    {
    //        CurrentPlayingCutscene.Stop();
    //        ProjectObjectController.ProjectObjectControllerInstance.PO_FlashColliderDisable();
    //    }
    //    if (CurrentPlayingCutscene = CameraCutscene[cs])
    //        StartCoroutine(IE_PlaySameCameraCutscene(cs));
    //    else
    //        CameraCutscene[cs].Play();
    //}
    //IEnumerator IE_PlaySameCameraCutscene(int cs)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    CameraCutscene[cs].Play();
    //}
    public void StopAndPlayCutscene(int cs)
    {
        if (CurrentPlayingCutscene != null)
        {
            CurrentPlayingCutscene.Stop();
            DanKai_ProjectObjectController.ProjectObjectControllerInstance.PO_FlashColliderDisable();
        }


        if (CurrentPlayingCutscene == CameraCutscene[cs])
            StartCoroutine(IE_PlaySameCameraCutscene(cs));
        else
            CameraCutscene[cs].Play();


    }
    IEnumerator IE_PlaySameCameraCutscene(int cs)
    {
        yield return null;
        CameraCutscene[cs].Play();
    }

    public Text strText;
    public void PauseOrPlay()
    {
        if (CurrentPlayingCutscene == null)
            return;
        if (!CurrentPlayingCutscene.isPaused)
        {
            CurrentPlayingCutscene.Pause();
            strText.text = "继  续";
        }
        else
        {
            CurrentPlayingCutscene.Play();
            strText.text = "暂  停";
        }
    }
    public void BuZhouPlay()
    {
        strText.text = "暂  停";
    }
}
