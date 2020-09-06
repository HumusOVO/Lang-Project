using UnityEngine;

public class PanMoveState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        CheckSpawn();   //ȷ�����ɷ��������
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        if (gm.mainPan.GetComponent<MainPanController>().PanIsCrash)    //���������ײ��״̬
        {
            //�������µķ���������򶳽�
            foreach (var item in gm.grids)
            {
                if (gm.dir == directions.up || gm.dir == directions.down)    //�������������
                {
                    item.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
                }
                else if (gm.dir == directions.left || gm.dir == directions.right)     //�������������
                {
                    item.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionX;
                }
            }
            gm.TransState(gm.destroyState);        //�ƶ������һ�������ж�
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }

    public void CheckSpawn()    //ȷ�����ɷ��������
    {
        switch (GameManager.instance.dir)        //�����巽��λ
        {
            case directions.up:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnDownPoint;
                break;
            case directions.down:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnUpPoint;
                break;
            case directions.left:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnRightPoint;
                break;
            case directions.right:
                GameManager.instance.spawnGridPoint = GameManager.instance.spawnLeftPoint;
                break;
            default:
                break;
        }
    }
}
