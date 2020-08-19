using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGridState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.isDesGrid = true;    //告知GM开始进行删除
        //判断要打开的判断obj
        if (gm.dir == directions.up || gm.dir == directions.down)
        {
            gm.leftRightGridDesCheck.SetActive(false);
            gm.upDownGridDesCheck.SetActive(true);
            gm.currGridDesCheck = gm.upDownGridDesCheck;
        }
        else if (gm.dir == directions.left || gm.dir == directions.right)
        {
            gm.leftRightGridDesCheck.SetActive(true);
            gm.upDownGridDesCheck.SetActive(false);

            gm.currGridDesCheck = gm.leftRightGridDesCheck;
        }
    }

    public override void OnFixedUpdate(GameManager gm)
    {

    }

    public override void OnUpdate(GameManager gm)
    {
        for (int i = 0; i < gm.currGridDesCheck.transform.childCount; i++)
        {
            gm.currGridDesCheck.transform.GetChild(i).GetComponent<CheckPhyCount>().CheckDesGrid();
        }
    }

}
