using Sirenix.OdinInspector;
using UnityEngine;

public class collCheck : SerializedMonoBehaviour
{
    /// <summary>
    /// 负责判断超出界限的方块来决定游戏是否结束
    /// 挂载在主盘的四个边界上
    /// </summary>

    [Title("判断射线参数", HorizontalLine = true)]
    [LabelText("是否在顶部")]
    public bool topColl;
    [LabelText("射线尺寸")]
    public Vector2 sizeCheck;
    [LabelText("被判断层")]
    public LayerMask layer;
    [LabelText("射线位置")]
    public Vector3 currDir;

    void Update()
    {
        //创建方形射线检测方块
        //如果此射线在顶部且检测到方块（即方块超出界限）
        if (Physics2D.OverlapBox(transform.position + currDir, sizeCheck, 1f, layer) && topColl)
        {
            GameManager.instance.gameOver = true;      //告知GM游戏结束
        }
    }

    public void OnDrawGizmos()    //射线绘画
    {
        Gizmos.DrawWireCube(transform.position + currDir, sizeCheck);
    }
}
