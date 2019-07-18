	//------------------------------
//  MenuUI : 摄像机控制模块
//  Editor ：jhj
//  Data : 2016-5-25

//------------------------------
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public enum CAMERA_TYPE
{
    FOLLOW = 0,
    AROUND = 1
}

public class CameraKongZhi: MonoBehaviour {

    public static CameraKongZhi _instance;
    
    //ZoomUpdate函数中改缩放的值

    public Transform targetSelf;                    // 跟随目标//
	public Camera mCamera;                         //

    //限制y轴
    public bool bCanRotatDown = true;               //是否可以旋转视角到底面  

    public float bDampingLook = 0.2f;               //缓动look  

    public float distance = 5f;                     //距离//
    private float fAngle = 25f;                     //
    private float heightDamping = 2.5f;             //
    private float rotationDamping = 3.0f;           //
    public float fixHeight = 0f;		            //

    private CAMERA_TYPE eCameraType = CAMERA_TYPE.AROUND;		//摄像机跟随方式 //
    public Vector3 vCamRotation = new Vector3(160f, 100f, 180f);//旋转摄像机角度 //
    public Vector3 vTargetOffset = Vector3.zero;     //目标偏移

    public GameObject objArrayAll;                  //
    private RaycastHit hit;                         //
    private Ray ray;                                //

    public bool bCameraOffset = false;             //摄像机平移
    private bool bRightPress;                       //鼠标右键 
    private bool bLeftPress;                        //鼠标左键
    private float fRotatSpeed = 1.5f;                //旋转速度
    private float fMoveSpeed = 0.1f;                //平移速度   
    private CElement mSelectElement;                //选择的组件   

    [HideInInspector]
    public  bool bLock = false;                  //可以移动

	[HideInInspector]
	public float fRotateAngle = 360;			//限制旋转角度


	public Camera _camera;

    private void Awake()
    {
        _instance = this;
    }


    void Start () { 

        mCamera = this.GetComponent<Camera>();//.eventMask = 1 << 5;
                                              //print (GetComponent<Camera> ().eventMask);   
    }


    /// <summary>
    /// 设置拍摄目标
    /// </summary>
    /// <param name="_trans"></param>
    public void SetTarget(Transform _trans)
    {
        targetSelf = _trans;
    }



    void Update()
    {
        OnMouseAction(); 
    }

    
    void LateUpdate () {

        if (!targetSelf)
            return;

        switch (eCameraType)
        {
            default:
            case CAMERA_TYPE.FOLLOW:
                FollowTarget();
                break;
            case CAMERA_TYPE.AROUND:
                ARoundTarget();
                break;
        } 
        ZoomUpdate();
    }




    /// <summary>
	///  处理鼠标行为事件
	/// </summary>
	void OnMouseAction()
    {

        if (bLock)
            return;


     
//        #region 平移 视角
//        if (Input.GetMouseButtonDown(2))
//        {
//            bCameraOffset = true;
//        }
//        if (Input.GetMouseButtonUp(2))
//        {
//            bCameraOffset = false;
//        }
//
//        if (bCameraOffset)
//        {
//            vTargetOffset = new Vector3(
//            vTargetOffset.x + ((Input.GetAxis("Mouse X") * fMoveSpeed)),
//            vTargetOffset.y + ((Input.GetAxis("Mouse Y") * fMoveSpeed)),
//            0
//            );
//
//            //Vector3 vOffset = new Vector3(Input.GetAxis("Mouse X") * fMoveSpeed, 0, Input.GetAxis("Mouse Y") * fMoveSpeed); 
//            //this.transform.Translate(this.transform.position + Vector3.left); 
//            //Debug.Log(this.transform.position);
//
//            return;
//        }
//        #endregion


        #region 移动视角

        if (mSelectElement == null)
        {
			//!EventSystem.current.IsPointerOverGameObject()是否点在UI上
			//
//
//            if (Input.GetMouseButtonDown(1) && GUIUtility.hotControl == 0 && !EventSystem.current.IsPointerOverGameObject())
//            {
//                bRightPress = true;
//            }
			if (Input.GetMouseButtonDown(1))
			{
				bRightPress = true;
				Screen.lockCursor = false;
			}

            if (Input.GetMouseButtonUp(1))
            {
               bRightPress = false;
            }
            if (bRightPress)
            {  

                float fCamRotaY;
                if (bCanRotatDown)
                {
					//Mathf.Clamp 钳制.限制value的值在min和max之间， 如果value小于min，返回min。 如果value大于max，返回max，否则返回value
					fCamRotaY = Mathf.Clamp(vCamRotation.x + (Input.GetAxis("Mouse Y") * fRotatSpeed), 90f, 180);

                }
                else
                {
                    fCamRotaY = vCamRotation.x + (Input.GetAxis("Mouse Y") * fRotatSpeed);
                } 

                vCamRotation = new Vector3(
                    fCamRotaY,
                    vCamRotation.y + (Input.GetAxis("Mouse X") * fRotatSpeed),
                    180
                );

            }
        }
        #endregion


        /// 

        if (Input.GetMouseButtonUp(0))
        {
            bLeftPress = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (GUIUtility.hotControl == 0)
            {
				ray = mCamera.ScreenPointToRay(Input.mousePosition);
                  
                // 基本选择物体   
                if (Physics.Raycast(ray, out hit))
                { 
                    CElement mElement = hit.collider.gameObject.GetComponent<CElement>();
                    if (mElement != null)
                    {   // 选中 
                        ClearSelectElement();
                        mSelectElement = mElement;
                        mSelectElement.ShowHighLight();

                        if (objArrayAll)
                        {
                            objArrayAll.transform.position = mSelectElement.transform.position;
							float fScale =mCamera.fieldOfView * 0.01f;
                            objArrayAll.transform.localScale = new Vector3(fScale, fScale, fScale);
                            objArrayAll.gameObject.SetActive(true);
                        }
                        else
                        {
                            //分解 , 结合操作 
                        }
                    }
                }
                else
                {
                    ClearSelectElement();  //没有选择
                    return;
                }

            }
        }
    }



    /// <summary>
    /// 
    /// </summary>
    private void ZoomUpdate()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
			if (mCamera.fieldOfView <= 75)
				mCamera.fieldOfView += 2;
			if (mCamera.orthographicSize <= 20)
				mCamera.orthographicSize += 0.5F;


        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
			if (mCamera.fieldOfView > 15)
				mCamera.fieldOfView -= 2;
			if (mCamera.orthographicSize >= 1)
				mCamera.orthographicSize -= 0.5F;


        }


		float fScale = mCamera.fieldOfView * 0.01f;
        if (objArrayAll != null)
            objArrayAll.transform.localScale = new Vector3(fScale, fScale, fScale);

    }

     

     

    /// <summary>
    /// 清除选择
    /// </summary>
    private void ClearSelectElement()
    {
        if (null != mSelectElement)
            mSelectElement.HideHighLight();

        mSelectElement = null;
        if (objArrayAll)
            objArrayAll.gameObject.SetActive(false);

    }



    /// <summary>
    /// 设置旋转
    /// </summary>
    /// <param name="vRotate"></param>
    public void SetRotate(Vector3 vRotate)
    {
        vCamRotation = vRotate;
    }  


    /// <summary>
    /// 360度围绕目标 As the round target.
    /// </summary>
    private void ARoundTarget()
    {
        Vector3 tarPos = new Vector3(targetSelf.position.x + vTargetOffset.x, targetSelf.position.y + fixHeight + vTargetOffset.y, targetSelf.position.z + vTargetOffset.z);

        //if (bDampingLook > 0)
        //{
        //    //缓动
        //    Vector3 vToPos = new Vector3(targetSelf.position.x + vTargetOffset.x, targetSelf.position.y + fixHeight + vTargetOffset.y, targetSelf.position.z + vTargetOffset.z);
        //    tarPos = Vector3.Lerp(vToPos, transform.position, 0.1f);
            
        //}
        //else
        //{   //直接定位
        //    tarPos = new Vector3(targetSelf.position.x + vTargetOffset.x, targetSelf.position.y + fixHeight + vTargetOffset.y, targetSelf.position.z + vTargetOffset.z);
        //}

        Quaternion rotation = Quaternion.Euler(vCamRotation);
        Vector3 position = (rotation * (new Vector3(0, 0, -distance))) + tarPos;

        transform.rotation = rotation;
        transform.position = position;
    }


    /// <summary>
    /// 跟随目标 Follows the target.
    /// </summary>
    private void FollowTarget()
    {
        
        // Calculate the current rotation angles
        float wantedRotationAngle = targetSelf.eulerAngles.y;
        float wantedRotationAngleX = targetSelf.eulerAngles.x + fAngle;
        float wantedHeight = targetSelf.position.y + fixHeight;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentForwardAngle = transform.eulerAngles.x;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the rotation around the x-axis
        currentForwardAngle = Mathf.LerpAngle(currentForwardAngle, wantedRotationAngleX, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(currentForwardAngle, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        //transform.position = target.position;
        Vector3 vTar = new Vector3(targetSelf.position.x, targetSelf.position.y + fixHeight, targetSelf.position.z);
        transform.position = vTar;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.LookAt(vTar);

    }


}
