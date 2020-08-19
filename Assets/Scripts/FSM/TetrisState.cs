using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TetrisState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.ResetColl();          //ÿ�ν�����Ϸʱ�ȸ�ԭ�߽�
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        var grid = gm.grid;      //�Ǽ���ʱ����(����������) 

        if (gm.currState == gm.pan || gm.isPanMove)       //������������ƶ�״̬/�ƶ���ʶΪ�����������
        {
            return;
        }

        if (Input.GetKeyDown(gm.keyCode))       //����������䰴ť
        {
            gm.upFrontier.gameObject.SetActive(false);     //�ص�����ı߽�����ܹ�����
            grid.GetComponent<Grid>().IsMove = false;    //�����ر������ƶ�
            grid.GetComponent<Grid>().IsMoving = true;    //��������ƶ�
            grid.GetComponent<Rigidbody2D>().AddForce(gm.powerDir * gm.gridMovePower * 5f, ForceMode2D.Force);//������һ����������ʹ��ǰ��
        }


        if (grid != null && grid.GetComponent<Grid>().IsMoving && grid.GetComponent<Grid>().hasColl)    //������ڷ���/����������״̬/������ײ������
        {
            //���������
            grid.GetComponent<Grid>().IsMoving = false;    //ȥ�����������ʶ
            grid.GetComponent<Grid>().IsMove = false;    //ȥ���ƶ���ʶ�����գ�
            grid.GetComponent<Grid>().isGround = true;     //��ʶ���
            grid.transform.SetParent(GameManager.instance.mainPan.transform.GetChild(0).transform);     //����list������
            grid.GetComponent<Rigidbody2D>().velocity = Vector3.zero; //�����ٶ���0�������ܳ����Ӱ�죨���з���������Ҫ�Ľ���
            gm.grids.Add(grid);//��������ط��鼯�ϣ��ȴ��ж�

            gm.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();     //��֪������������һ������
            gm.TransState(gm.destroyState);//�����������״̬
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
