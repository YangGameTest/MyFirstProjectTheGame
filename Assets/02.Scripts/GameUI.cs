using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//为了访问UI组件，需要声明使用UI命名空间。
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    //声明文本变量
    public Text txtScore;

    //表示分数的变量
    private int totScore = 0;

    private void Start()
    {
        DispScore(0);
    }

    //累加分数并显示到游戏画面
    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "score<color=#ff0000>" + totScore.ToString() + "</color>";
    }


}