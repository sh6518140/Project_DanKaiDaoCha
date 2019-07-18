using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HighlightingSystem;

public class IntroduceTools : MonoBehaviour
{
    public CameraKongZhi cameraKongZhi = CameraKongZhi._instance;

    public Text toolNameText;
    public Text introduceToolText;
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public static string[] toolsIntroduceArr = new string[] {
        "使用时，任意一个直尺都能够量取长度，当需要画角时，让没有量角器刻度的直尺绕铆钉旋转，使其一条边对准所要画的角度，便能够确定角的两条边，从而方便快捷的画出所需的角。将直尺与量角器有机结合在一起，不仅使用方便，而且减少了占用空间，易于存放。",
        "钢尺的基本分划为厘米，在每米及每分米处都有数字注记，适用于一般的距离测量。有的钢尺在起点处至第一个10cm间，甚至整个尺长内都刻有毫米分划，这种钢尺适用于精密距离测量。",
        "由一组具有不同厚度级差的薄钢片组成的量规。塞尺用于测量间隙尺寸。在检验被测尺寸是否合格时，可以用通止法判断，也可由检验者根据塞尺与被测表面配合的松紧程度来判断。",
        "其结构由尺身、标度尺及尺身上的游框组成，尺身为双工形型材结构，呈方管形，在型材上下两边外沿带有向外延伸，在外沿内侧开有槽口，将其不锈钢标度尺插入在槽口内，然后在其端头用销钉固定，尺身中间装有游框，游框上固定有副尺，侧板，侧板上固定有调整螺栓，尺身端头装有十字板和测头，整个支距尺呈丁字形结构。",
        "线路曲线正是检测高低通常需要三人一组进行，使用此型曲线正矢测量盒仅需一人就可以完成这一工作。曲线正矢测量盒制作精细，小巧，轻便、美观，盒体为乳白色耐磨型ABS为原料，且操作方便。曲线正矢测量盒的前端置放两块边长为20*20mm;20*30mm的长方块强 力磁铁。",
        "铁路检验锤检车锤检查,主要是检查连接件是否有松动,就是敲螺丝,听声音看它松没松,再就是检查机件有没有失效,比如说敲钢板弹簧,哪一片断了或者有裂纹,一敲就知道。除了声音，敲击故障部件手感也不一样。12mm以下的螺丝、非钢质和压力部件不能敲。",
        "记录在检查过程中出现的问题及特殊情况。",
        "从轨距尺的测量实现方式上讲，轨距尺的功能分为2大部分：一是横向长度测量，包括轨距、查照间隔和护背距离，从结构原理及量值溯源的角度分析，这3个参数是相互关联和相互制约的，按现行量值传递方法，测量误差从轨距到查照间隔、再到护背距离是逐渐增大的。",
        "卷尺主要由由外壳、尺条、制动、尺钩、提带、尺簧、防摔保护套和贴标八个部件构成。",
        "包括垂磨尺、侧磨尺、定位块所组成。在主座的左端水平设置有侧磨尺，主座的水平面上垂直设置有垂磨尺，主座的右下方设置有定位块。本实用新型属于机械式的刻度读数检测测量。结构简单、使用方便、使用寿命长",
        "拔出固定杆，可以旋转各标高板，用于测量标高。" };

    public Highlighter muzhechi;
    public Highlighter gangchi;
    public Highlighter saichi;
    public Highlighter zhijuchi;
    public Highlighter quxianzhenshihe;
    public Highlighter jianchachui;
    public Highlighter jilubo;
    public Highlighter guijuchi;
    public Highlighter gangjuanchi;
    public Highlighter gangguimohaojianceyi;
    public Highlighter gaoduban;

    public GameObject[] allGameobject;

    public GameObject muZhiChi;
    public GameObject gangChi;
    public GameObject saiChi;
    public GameObject zhiJuChi;
    public GameObject quXianZhenShiHe;
    public GameObject jianChaChui;
    public GameObject jiLuBo;
    public GameObject guiJuChi;
    public GameObject gangJuanChi;
    public GameObject gangGuiMoHaoJianCeYi;
    public GameObject gaoDuBan;

    public Camera mainCamera;

    public Button returnBackButton;

    public Image muzhechiImage;
    public Image gangchiImage;
    public Image saichiImage;
    public Image zhijuchiImage;
    public Image quxianheImage;
    public Image jianchachuiImage;
    public Image jiluboImage;
    public Image guijuchiImage;
    public Image gangjuanchiImage;
    public Image gangguijianceImage;
    public Image gaodubanImage;

    public Image[] allUIArr;

    void Awake()
    {
        returnBackButton.gameObject.SetActive(false);
        returnBackButton.onClick.AddListener(OnReturnBackClick);
        for (int i = 0; i < allUIArr.Length; i++)
        {
            allUIArr[i].gameObject.SetActive(false);
        }
    }


    void Start()
    {
        CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/水泥台子").transform;
    }


    void Update()
    {
        



        if (mainCamera.clearFlags==CameraClearFlags.Color)
        {
            for (int i = 0; i < allUIArr.Length; i++)
            {
                allUIArr[i].gameObject.SetActive(false);
            }
        }
        else
        {
            ShowUI();
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Tools"))
            {
                toolNameText.text = hit.collider.name;

                if (hit.collider.name == "水泥台子")
                {
                    toolNameText.text = "";
                }

                    for (int i = 0; i < audioClip.Length; i++)
                {
                    switch (hit.collider.name)
                    {
                        case "木折尺": audioSource.clip = audioClip[0]; introduceToolText.text = toolsIntroduceArr[0]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/木折尺").transform; muzhechi.FlashingOn(Color.red, Color.green, 1); gangchi.FlashingOff();saichi.FlashingOff();zhijuchi.FlashingOff();quxianzhenshihe.FlashingOff();jianchachui.FlashingOff();jilubo.FlashingOff();guijuchi.FlashingOff();gangjuanchi.FlashingOff();gangguimohaojianceyi.FlashingOff();gaoduban.FlashingOff(); MuZheChiSetActiveTrue(); returnBackButton.gameObject.SetActive(true); break;
                        case "钢尺": audioSource.clip = audioClip[1]; introduceToolText.text = toolsIntroduceArr[1]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/钢尺").transform; gangchi.FlashingOn(Color.red, Color.green, 1);muzhechi.FlashingOff();saichi.FlashingOff();zhijuchi.FlashingOff();quxianzhenshihe.FlashingOff();jianchachui.FlashingOff();jilubo.FlashingOff();guijuchi.FlashingOff(); gangjuanchi.FlashingOff();gangguimohaojianceyi.FlashingOff();gaoduban.FlashingOff();GangChiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "塞尺": audioSource.clip = audioClip[2]; introduceToolText.text = toolsIntroduceArr[2]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/塞尺").transform; saichi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();SaiChiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "支距尺": audioSource.clip = audioClip[3]; introduceToolText.text = toolsIntroduceArr[3]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/支距尺").transform; zhijuchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();ZhiJuChiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "曲线正矢盒": audioSource.clip = audioClip[4]; introduceToolText.text = toolsIntroduceArr[4]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/曲线正矢盒").transform; quxianzhenshihe.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); QuXianHeSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "检查锤": audioSource.clip = audioClip[5]; introduceToolText.text = toolsIntroduceArr[5]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/检查锤").transform; jianchachui.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); JianChaChuiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "记录簿": audioSource.clip = audioClip[6]; introduceToolText.text = toolsIntroduceArr[6]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/记录簿").transform; jilubo.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();JiLuBoSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "轨距尺": audioSource.clip = audioClip[7]; introduceToolText.text = toolsIntroduceArr[7]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/轨距尺").transform; guijuchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();GuiJuChiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "钢卷尺": audioSource.clip = audioClip[8]; introduceToolText.text = toolsIntroduceArr[8]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/钢卷尺").transform; gangjuanchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();GangJuanChiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "钢轨磨耗检测仪": audioSource.clip = audioClip[9]; introduceToolText.text = toolsIntroduceArr[9]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/钢轨磨耗检测仪").transform; gangguimohaojianceyi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gaoduban.FlashingOff();GangGuiJianCeYiSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "高度板": audioSource.clip = audioClip[10]; introduceToolText.text = toolsIntroduceArr[10]; CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/高度板").transform; gaoduban.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff();GaoDuBanSetActiveTrue(); returnBackButton.gameObject.SetActive(true);  break;
                        case "水泥台子": CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/水泥台子").transform;
                            break;
                        default:
                            audioSource.Stop(); 
                            break;
                    }
                    audioSource.Play();

                }
            }
        }

        if (audioSource.clip==audioClip[22])
        {
            audioSource.Stop();
        }

    }
    


    void MuZheChiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            muZhiChi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void GangChiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            gangChi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void SaiChiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            saiChi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void ZhiJuChiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            zhiJuChi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void QuXianHeSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            quXianZhenShiHe.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void JianChaChuiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            jianChaChui.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void JiLuBoSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            jiLuBo.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void GuiJuChiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            guiJuChi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void GangJuanChiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            gangJuanChi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void GangGuiJianCeYiSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            gangGuiMoHaoJianCeYi.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    void GaoDuBanSetActiveTrue()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(false);
            gaoDuBan.SetActive(true);
            mainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
    }

    //返回按钮
    void OnReturnBackClick()
    {
        for (int i = 0; i < allGameobject.Length; i++)
        {
            allGameobject[i].SetActive(true);
        }
        audioSource.Stop();
        CameraKongZhi._instance.targetSelf = GameObject.Find("Tools/水泥台子").transform;
        mainCamera.transform.position = new Vector3(-25.133f,4.08017f,1.948706f);
        //mainCamera.transform.rotation =  Quaternion.Euler(86.37f,0f,0f);
        //mainCamera.transform.rotation = Quaternion.Euler(86.37f, 0f, 0f);

        mainCamera.GetComponent<CameraKongZhi>().vCamRotation = new Vector3(93.62f, -0.2749996f, 180f);

        mainCamera.GetComponent<Camera>().fieldOfView = 45;

        mainCamera.clearFlags = CameraClearFlags.Skybox;

        returnBackButton.gameObject.SetActive(false);



    }

    void ShowUI()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Tools"))
            {
                toolNameText.text = hit.collider.name;

                if (hit.collider.name == "水泥台子")
                {
                    toolNameText.text = "";
                    muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();
                    for (int i = 0; i < allUIArr.Length; i++)
                    {
                        allUIArr[i].gameObject.SetActive(false);
                    }
                    
                    audioSource.clip = audioClip[22];
                }

                if (audioSource.clip.name!=hit.transform.name)
                {
                    for (int i = 0; i < allUIArr.Length; i++)
                    {
                        

                        switch (hit.collider.name)
                        {
                            case "木折尺": muzhechi.FlashingOn(Color.red, Color.green, 1); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); muzhechiImage.gameObject.SetActive(true); audioSource.clip = audioClip[11]; break;
                            case "钢尺": gangchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); gangchiImage.gameObject.SetActive(true); audioSource.clip = audioClip[12]; break;
                            case "塞尺": saichi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); saichiImage.gameObject.SetActive(true); audioSource.clip = audioClip[13]; break;
                            case "支距尺": zhijuchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); zhijuchiImage.gameObject.SetActive(true); audioSource.clip = audioClip[14]; break;
                            case "曲线正矢盒": quxianzhenshihe.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); quxianheImage.gameObject.SetActive(true); audioSource.clip = audioClip[15]; break;
                            case "检查锤": jianchachui.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); jianchachuiImage.gameObject.SetActive(true); audioSource.clip = audioClip[16]; break;
                            case "记录簿": jilubo.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); jiluboImage.gameObject.SetActive(true); audioSource.clip = audioClip[17]; break;
                            case "轨距尺": guijuchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); guijuchiImage.gameObject.SetActive(true); audioSource.clip = audioClip[18]; break;
                            case "钢卷尺": gangjuanchi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); gangjuanchiImage.gameObject.SetActive(true); audioSource.clip = audioClip[19]; break;
                            case "钢轨磨耗检测仪": gangguimohaojianceyi.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gaoduban.FlashingOff(); allUIArr[i].gameObject.SetActive(false); gangguijianceImage.gameObject.SetActive(true); audioSource.clip = audioClip[20]; break;
                            case "高度板": gaoduban.FlashingOn(Color.red, Color.green, 1); muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); allUIArr[i].gameObject.SetActive(false); gaodubanImage.gameObject.SetActive(true); audioSource.clip = audioClip[21]; break;
                            
                            default:
                                muzhechi.FlashingOff(); gangchi.FlashingOff(); saichi.FlashingOff(); zhijuchi.FlashingOff(); quxianzhenshihe.FlashingOff(); jianchachui.FlashingOff(); jilubo.FlashingOff(); guijuchi.FlashingOff(); gangjuanchi.FlashingOff(); gangguimohaojianceyi.FlashingOff(); gaoduban.FlashingOff();
                                audioSource.Stop();
                                break;
                        }
                        audioSource.Play();

                    }
                   
                }
            }
        }
    }
    

}
