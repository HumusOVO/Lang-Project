using UnityEngine;

public class PanMoveState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        CheckSpawn();   //确定生成方块的区域
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        if (gm.mainPan.GetComponent<MainPanController>().PanIsCrash)    //如果主盘在撞下状态
        {
            //对已落下的方块进行轴向冻结
            foreach (var item in gm.grids)
            {
                if (gm.dir == directions.up || gm.dir == directions.down)    //如果方向是上下
                {
                    item.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
                }
                else if (gm.dir == directions.left || gm.dir == directions.right)     //如果方向是左右
                {
                    item.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;
                }
            }
            gm.TransState(gm.destroyState);        //移动完后做一次消除判断
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }

    public void CheckSpawn()    //确定生成方块的区域
    {
        switch (GameManager.instance.dir)        //用整体方向定位
        {
            case directions.up:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnDownPoint;
                break;
            case directions.down:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnUpPoint;
                break;
            case directions.left:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnRightPoint;
                break;
            case directions.right:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnLeftPoint;
                break;
            default:
                break;
        }
    }
}
