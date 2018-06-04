using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//声音脚本需要的组件，以防该组件被删除
[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    //子弹预设
    public GameObject bullet;
    //子弹坐标
    public Transform firePos;

    //子弹发射声音
    public AudioClip fireSfx;
    //保存AudioSource组件的变量
    private AudioSource source = null;
    //连接MuzzleFlash的MeshRenderer组件
    public MeshRenderer muzzleFlash;

   
    void Start()
    {
        //获取AudioSource组件后分配到变量
        source = GetComponent<AudioSource>();
        //禁用MuzzleFlash MeshRenderer
        muzzleFlash.enabled = false;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            //播放声音的函数
            source.PlayOneShot(fireSfx, 1f);
            //使用例程调用处理MuzzleFlash效果的函数
            StartCoroutine(this.ShowMuzzleFlash());
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }

    IEnumerator ShowMuzzleFlash()
    {
        //随机更改MuzzleFlash大小
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        //MuzzleFlash以Z轴为基准随机旋转
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        //激活使其显示
        muzzleFlash.enabled = true;

        //等待随机时间后再禁用MeshRenderer组件
        yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));

        //禁用MeshRenderer组件使其不显示
        muzzleFlash.enabled = false;
    }
}
