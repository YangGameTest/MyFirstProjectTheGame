using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform targetTr;  //要追踪的游戏对象的Transform变量
    public float dist = 10.0f;  //与摄像机之间的距离
    public float height = 3.0f;  //设置摄像机高度
    public float dempTrace = 20.0f;  //实现平滑追踪的变量


    //public float velocity = 0.0f;
    //public float smoothTime_x;
    //public float smoothTime_y;
    //public float smoothTime_z;
    //
    //public float dic_y;
    //public float dic_z;

    //摄像机本身的Transform变量
    private Transform tr;

    void Start()
    {
        //将摄像机本身的Transform组件分配至tr
        tr = GetComponent<Transform>();
    }

    /// <summary>
    /// 使用LateUpdate函数，调用所有Update函数后才会调用该函数。
    /// 要追踪的目标游戏对象停止移动后，调用LateUpdate函数。
    /// </summary>
    void LateUpdate()
    {
       tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dempTrace);
        //                           起始位置   结束位置                                                               内插时间

        //将摄像机放置在被追踪目标后方的dist距离的位置。
        //将摄像机向上抬高height。 
        //Vector3 targetVec = new Vector3(Mathf.SmoothDamp(transform.position.x, targetTr.position.x, ref velocity, smoothTime_x),
        //Mathf.SmoothDamp(transform.position.y, targetTr.position.y + dic_y, ref velocity, smoothTime_y),
        //Mathf.SmoothDamp(transform.position.z, targetTr.position.z - dic_z, ref velocity, smoothTime_z));
        //transform.position = targetVec;


       

        //float angle = Vector3.Angle(transform.forward, targetTr.forward);
        //Vector3 dir = Vector3.Cross(transform.forward, targetTr.forward);

        //Debug.Log("夹角："+angle);
       
        //if (dir.z > 0)
        //{
        //    tr.Rotate(targetTr.up, angle);
        //    Debug.Log("方向：" + dir);
        //}
        //else if (dir.z < 0)
        //{
        //    tr.Rotate(targetTr.up, angle * -1);
        //}
        //使摄像机朝向游戏对象
        tr.LookAt(targetTr.position);
    }

}
