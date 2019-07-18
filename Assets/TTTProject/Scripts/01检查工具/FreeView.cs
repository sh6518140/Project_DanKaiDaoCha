using UnityEngine;
using System.Collections;

public class FreeView : MonoBehaviour
{
    //观察目标  
    public Transform Target;
    //观察距离  
    public float Distance = 5F;
    //旋转速度  
    public float SpeedX = 240;
    public float SpeedY = 120;

    //角度限制  
    public float MinLimitY = -180;
    public float MaxLimitY = 180;

    //旋转角度  
    public float mX = 0.0F;
    public float mY = 0.0F;

    //鼠标缩放距离最值  
    public float MaxDistance = 10;
    public float MinDistance = 1.5F;
    //鼠标缩放速率  
    public float ZoomSpeed = 2F;

    //是否启用差值  
    public bool isNeedDamping = true;
    //速度  
    public float Damping = 10F;

    //存储角度的四元数  
    private Quaternion mRotation;

    //定义鼠标按键枚举  
    private enum MouseButton
    {
        //鼠标左键  
        MouseButton_Left = 0,
        //鼠标右键  
        MouseButton_Right = 1,
        //鼠标中键  
        MouseButton_Midle = 2
    }

    //相机移动速度  
    private float MoveSpeed = 5.0F;
    //屏幕坐标  
    private Vector3 mScreenPoint;
    //坐标偏移  
    private Vector3 mOffset;

    void Start()
    {
        //初始化旋转角度  
        mX = transform.eulerAngles.x;
        mY = transform.eulerAngles.y;
    }

    void LateUpdate()
    {
        //鼠标右键旋转  
        if (Target != null && Input.GetMouseButton((int)MouseButton.MouseButton_Right))
        {
            //获取鼠标输入  
            mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
            mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;
            //范围限制  
            mY = ClampAngle(mY, MinLimitY, MaxLimitY);
            //计算旋转  
            mRotation = Quaternion.Euler(mY, mX, 0);
            //根据是否插值采取不同的角度计算方式  
            if (isNeedDamping)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, mRotation, Time.deltaTime * Damping);
            }
            else
            {
                transform.rotation = mRotation;
            }
        }

        //鼠标中键平移  


        //鼠标滚轮缩放  
        Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);

        //重新计算位置  
        Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + Target.position;

        //设置相机的位置  
        if (isNeedDamping)
        {
            transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * Damping);
        }
        else
        {
            transform.position = mPosition;
        }

    }


    //角度限制  
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
