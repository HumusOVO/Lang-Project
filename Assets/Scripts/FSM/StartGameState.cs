using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : EasePotralState
{
    /// <summary>
    /// 开始状态  =》从点击开始按钮后到主盘第0次下落完毕
    /// 接续游戏状态
    /// </summary>
    /// <param name="gm"></param>

    public override void EnterPotral(GameManager gm)
    {
        gm.gameStart = true;        //向GM标记游戏开始

        //执行一次等同于按键盘下键的操作
        gm.mainPan.GetComponent<MainPanController>().FristMove();         //呼叫GM登记的主盘里的函数方法进行第一次移动
        gm.TransState(gm.tetris);      //移动完成后将GM的状态改成游戏状态
    }

    public override void OnFixedUpdate(GameManager gm)
    {

    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
