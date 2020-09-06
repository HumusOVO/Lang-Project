using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CheckPhyCountRay : SerializedMonoBehaviour
{
    /// <summary>
    /// ������ÿ��ÿ����
    /// �жϱ��з����Ƿ������
    /// </summary>

    [Title("���߲���", HorizontalLine = true)]
    [LabelText("���߷���")]
    public Vector2 lineDir;
    [LabelText("���߳���")]
    public float lineLenght;
    [LabelText("������")]
    public LayerMask gridLayer;
    [LabelText("��⵽�����弯")]
    public List<GameObject> grids = new List<GameObject>();

    [Title("������������", HorizontalLine = true)]
    [LabelText("������ﵽ�ķ�������")]
    public float totleCount;
    [LabelText("Ŀǰ��������⵽�ķ�������")]
    public float currGridCount;

    void Update()
    {
        Debug.DrawRay(transform.position, lineDir * lineLenght, Color.green);   //�滭����
    }

    public void DrawCheckLink()       //�滭�������
    {
        grids.Clear();   //������ϣ���ֹ����obj
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, lineDir * lineLenght, gridLayer);   //������
        foreach (var item in hits)    //ѭ������ȡ����obj
        {
            if (!grids.Contains(item.collider.gameObject))   //���ȡ�������岻�ڼ�����
            {
                if (item.collider.gameObject.tag == "simGrid")    //�������Ƿ���
                {
                    grids.Add(item.collider.gameObject);    //���뼯��
                }
            }
        }
        currGridCount = grids.Count;      //ȫ������󣬼�¼�õ��ķ�������
    }

    public void CheckPointRay()     //�����ж�
    {
        if (currGridCount >= totleCount)     //��������з�������������趨�õ���������
        {
            for (int i = (int)totleCount - 1; i >= 0; i--)      //ѭ������
            {
                Destroy(grids[i].gameObject);            //ɾ�����ϴ洢��obj
                GameManager.instance.score += 2;     //ɾһ������+2
            }
            UIManager.instrance.scoreAdd();       //��֪UIM���·���
            grids.Clear();    //������
            currGridCount = 0;    //���߼��������

            //�����з����������ֹ����
            for (int j = 0; j < GameManager.instance.grids.Count; j++)     //������������صķ���
            {
                if (GameManager.instance.dir == directions.up || GameManager.instance.dir == directions.down)    //�������������
                {
                    GameManager.instance.grids[j].GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;        //������Y�������������
                }
                else if (GameManager.instance.dir == directions.left || GameManager.instance.dir == directions.right)     //�������������
                {
                    GameManager.instance.grids[j].GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;    //������X�������������
                }
            }
        }
        GameManager.instance.DesCheckEnd();     //��GM�����������
    }
}
