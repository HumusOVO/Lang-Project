using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PhysiceCheck : MonoBehaviour
{

    /// <summary>
    /// ����ÿһ��洢������е����жϣ������ڸ�������
    /// </summary>

    public bool hasGrid;   //��ʶ�ж������Ƿ��з���
    public GameObject grid;  //�洢���ж������ϵķ���
    public LayerMask checkLayer;//�жϲ�

    public void DrawCheck()
    {
        var hasCheck = Physics2D.OverlapCircle(transform.position, 0.5f, checkLayer);
        if (Physics2D.OverlapCircle(transform.position, 0.5f, checkLayer))
        {
            if (!hasGrid)
            {
                grid = hasCheck.gameObject;   //�洢�˷���
                transform.parent.GetComponent<CheckPhyCount>().AddCount(); //��֪�����д洢����
                hasGrid = true;    //��ʶ���ж��������з���
            }

        }
        else
        {
            if (grid != null)
            {
                hasGrid = false;    //��ʶ�洢����û�з���
                grid = null;     //ɾ���洢
                transform.parent.GetComponent<CheckPhyCount>().RemoteCount();      //��֪����û�д洢����
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
