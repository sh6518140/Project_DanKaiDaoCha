using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Events;


public class VideoPlayManager : MonoBehaviour {

    //默认界面
    public GameObject DefaultTex;


    //要播放的视频
    public VideoClip[] m_Vclip;

//视频进度条 0:Min  1:Max
    public Slider[] VedioBar;
    public Slider[] AudioBar;

    public Toggle SwitchMode;
    //音量控制条

    //播放和暂停按钮
    //public Text PlayText;
    public Image[] PlayImage;

    //0 :播放  1：暂停
    public Sprite[] PlayIcon;

    //时间显示框
    public Text VedioTimer;

    //播放对应视频内容；
    public void PlayVedio(int i)
    {
        StopAllCoroutines();
        StartCoroutine(IE_PlayVedio(i));
        if (ChangePlayMode._instans.logo.Length == 2)
        {
            ChangePlayMode._instans.logo[0].SetActive(true);
        }
        else
        {
            ChangePlayMode._instans.logo[0].SetActive(true);
            ChangePlayMode._instans.logo[1].SetActive(true);
        }
    }

    public void PlayVedio1(int i)
    {
        Fuc_PlayVedio(i);
    }

    //播放暂停功能
    public void PlayOrPause()
    {
        if (++m_PlaySw % 2 == 1)
        {
            //StartCoroutine(IE_PlayVedio(m_Cclip));
            PlayVedio1(m_Cclip);
            ChangePlayIcon(1);
        }
        else
        {
            VedioInte.Pause();
            ChangePlayIcon(0);
        }
    }
    //结束视频播放
    public void StopAudio()
    {
        VedioTimer.text = "00:00/00:00";
        DefaultTex.SetActive(true);
        VedioInte.Stop();
        VedioBar[ChangePlayMode.ClickTimes % 2].value = 0;
        m_PlaySw = 0;
        ChangePlayIcon(0);
    }


    private void Awake()
    {
        //UnityactionEve += ToggleplaySwitch;
        //VedioBar.onValueChanged.AddListener(UnityactionEve);
    }

    void Start()
    {
        DefaultTex.SetActive(true);
        VedioInte = GetComponent<VideoPlayer>();
        AudioInte = GetComponent<AudioSource>();
    }
    void Update()
    {
        //视频进度条 暂停时拖动进度条
        if (VideoPointController.VideoPlaySwitch || VideoPointController.HandleisDown)
            SetCurrentPoint(VedioBar[ChangePlayMode.ClickTimes%2].value);
        else if (VedioInte.isPlaying)
            SetCurrentPlayPoint();

        SetVolumn(AudioBar[ChangePlayMode.ClickTimes % 2].value);
        ToggleplaySwitch(VedioBar[ChangePlayMode.ClickTimes%2].value);

    }

    private UnityAction<float> UnityactionEve;
    private VideoPlayer VedioInte;
    private AudioSource AudioInte;
    private int m_Cclip;
    private int m_PlaySw;

    private float TotalTime;
    private float CurrentTime;


    //刷新视频进度
    private void SetCurrentPlayPoint()
    {
        VedioBar[ChangePlayMode.ClickTimes%2].value = (float)GetCurrentPoint();
    }

    //获得视频进度
    private double GetCurrentPoint()
    {         
        double cp = 0;
        cp =VedioInte.time / m_Vclip[m_Cclip].length;
        ChangeFormat();
        return cp;
    }

    private void ChangeFormat()
    {
        int f = (int)m_Vclip[m_Cclip].length % 60;
        int s = (int)m_Vclip[m_Cclip].length / 60;
        int cf = (int)VedioInte.time % 60;
        int cs = (int)VedioInte.time / 60;
        VedioTimer.text = NumberFormat(cs)+":"+NumberFormat(cf)+"/"+NumberFormat(s)+":"+NumberFormat(f);
    }

    private string NumberFormat(int c)
    {
        if (c < 10)
            return "0" + c;
        else
            return "" + c;
    }

    //拖动视频进度
    private void SetCurrentPoint(float sp)
    { 
        VedioInte.time = m_Vclip[m_Cclip].length * sp;      
    }
    //拖动音量大小
    private void SetVolumn(float sp)
    {
        AudioInte.volume = sp;
        AudioBar[1 - ChangePlayMode.ClickTimes % 2].value = AudioBar[ChangePlayMode.ClickTimes % 2].value;
    }

    private void ToggleplaySwitch(float bv)
    {
        if (bv <= 0.995F || !VedioInte.isPlaying)
            return;
        if (SwitchMode.isOn)
                if (VideoPointController.HandleisDown)
                    return;
                else
                    PlayVedio(++m_Cclip);
            else
            {
                if (VideoPointController.HandleisDown)
                    return;
                else
                {
                    VedioInte.time = 0;
                    VedioBar[ChangePlayMode.ClickTimes % 2].value = 0;
                    VedioInte.Stop();
                    ChangePlayIcon(0);
                    m_PlaySw = 0;
                }
            }
    }

    IEnumerator IE_PlayVedio(int i)
    { 
        if (i >= m_Vclip.Length)
        {
            VedioInte.clip = m_Vclip[0];
            m_Cclip = 0;
            yield return new WaitForSeconds(0.01f);
            StopAudio();
             //DefaultTex.SetActive(true);
             //   VedioInte.Stop();
             //   VedioBar[ChangePlayMode.ClickTimes % 2].value = 0;
             //   m_PlaySw = 0;
             //   ChangePlayIcon(0);
        }
        else
        {
            DefaultTex.SetActive(false);
            VedioInte.clip = m_Vclip[i];
            VedioBar[ChangePlayMode.ClickTimes%2].value = 0;
            m_Cclip = i;
           ChangePlayIcon(1);
            m_PlaySw = 1;
            yield return new WaitForSeconds(0.01f);
            VedioInte.Play();
        }
    }

    private void Fuc_PlayVedio(int i)
    {
        if (i >= m_Vclip.Length)
        {
            DefaultTex.SetActive(true);
            VedioInte.clip = m_Vclip[0];
            m_Cclip = 0;
          ChangePlayIcon(0);
            m_PlaySw = 0;
            VedioBar[ChangePlayMode.ClickTimes % 2].value = 0;
            VedioInte.Stop();
        }
        else
        {
            DefaultTex.SetActive(false);
            VedioInte.clip = m_Vclip[i];
            VedioBar[ChangePlayMode.ClickTimes % 2].value = 0;
            m_Cclip = i;
           ChangePlayIcon(1);
            m_PlaySw = 1;
            VedioInte.Play();
        }
    }

    private void ChangePlayIcon(int ai)
    {
        for(int i = 0;i< PlayImage.Length;i++)
        {
            PlayImage[i].sprite = PlayIcon[ai];
        }
    }
}
