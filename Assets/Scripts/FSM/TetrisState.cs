using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TetrisState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.ResetColl();          //每次进行游戏时先复原边界
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        var grid = gm.grid;      //登记临时变量(避免语句过长) 

        if (gm.currState == gm.pan || gm.isPanMove)       //如果主盘正在移动状态/移动标识为真则跳过监测
        {
            return;
        }

        if (Input.GetKeyDown(gm.keyCode))       //如果按下下落按钮
        {
            gm.upFrontier.gameObject.SetActive(false);     //关掉上面的边界令方块能够下落
            grid.GetComponent<Grid>().IsMove = false;    //下落后关闭左右移动
            grid.GetComponent<Grid>().IsMoving = true;    //标记正在移动
            grid.GetComponent<Rigidbody2D>().AddForce(gm.powerDir * gm.gridMovePower * 5f, ForceMode2D.Force);//给方块一个向底面的力使其前进
        }


        if (grid != null && grid.GetComponent<Grid>().IsMoving && grid.GetComponent<Grid>().hasColl)    //如果存在方块/方块在下落状态/方块碰撞到物体
        {
            //代表其落地
            grid.GetComponent<Grid>().IsMoving = false;    //去除正在下落标识
            grid.GetComponent<Grid>().IsMove = false;    //去除移动标识（保险）
            grid.GetComponent<Grid>().isGround = true;     //标识落地
            grid.transform.SetParent(GameManager.instance.mainPan.transform.GetChild(0).transform);     //整理到list物体里
            grid.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //刚体速度设0，避免受冲击力影响（仍有反冲力，需要改进）
            gm.grids.Add(grid);//加入已落地方块集合，等待判断

            gm.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();     //告知生成器新生成一个方块
            gm.TransState(gm.destroyState);//进入清除方块状态
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
