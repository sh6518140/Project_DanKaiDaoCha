using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
public class ControllerButton : MonoBehaviour {
	[DllImport("user32.dll")]
	public static extern bool ShowWindow(System.IntPtr hwnd, int nCmdShow);
	[DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
	public static extern System.IntPtr GetForegroundWindow();
	int count;//最大化时计数
    
    public GameObject bangZhuBG;
    bool isShowBangZhu;

    void Start () {
		//Cursor.SetCursor(null,Vector2.zero,CursorMode.ForceSoftware);
		count = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShowBangZhu)
        {
            bangZhuBG.SetActive(true);
        }
        else
        {
            bangZhuBG.SetActive(false);
        }
	}
	public void QuitButton()//退出
	{
		Application.Quit ();
	}


    public void SceneSkip(string sceneName)//加载场景
    {

        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
    }
	public void MinWindow()//最小化
	{
		ShowWindow(GetForegroundWindow(), 2);//
	}
	public void SetHelpButton()//帮打打开
	{
        isShowBangZhu = !isShowBangZhu;
    }


    public GameObject quitBG;
    public void ShowQuitBG()
    {
        quitBG.SetActive(true);
    }
    public void HideQuitBG()
    {
        quitBG.SetActive(false);
    }


    public void QieHuanEXE()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        UnityEngine.SceneManagement.SceneManager.LoadScene("集中检修选择界面");
    }

}
