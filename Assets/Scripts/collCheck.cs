using Sirenix.OdinInspector;
using UnityEngine;

public class collCheck : SerializedMonoBehaviour
{
    /// <summary>
    /// �����жϳ������޵ķ�����������Ϸ�Ƿ����
    /// ���������̵��ĸ��߽���
    /// </summary>

    [Title("�ж����߲���", HorizontalLine = true)]
    [LabelText("�Ƿ��ڶ���")]
    public bool topColl;
    [LabelText("���߳ߴ�")]
    public Vector2 sizeCheck;
    [LabelText("���жϲ�")]
    public LayerMask layer;
    [LabelText("����λ��")]
    public Vector3 currDir;

    void Update()
    {
        //�����������߼�ⷽ��
        //����������ڶ����Ҽ�⵽���飨�����鳬�����ޣ�
        if (Physics2D.OverlapBox(transform.position + currDir, sizeCheck, 1f, layer) && topColl)
        {
            GameManager.instance.gameOver = true;      //��֪GM��Ϸ����
        }
    }

    public void OnDrawGizmos()    //���߻滭
    {
        Gizmos.DrawWireCube(transform.position + currDir, sizeCheck);
    }
}
