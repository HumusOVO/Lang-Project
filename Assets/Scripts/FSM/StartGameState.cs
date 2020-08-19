using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : EasePotralState
{
    /// <summary>
    /// ��ʼ״̬  =���ӵ����ʼ��ť�����̵�0���������
    /// ������Ϸ״̬
    /// </summary>
    /// <param name="gm"></param>

    public override void EnterPotral(GameManager gm)
    {
        gm.gameStart = true;        //��GM�����Ϸ��ʼ

        //ִ��һ�ε�ͬ�ڰ������¼��Ĳ���
        gm.mainPan.GetComponent<MainPanController>().FristMove();         //����GM�Ǽǵ�������ĺ����������е�һ���ƶ�
        gm.TransState(gm.tetris);      //�ƶ���ɺ�GM��״̬�ĳ���Ϸ״̬
    }

    public override void OnFixedUpdate(GameManager gm)
    {

    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
