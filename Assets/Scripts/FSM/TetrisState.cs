
public class TetrisState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.upFrontier.gameObject.SetActive(true);     //�ص�����ı߽�����ܹ�����
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        var grid = gm.grid;      //�Ǽ���ʱ����(����������) 

        if (gm.currState == gm.pan || gm.isPanMove)       //������������ƶ�״̬/�ƶ���ʶΪ�����������
        {
            return;
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
