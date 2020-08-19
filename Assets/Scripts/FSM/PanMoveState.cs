using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanMoveState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.ResetColl();      //一开始移动主盘就先复原四周的边界
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        if (gm.mainPan.GetComponent<MainPanController>().PanIsCrash)    //如果主盘在撞下状态
        {
            //对已落下的方块进行轴向冻结
            foreach (var item in gm.grids)
            {
                if (GameManager.instance.dir == directions.up || GameManager.instance.dir == directions.down)
                {
                    item.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
                }
                else if (GameManager.instance.dir == directions.left || GameManager.instance.dir == directions.right)
                {
                    item.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;
                }
                //给方块一个向底面的力
                item.GetComponent<Rigidbody2D>().AddForce(gm.powerDir * gm.gridMovePower);
            }
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
