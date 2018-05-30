using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    //子弹预设
    public GameObject bullet;
    //子弹坐标
    public Transform firePos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }
}
