using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{

    public float RedmatTime;
    public float IntmatTime;
    public int FlashTimes;

    public void FlashShow()
    {
        FlashStart = 1;

        for (int i = 0; i < InsRenderer.Length; i++)
        {
            midmate[i] = Instantiate(InsMate[i]) as Material;
            midmate[i].SetColor("_Color", Color.red);
            midmate[i].SetColor("_EmissionColor", Color.red);
        }
    }
    void Start()
    {
        InsRenderer = GetComponentsInChildren<Renderer>();
        InsMate = new Material[InsRenderer.Length];
        midmate = new Material[InsRenderer.Length];
        for (int i = 0; i < InsRenderer.Length; i++)
        {
            InsMate[i] = InsRenderer[i].material;
        }
        PeriodTime = RedmatTime + IntmatTime;
        TimeKeeper = 0;
    }

    // Update is called once per frame
    void Update()
    {
        FlashProcess(FlashStart);
    }

    private Renderer[] InsRenderer;
    private Material[] InsMate;
    private Material[] midmate;
    private int FlashStart;
    private float PeriodTime;
    private float TimeKeeper;

    private void FlashProcess(int i)
    {
        if (i == 0 || i > FlashTimes * 2)
        {
            for (int j = 0; j < InsRenderer.Length; j++)
            {
                InsRenderer[j].material = InsMate[j];
                Destroy(midmate[j]);
                midmate[j] = null;
            }
            i = 0;
            TimeKeeper = 0;
            return;
        }
        else
        {
            if (TimeKeeper >= (i - 1) * PeriodTime / 2 && i / 2 != i / 2.0)
            {
                FlashStart++;
                for (int j = 0; j < InsRenderer.Length; j++)
                {
                    InsRenderer[j].material = midmate[j];
                }
            }
            else if (TimeKeeper >= (i * PeriodTime / 2 - IntmatTime) && i / 2 == i / 2.0)
            {
                FlashStart++;
                for (int j = 0; j < InsRenderer.Length; j++)
                {
                    InsRenderer[j].material = InsMate[j];
                }
            }
        }
        TimeKeeper += Time.deltaTime;
    }

    public void FlashStaats()
    {
        FlashStart = 0;
    }

}
