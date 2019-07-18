using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsObjects : MonoBehaviour {

    public GameObject[] AllinsObjects;

    private void ShowOut()
    {
        foreach (var i in AllinsObjects)
        {
            i.SetActive(true);
        }
    }
    /// <summary>
    /// ////闪红
    /// </summary>

    public GameObject FFFFF;
    
    // 0:原色   1：红色
    public Material[] FlashRed;

    public void Flash_ChangeMaterial()
    {
        StartCoroutine(IE_Flash_ChangeMaterial());
    }

    IEnumerator IE_Flash_ChangeMaterial()
    {
        Renderer r = FFFFF.GetComponent<Renderer>();
        for (int i = 0; i < 3; i++)
        {
            r.material = FlashRed[1];
            yield return new WaitForSeconds(1f);
            r.material = FlashRed[0];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public GameObject YYYYYY;
    public Material[] FlashColor;
    public void Flash_ChangeYYY()
    {
        StartCoroutine(IE_Flash_ChangeYYY());
    }

    IEnumerator IE_Flash_ChangeYYY()
    {
        Renderer r = YYYYYY.GetComponent<Renderer>();
        for (int i = 0; i < 3; i++)
        {
            r.material = FlashColor[1];
            yield return new WaitForSeconds(1f);
            r.material = FlashColor[0];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public GameObject PeixianKong;
    // 0:原色   1：红色
    public Material[] FlashColor1;
    public void Flash_ChangePei()
    {
        StartCoroutine(IE_Flash_ChangePei());
    }

    IEnumerator IE_Flash_ChangePei()
    {
        Renderer r = PeixianKong.GetComponent<Renderer>();
        for (int i = 0; i < 3; i++)
        {
            r.material = FlashColor1[1];
            yield return new WaitForSeconds(1f);
            r.material = FlashColor1[0];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public GameObject PeixianKongKa;
    // 0:原色   1：红色
    public Material[] FlashColorka1;
    public void Flash_ChangePeika()
    {
        StartCoroutine(IE_Flash_ChangePeika());
    }

    IEnumerator IE_Flash_ChangePeika()
    {
        Renderer r = PeixianKongKa.GetComponent<Renderer>();
        for (int i = 0; i < 3; i++)
        {
            r.material = FlashColorka1[1];
            yield return new WaitForSeconds(1f);
            r.material = FlashColorka1[0];
            yield return new WaitForSeconds(0.5f);
        }
    }

    public GameObject[] PeixianKongKaXian;
    // 0:红色   1——后：原色
    public Material[] FlashColorkaxian1;
    public void Flash_ChangePeikaxian()
    {
        StartCoroutine(IE_Flash_ChangePeikaxian());
    }

    IEnumerator IE_Flash_ChangePeikaxian()
    {
        for (int i = 0; i < 3; i++)
        {
            foreach (var a in PeixianKongKaXian)
            {
                a.GetComponent<Renderer>().material = FlashColorkaxian1[0];
            }
            yield return new WaitForSeconds(1f);
            int k = 0;
            foreach (var a in PeixianKongKaXian)
            {
                a.GetComponent<Renderer>().material = FlashColorkaxian1[++k];
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    //左右螺丝旋转60

    public Transform Luosi1;
    public Transform Luosi2;
    private float XuanzhuanTime;
    private int kongzhicanshu;

    public void HowXuanzhuan(int i)
    {
        kongzhicanshu = i;
    }

    private void Xuanzhuan(int luosi)
    {
        if (luosi == 0)
        {
            XuanzhuanTime = 0;
            return;
        }
        else if(luosi == 1)
        {
            Luosi1.rotation = Luosi1.rotation * Quaternion.Euler(Vector3.up * 60/0.7f*Time.deltaTime);
        }
        else if (luosi == 2)
        {
            Luosi2.rotation = Luosi2.rotation * Quaternion.Euler(Vector3.up * 60 / 0.7f * Time.deltaTime);
        }

        XuanzhuanTime += Time.deltaTime;
        if(XuanzhuanTime >= 0.7f)
            kongzhicanshu = 0;
    }

    public ObjectRotate GuanNingzi;
    public void XuanzhunGuanNingzi()
    {
        GuanNingzi.RotateFuction();
    }


    public FlashColor[] dengzuo;
    public void shanghogn(int i)
    {
        dengzuo[i].FlashShow();
    }


    public ObjectRotate DianYaDangwei;
    public void Xuanzhuandangwei()
    {
        DianYaDangwei.RotateFuction();
    }

    public ObjectRotate Biaozhen;
    public void XuanzhuanBiaozhen()
    {
        Biaozhen.RotateFuction();
    }

    public void XuanzhuanBiaozhen1(int i)
    {
        Biaozhen.RotateFuction1(i);
    }

    // Use this for initialization
    void Start () {
        ShowOut();
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    XuanzhunGuanNingzi();
        //}

        Xuanzhuan(kongzhicanshu);
    }

    public Text da;
    public void fdsaf()
    {
        da.text = "3.检查信号机配线良好";
    }

}
