using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    //声明表示怪兽状态信息的Enumerable变量
    public enum MonsterState { idle,trace,attcak,die};
    //保存怪兽当前状态的Enum变量
    public MonsterState monsterState = MonsterState.idle;

    //为提高速度而向变量分配各种组件
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    //追击范围
    public float traceDist = 10.0f;
    //攻击范围
    public float attackDist = 2.0f;
    //怪兽是否死亡
    private bool isDie = false;

    //血迹效果预设
    public GameObject bloodEffect;
    //血迹贴图效果预设
    public GameObject bloodDecal;

    private void Start()
    {
        //获取怪兽的Transform组件
        monsterTr = this.gameObject.GetComponent<Transform>();
        //获取怪兽要追击的对象—玩家的Transform
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //获取NavMeshAgent组件
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        //获取Animator组件
        animator = this.GetComponent<Animator>();

        //设置要追击对象的位置后，怪兽马上开始追击
        //nvAgent.destination = playerTr.position;

        //运行定期检查怪兽当前状态的协程函数
        StartCoroutine(this.CheckMonsterState());

        //运行根据怪兽当前状态执行相应例程的协程函数
        StartCoroutine(this.MonsterAction());

    }

    //定期检查怪兽当前状态并更新monsterState值
    IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            //等待0.2秒后再执行后续代码
            yield return new WaitForSeconds(0.2f);

            //测量怪兽与玩家之间的距离
            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist<=attackDist) //查看玩家是否进入攻击范围
            {
                monsterState = MonsterState.attcak;
            }
            else if (dist <=traceDist)  //查看玩家是否进入追击范围
            {
                monsterState = MonsterState.trace;  //将怪兽状态设置为追击
            }
            else
            {
                monsterState = MonsterState.idle;  //将怪兽状态设置为idle
            }

        }

    }

    //根据怪兽当前状态执行适当的动作
    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monsterState)
            {
                //idle状态
                case MonsterState.idle:
                    //停止追击
                    nvAgent.Stop();
                    //将Aniamtor的IsTrace变量设置为false
                    animator.SetBool("IsTrace", false);
                    break;

                    //追击状态
                case MonsterState.trace:
                    //传递要追击对象的位置
                    nvAgent.destination = playerTr.position;
                    //重新开始追击
                    nvAgent.Resume();
                    //将Aniamtor的IsAttack变量设置为false
                    animator.SetBool("IsAttack", false);
                    //将Aniamtor的IsTrace变量设置为True
                    animator.SetBool("IsTrace", true);
                    break;

                    //攻击状态
                case MonsterState.attcak:
                    //停止追击
                    nvAgent.Stop();
                    //将IsAttack设置为true后，转换为attack状态
                    animator.SetBool("IsAttack", true);
                    break;

            }
            yield return null;
        }
    }

    //检查怪兽是否与子弹发生碰撞
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag=="BULLET")
        {
            //调用血迹效果函数
            CreateBloodEffect(coll.transform.position);

            //删除子弹对象Bullet
            Destroy(coll.gameObject);
            //触发IsHit Trigger，使怪兽从 Any State 转换为 gothit 状态
            animator.SetTrigger("IsHit");
        }
    }

    void CreateBloodEffect(Vector3 pos)
    {
        //生成血迹效果
        GameObject bloodl = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(bloodl, 2.0f);

        //贴图生成位置：计算在地面以上的位置
        Vector3 dectorPos = monsterTr.position + (Vector3.up * 0.05f);
        //随机设置贴图旋转值
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));

        //生成贴图预设
        GameObject blood2 = (GameObject)Instantiate(bloodDecal, dectorPos, decalRot);
        //调整贴图大小，使其每次生成的尺寸不同
        float scale = Random.Range(1.5f, 3.5f);
        blood2.transform.localScale = Vector3.one * scale;

        //5秒后删除血迹效果预设
        Destroy(blood2, 5.0f);
    }


}
