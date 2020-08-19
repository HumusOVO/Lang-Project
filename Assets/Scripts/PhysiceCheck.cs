using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysiceCheck : MonoBehaviour
{

    /// <summary>
    /// 放在每一格存储区里进行单独判断，数据在父级处理
    /// </summary>

    public bool hasGrid;   //标识判断区上是否有方块
    public GameObject grid;  //存储在判断区域上的方块
    public LayerMask checkLayer;//判断层

    public void DrawCheck()
    {
        var hasCheck = Physics2D.OverlapCircle(transform.position, 0.5f, checkLayer);
        if (Physics2D.OverlapCircle(transform.position, 0.5f, checkLayer))
        {
            if (!hasGrid)
            {
                grid = hasCheck.gameObject;   //存储此方块
                transform.parent.GetComponent<CheckPhyCount>().AddCount(); //告知父级有存储方块
                hasGrid = true;    //标识此判断区里已有方块
            }

        }
        else
        {
            if (grid != null)
            {
                hasGrid = false;    //标识存储区里没有方块
                grid = null;     //删除存储
                transform.parent.GetComponent<CheckPhyCount>().RemoteCount();      //告知父级没有存储方块
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
