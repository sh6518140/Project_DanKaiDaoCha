using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanKai_PracticeUserInterface : MonoBehaviour {
    /// <summary>
    /// 显示结束面板
    /// </summary>
    //结束面板对象
    public GameObject ProjectOverPanel;
    public GameObject LeftPanel;
    //显示结束面板
   


    private static DanKai_PracticeUserInterface practiceInstance;

    public static DanKai_PracticeUserInterface PracticeInstance
    {
        get
        {
            return practiceInstance;
        }

        set
        {
            practiceInstance = value;
        }
    }
    public void PU_ShowProjectOverPanel()
    {
        ProjectOverPanel.SetActive(true);
        LeftPanel.SetActive(false);
    }

    private void Awake()
    {
        if(practiceInstance==null)
        {
            practiceInstance = this;
        }
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        practiceInstance = null;
    }
}
