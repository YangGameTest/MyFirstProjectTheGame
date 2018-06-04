using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //表示爆炸效果的变量
    public GameObject expEffect;
    //要随机选择的纹理数组
    public Texture[] textures;

    private Transform tr;

    //保存被子弹击中次数的变量
    private int hitCount = 0;


    private void Start()
    {
        tr = GetComponent<Transform>();

        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BULLET")
        {
            //删除子弹
            Destroy(collision.gameObject);

            //累加油桶被子弹击中的次数，3次以上则触发爆炸
            if (++hitCount >= 3)
            {
                ExpBarrel();
            }
        }

    }

    void ExpBarrel()
    {
        //生成爆炸效果粒子
        Instantiate(expEffect, tr.position, Quaternion.identity);

        //以指定原点为中心，获取半径10.0f内的Collider对象
        Collider[] colls = Physics.OverlapSphere(tr.position, 10.0f);

        //对获取的Collider对象施加爆炸力
        foreach (Collider collision in colls)
        {
            Rigidbody rbody = collision.GetComponent<Rigidbody>();
            if (rbody != null)
            {
                rbody.mass = 1.0f;
                rbody.AddExplosionForce(1000.0f, tr.position, 10.0f, 300.0f);
            }
        }

        //3秒后删除油桶模型
        Destroy(gameObject, 3.0f);

    }

}
