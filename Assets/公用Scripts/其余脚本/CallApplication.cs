using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System;

public class CallApplication 
{
    private static CallApplication _instance;
    public static CallApplication Instance()
    {

        if (_instance == null)
        {
            _instance = new CallApplication();
        }
        return _instance;
    }
    private static string outputPath = "D:/录音啦/录音啦.exe";
    private int x = 0, y = 0;
    private bool isOn = false;
    private int minute = 0;
    private string[] exeName = {"EV录屏","coursemaker","轻松录屏","万彩录屏大师","LiveView 3.5.2","剪辑师 1.6.0.589","屏幕录像软件 SCREEN2SWF 3.7", "苹果录屏大师 1.0.2.3","Apowersoft视频下载王"
    ,"录屏大师","录屏王","Apowersoft Screen Recorder Pro","超级录屏","kk录像机","Apowersoft Screen Recorder Pro 2"};

    // Use this for initialization  
    void Start()
    {
      
        UnityEngine.Debug.Log("当前应用：" + Process.GetCurrentProcess().ProcessName + " 进程ID: " + Process.GetCurrentProcess().Id);
    }
    void Update() {
        MonitorWindows();
    }
    public void MonitorWindows() {
        ListAllAppliction();
        for (int i = 0; i < exeName.Length; i++)
        {
            if (CheckProcess(exeName[i]))
            {
                KillProcess(exeName[i]);
            }
        }
    }
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 200, 50), "打开外部应用"))
    //    {
    //        if (CheckProcess("录音啦"))
    //            return;
    //        else
    //            StartProcess(outputPath);
    //    }
    //    if (GUI.Button(new Rect(10, 60, 200, 50), "杀死应用进程"))
    //    {
    //        KillProcess("录音啦");
    //    }
    //    if (GUI.Button(new Rect(10, 110, 200, 50), "开启定时关闭"))
    //    {
    //        minute = System.DateTime.Now.Minute + 1;
    //        isOn = true;
    //    }
    //    if (isOn)
    //    {
    //        GUI.contentColor = Color.red;
    //        GUI.Label(new Rect(10, 160, 160, 30), "\t倒计时:" + (60 - System.DateTime.Now.Second));
    //        if (System.DateTime.Now.Minute == minute)
    //        {
    //            UnityEngine.Debug.Log("自动关闭应用....");
    //            KillProcess("kwmusic");
    //            isOn = false;
    //        }
    //    }
    //}
    /// <summary>  
    /// 开启应用  
    /// </summary>  
    /// <param name="ApplicationPath"></param>  
    void StartProcess(string ApplicationPath)
    {
        UnityEngine.Debug.Log("打开本地应用");
        Process foo = new Process();
        foo.StartInfo.FileName = ApplicationPath;
        foo.Start();
    }

    /// <summary>  
    /// 检查应用是否正在运行  
    /// </summary>  
    bool CheckProcess(string processName)
    {
        bool isRunning = false;
        Process[] processes = Process.GetProcesses();
        int i = 0;
        foreach (Process process in processes)
        {
            try
            {
                i++;
                if (!process.HasExited)
                {
                    if (process.ProcessName.Contains(processName))
                    {
                        UnityEngine.Debug.Log(processName + "正在运行");
                        isRunning = true;
                        continue;
                    }
                    else if (!process.ProcessName.Contains(processName) && i > processes.Length)
                    {
                        UnityEngine.Debug.Log(processName + "没有运行");
                        isRunning = false;
                    }
                }
            }
            catch (Exception ep)
            {
            }
        }
        return isRunning;
    }
    /// <summary>  
    /// 列出已开启的应用  
    /// </summary>  
    void ListAllAppliction()
    {
        Process[] processes = Process.GetProcesses();
        int i = 0;
        foreach (Process process in processes)
        {
            try
            {
                if (!process.HasExited)
                {
                    //UnityEngine.Debug.Log("应用ID:" + process.Id + "应用名:" + process.ProcessName);
                }
            }
            catch (Exception ep)
            {
            }
        }
    }
    /// <summary>  
    /// 杀死进程  
    /// </summary>  
    /// <param name="processName">应用程序名</param>  
    void KillProcess(string processName)
    {
        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            try
            {
                if (!process.HasExited)
                {
                    if (process.ProcessName == processName)
                    {
                        process.Kill();
                        UnityEngine.Debug.Log("已杀死进程");
                    }
                }
            }
            catch (System.InvalidOperationException)
            {
                //UnityEngine.Debug.Log("Holy batman we've got an exception!");  
            }
        }
    }

}