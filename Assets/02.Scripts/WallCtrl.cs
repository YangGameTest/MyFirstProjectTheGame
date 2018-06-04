using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour
{

    //表示火花粒子对象的变量
    public GameObject sparkEffect;

    //碰撞开始时触发的事件
    private void OnCollisionEnter(Collision collision)
    {
        //比较发生碰撞的游戏对象的Tag值
        if (collision.collider.tag == "BULLET")
        {
            #region//动态生成火花粒子
            /*//动态生成火花粒子
            Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            */
            #endregion

            //动态生成火花粒子并将其保存到变量
            GameObject spark = (GameObject)Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            //经过 ParticleSystem 组件的 duration 时间后删除
            Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.2f);

            //删除发生碰撞的游戏对象
            Destroy(collision.gameObject);
        }
    }

}
