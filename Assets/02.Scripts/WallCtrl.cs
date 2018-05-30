using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour {

    //碰撞开始时触发的事件
    private void OnCollisionEnter(Collision collision)
    {
        //比较发生碰撞的游戏对象的Tag值
        if (collision.collider.tag == "BULLET")
        {
            //删除发生碰撞的游戏对象
            Destroy(collision.gameObject);
        }
    }
}
