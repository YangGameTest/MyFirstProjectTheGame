using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    //子弹破坏力
    public int damage = 20;
    //子弹发射速度
    public float speed = 1000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    void Update()
    {

    }

}
