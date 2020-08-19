using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanMoveState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.ResetColl();      //һ��ʼ�ƶ����̾��ȸ�ԭ���ܵı߽�
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        if (gm.mainPan.GetComponent<MainPanController>().PanIsCrash)    //���������ײ��״̬
        {
            //�������µķ���������򶳽�
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
                //������һ����������
                item.GetComponent<Rigidbody2D>().AddForce(gm.powerDir * gm.gridMovePower);
            }
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
