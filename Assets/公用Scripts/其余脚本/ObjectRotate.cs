using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour {

    public Vector3[] CurRotation;
    public Vector3 RotateDirection;
    public float RotateAngle;
    public float RotateTime;
    public float StopTime;
    public int RotateTimes;

    public void RotateFuction()
    {    
        RotateStart = 1;
    }

    public void RotateFuction1(int i)
    {
        transform.rotation = Quaternion.Euler(CurRotation[i]);
        RotateStart = 1;
    }

    public void SetRotation(int i)
    {
        transform.rotation = Quaternion.Euler(CurRotation[i]);
    }


    // Use this for initialization
    void Start()
    {
        PeriodTime = RotateTime + StopTime;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    RotateFuction();
        //}
        RotateProcess(RotateStart);
    }

    private int RotateStart;
    private float TimeKeeper;
    private float PeriodTime;

    private void RotateProcess(int i)
    {
        if (i == 0 || i > RotateTimes * 2)
        {
            RotateStart = 0;
            TimeKeeper = 0;
            return;
        }
        else
        { 
            if (i / 2 != i / 2.0)
            {
                transform.rotation = transform.rotation * Quaternion.Euler(RotateDirection * RotateAngle / RotateTime * Time.deltaTime);
                if (TimeKeeper >= (i + 1) * PeriodTime / 2 - StopTime)
                    RotateStart++;
            }
            else
                if (TimeKeeper >= i * PeriodTime / 2)
                    RotateStart++;
        }
        TimeKeeper += Time.deltaTime;              
    }
}
