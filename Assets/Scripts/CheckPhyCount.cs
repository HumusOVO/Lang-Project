using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CheckPhyCount : MonoBehaviour
{
    public float currCount;   //临时存储方块数量
    public float TotleCount;  //被消除应达到的方块量

    public void CheckDesGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<PhysiceCheck>().DrawCheck();
        }


        if (currCount >= TotleCount)     //如果临时存储方块数量到达应达方块量
        {
            for (int i = 0; i < transform.childCount; i++)         //遍历所有判断区
            {
                Destroy(transform.GetChild(i).GetComponent<PhysiceCheck>().grid.gameObject);      //删除判断区里存储的方块
                transform.GetChild(i).GetComponent<PhysiceCheck>().hasGrid = false;      //关闭判断区里有存储的标记
                for (int j = 0; j < GameManager.instance.grids.Count; j++)     //遍历所有已落地的方块
                {
                    GameManager.instance.grids[j].GetComponent<Grid>().Force();     //给这些方块一个下落的力
                }
            }
            Reset();     //重置临时数量
        }

        GameManager.instance.DesCheckEnd();
    }

    public void AddCount()     //增加方块量
    {
        currCount++;
    }

    public void RemoteCount()  //减少方块量
    {
        currCount--;
    }

    public void Reset()     //复原数据
    {
        currCount = 0;
    }

}
