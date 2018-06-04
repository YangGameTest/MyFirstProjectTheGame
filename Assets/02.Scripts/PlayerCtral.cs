using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//此类需要声明属性System.Serializable，表示可序列化。
//显示到检视视图
[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}


public class PlayerCtral : MonoBehaviour
{

    private float h = 0.0f;
    private float v = 0.0f;

    //必须先分配变量，之后才能使用需要访问的组件
    private Transform tr;

    //移动速度变量
    public float moveSpeed = 10.0f;

    //旋转速度变量
    public float rotSpeed = 100.0f;

    //要显示到检视视图的动画类变量
    public Anim anim;

    //要访问下列3D模型Animation组件对象的变量
    public Animation _animation;

     void Start()
    {
        //向脚本初始部分分配Transform组件
        tr = GetComponent<Transform>();

        #region
        //tr = this.gameObject.GetComponent<Transform>();
        ////从该脚本包含的游戏对象拥有的各组件中抽取Transform组件，并保存至tr变量。

        //float vec1 = Vector3.Magnitude(Vector3.forward);
        //float vec2 = Vector3.Magnitude(Vector3.forward + Vector3.right);
        //float vec3 = Vector3.Magnitude((Vector3.forward + Vector3.right).normalized);

        //Debug.Log(vec1);
        //Debug.Log(vec2);
        //Debug.Log(vec3);
        #endregion

        //查找位于自身下级的Animation组件并分配到变量
        _animation = GetComponentInChildren<Animation>();

        //保存并运行Animation组件的动画片段。
        _animation.clip = anim.idle;
        _animation.Play();
    }


     void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Debug.Log("H=" + h.ToString());
        Debug.Log("V=" + v.ToString());

        //计算前后左右移动方向向量
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //Translate(移动方向*速度*位移值*Time.deltaTime,基础坐标)
        tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        //tr.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime, Space.Self);
        //tr.Translate(Vector3.right * moveSpeed * h * Time.deltaTime, Space.Self);

        //以Vector3.up轴为基准，以rotSpeed速度旋转
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));

        //以键盘输入的值为基准，执行要操作的动画
        if (v >= 0.1f)
        {
            //前进动画
            _animation.CrossFade(anim.runForward.name, 0.1f);
        }
        else if (v <= -0.1f)
        {
            //后退动画
            _animation.CrossFade(anim.runBackward.name, 0.1f);
        }
        else if (h >= 0.1f)
        {
            //向左移动动画
            _animation.CrossFade(anim.runRight.name, 0.1f);
        }
        else if (h <= -0.1f)
        {
            //想右移动动画
            _animation.CrossFade(anim.runLeft.name, 0.1f);
        }
        else
        {
            //暂停时执行idle动画
            _animation.CrossFade(anim.idle.name, 0.1f);
        }

    }

}