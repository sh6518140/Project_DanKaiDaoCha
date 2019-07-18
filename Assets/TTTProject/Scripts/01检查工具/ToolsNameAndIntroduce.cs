using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HighlightingSystem;

public class ToolsNameAndIntroduce : MonoBehaviour
{
    public static string[] toolsNameArr = new string[] { "木折尺", "钢尺", "塞尺", "支距尺", "曲线正矢盒", "检查锤", "记录薄", "轨距尺", "钢卷尺", "钢轨磨耗检测仪", "高度板" };

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
        "包括垂磨尺、侧磨尺、定位块所组成。在主座的左端水平设置有侧磨尺，主座的水平面上垂直设置有垂磨尺，主座的右下方设置有定位块。本实用新型属于机械式的刻度读数检测测量。结构简单、使用方便、使用寿命长。",
        "拔出固定杆，可以旋转各标高板，用于测量标高。" };


    public Text toolsNameText;
    public Text toolsIntroduceText;
    public Text toolsCountText;
    public Button leftToolButton;
    public Button rightToolButton;
    public int j = 0;

    public Highlighter[] flicker = new Highlighter[11] ;

    void Awake()
    {
        
        leftToolButton.onClick.AddListener(() => { OnLeftToolButtonClick(); });
        rightToolButton.onClick.AddListener(() => { OnRightToolButtonClick(); });

        for (int i = 0; i < flicker.Length; i++)
        {
            flicker[i].GetComponent<Highlighter>();
        }

    }

    void Update()
    {
        ShowToolName();
    }

    void OnLeftToolButtonClick()
    {
        j -= 1;
        if (j <= 0)
        {
            j = 0;
        }
    }

    void OnRightToolButtonClick()
    {
        j += 1;
        if (j >= 10)
        {
            j = 10;
        }
    }

    void ShowToolName()
    {
        for (int i = 0; i < toolsNameArr.Length; i++)
        {
            if (j == i)
            {
                toolsNameText.text = toolsNameArr[i];
                toolsIntroduceText.text = toolsIntroduceArr[i];
                toolsCountText.text = "     "+(i+1) + "/11";
                
            } 
        }

        for (int k = 0; k < flicker.Length; k++)
        {
            if (j==k)
            {
                flicker[k].FlashingOn(Color.red, Color.green, 1);
            }
            else
            {
                flicker[k].FlashingOff();
            }
            
        }
    }
    
}
