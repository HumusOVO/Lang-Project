
public class DestroyGridState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.isDesGrid = true;    //��֪GM��ʼ����ɾ��
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        for (int i = 0; i < gm.currGridDesCheck.transform.childCount; i++)          //ѭ����������µ�������
        {
            gm.currGridDesCheck.transform.GetChild(i).GetComponent<CheckPhyCountRay>().CheckPointRay();      //��֪���������ж�
        }
    }

    public override void OnUpdate(GameManager gm)
    {
        for (int i = 0; i < gm.currGridDesCheck.transform.childCount; i++)      //ѭ����������µ�������
        {
            gm.currGridDesCheck.transform.GetChild(i).GetComponent<CheckPhyCountRay>().DrawCheckLink();        //��֪���������ж�
        }
    }

}
