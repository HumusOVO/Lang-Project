using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CheckPhyCountRay : SerializedMonoBehaviour
{
    /// <summary>
    /// 挂载在每列每行上
    /// 判断本行方块是否可消除
    /// </summary>

    [Title("射线参数", HorizontalLine = true)]
    [LabelText("射线方向")]
    public Vector2 lineDir;
    [LabelText("射线长度")]
    public float lineLenght;
    [LabelText("被检测层")]
    public LayerMask gridLayer;
    [LabelText("检测到的物体集")]
    public List<GameObject> grids = new List<GameObject>();

    [Title("方块数量参数", HorizontalLine = true)]
    [LabelText("消除需达到的方块数量")]
    public float totleCount;
    [LabelText("目前射线所检测到的方块数量")]
    public float currGridCount;

    void Update()
    {
        Debug.DrawRay(transform.position, lineDir * lineLenght, Color.green);   //绘画射线
    }

    public void DrawCheckLink()       //绘画检测射线
    {
        grids.Clear();   //清除集合，防止过期obj
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, lineDir * lineLenght, gridLayer);   //画射线
        foreach (var item in hits)    //循环射线取到的obj
        {
            if (!grids.Contains(item.collider.gameObject))   //如果取到的物体不在集合中
            {
                if (item.collider.gameObject.tag == "simGrid")    //且物体是方块
                {
                    grids.Add(item.collider.gameObject);    //加入集合
                }
            }
        }
        currGridCount = grids.Count;      //全部放完后，记录得到的方块数量
    }

    public void CheckPointRay()     //消除判定
    {
        if (currGridCount >= totleCount)     //如果集合中方块数量大等于设定好的消除总数
        {
            for (int i = (int)totleCount - 1; i >= 0; i--)      //循环集合
            {
                Destroy(grids[i].gameObject);            //删除集合存储的obj
                GameManager.instance.score += 2;     //删一个分数+2
            }
            UIManager.instrance.scoreAdd();       //告知UIM更新分数
            grids.Clear();    //清理集合
            currGridCount = 0;    //射线检测数归零

            //给所有方块锁轴向防止滑动
            for (int j = 0; j < GameManager.instance.grids.Count; j++)     //遍历所有已落地的方块
            {
                if (GameManager.instance.dir == directions.up || GameManager.instance.dir == directions.down)    //如果方向是上下
                {
                    GameManager.instance.grids[j].GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;        //锁掉除Y轴外的其他轴向
                }
                else if (GameManager.instance.dir == directions.left || GameManager.instance.dir == directions.right)     //如果方向是左右
                {
                    GameManager.instance.grids[j].GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;    //锁掉除X轴外的其他轴向
                }
            }
        }
        GameManager.instance.DesCheckEnd();     //向GM请求结束动作
    }
}
