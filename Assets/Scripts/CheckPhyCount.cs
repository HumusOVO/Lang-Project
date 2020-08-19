using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CheckPhyCount : MonoBehaviour
{
    public float currCount;   //��ʱ�洢��������
    public float TotleCount;  //������Ӧ�ﵽ�ķ�����

    public void CheckDesGrid()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<PhysiceCheck>().DrawCheck();
        }


        if (currCount >= TotleCount)     //�����ʱ�洢������������Ӧ�﷽����
        {
            for (int i = 0; i < transform.childCount; i++)         //���������ж���
            {
                Destroy(transform.GetChild(i).GetComponent<PhysiceCheck>().grid.gameObject);      //ɾ���ж�����洢�ķ���
                transform.GetChild(i).GetComponent<PhysiceCheck>().hasGrid = false;      //�ر��ж������д洢�ı��
                for (int j = 0; j < GameManager.instance.grids.Count; j++)     //������������صķ���
                {
                    GameManager.instance.grids[j].GetComponent<Grid>().Force();     //����Щ����һ���������
                }
            }
            Reset();     //������ʱ����
        }

        GameManager.instance.DesCheckEnd();
    }

    public void AddCount()     //���ӷ�����
    {
        currCount++;
    }

    public void RemoteCount()  //���ٷ�����
    {
        currCount--;
    }

    public void Reset()     //��ԭ����
    {
        currCount = 0;
    }

}
