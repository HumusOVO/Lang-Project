using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{

    /// <summary>
    /// �����������ȫ�ֲ���
    /// </summary>

    #region simgle

    public static GameManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    #endregion

    [Title("���¼�Ĳ���", HorizontalLine = true)]
    //������ж�������λ��Ϊ׼���������巽�����=�������£��������������ɣ����µ���
    [LabelText("���巽��")]
    public directions dir = directions.down;
    [LabelText("Ŀǰ���Ƶķ���")]
    public GameObject grid;
    [LabelText("��߽�")]
    public Transform leftFrontier;
    [LabelText("�ұ߽�")]
    public Transform rightFrontier;
    [LabelText("�±߽�")]
    public Transform downFrontier;
    [LabelText("�ϱ߽�")]
    public Transform upFrontier;
    [LabelText("���䰴ť")]
    public KeyCode keyCode;
    [LabelText("�����ƶ�����")]
    public Vector2 powerDir;
    [LabelText("������������")]
    public float gridMovePower;
    [LabelText("��ǰ��������жϵ����")]
    public GameObject currGridDesCheck;

    [Title("����״̬����")]
    [LabelText("��ǰ״̬")]
    public EasePotralState currState;
    public StartGameState start = new StartGameState();     //��ʼ״̬
    public PanMoveState pan = new PanMoveState();     //�ƶ�״̬
    public TetrisState tetris = new TetrisState();    //��Ϸ״̬
    public DestroyGridState destroyState = new DestroyGridState();    //��������״̬
    [LabelText("��Ϸ�Ƿ��ڿ�ʼ״̬")]
    public bool gameStart;
    [LabelText("��Ϸ�Ƿ��ڽ���״̬")]
    public bool gameOver;
    [LabelText("�����Ƿ����ƶ�")]
    public bool isPanMove;
    [LabelText("�����Ƿ�����Ϸ״̬")]
    public bool isTetris;
    [LabelText("�����Ƿ�������״̬")]
    public bool isDesGrid;

    [Title("���")]
    [LabelText("����")]
    public GameObject mainPan;
    [LabelText("������")]
    public GameObject spawnGridObj;
    [LabelText("�����·���ļ���")]
    public List<GameObject> grids = new List<GameObject>();
    [LabelText("�����")]
    public GameObject gridListObj;
    [LabelText("�����������·��������ж�")]
    public GameObject upDownGridDesCheck;
    [LabelText("�����������ҷ��������ж�")]
    public GameObject leftRightGridDesCheck;


    public void Start()
    {
        TransState(start);        //��ʼ�˳���ʱ�Ƚ��뿪ʼ״̬���������£�
    }

    public void Update()
    {
        currState.OnUpdate(this);      //ִ��״̬��update����
    }

    public void FixedUpdate()
    {
        currState.OnFixedUpdate(this);     //ִ��״̬��FixedUpdate����
    }

    public void TransState(EasePotralState state)        //�ı�״̬
    {
        currState = state;     //��GM�Ǽ����ڵ�״̬
        currState.EnterPotral(this);    //ִ�н���״̬ʱ�ȴ���Ķ���
    }

    public void ResetColl()        //��ԭ�߽磨�ƶ�ʱʹ�ã�
    {
        //�����еı߽����¿���
        upFrontier.gameObject.SetActive(true);
        downFrontier.gameObject.SetActive(true);
        leftFrontier.gameObject.SetActive(true);
        rightFrontier.gameObject.SetActive(true);
    }


    public void DesCheckEnd()
    {
        //�ر������жϵ�obj
        GameManager.instance.isDesGrid = false;
        TransState(tetris);//������Ϸ״̬
    }

}

public enum directions   //��Ϊ������巽���ö��
{
    up, down, left, right
}
