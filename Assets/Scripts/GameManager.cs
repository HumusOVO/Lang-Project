using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{

    /// <summary>
    /// �����������ȫ�ֲ���
    /// </summary>

    #region Simgle

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
    [LabelText("�±߽�(����)")]
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
    [LabelText("������")]
    public GameObject spawnGridPoint;
    [LabelText("����")]
    public GameObject spawnUpPoint;
    [LabelText("����")]
    public GameObject spawnDownPoint;
    [LabelText("����")]
    public GameObject spawnLeftPoint;
    [LabelText("����")]
    public GameObject spawnRightPoint;
    [LabelText("����")]
    public int score;
    [LabelText("��߷���")]
    public int bestscore = 0;

    [Title("����״̬����", HorizontalLine = true)]
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

    [Title("���", HorizontalLine = true)]
    [LabelText("����")]
    public GameObject mainPan;
    [LabelText("������")]
    public GameObject spawnGridObj;
    [LabelText("�����·���ļ���")]
    public List<GameObject> grids = new List<GameObject>();
    [LabelText("������·��鼯�ϵ�obj")]
    public GameObject gridListObj;
    [LabelText("�����������·��������ж�")]
    public GameObject upDownGridDesCheck;
    [LabelText("�����������ҷ��������ж�")]
    public GameObject leftRightGridDesCheck;
    [LabelText("bgm")]
    public AudioSource bgm;


    public void Start()
    {
        bgm = this.GetComponent<AudioSource>();
        //TransState(start);        //��ʼ�˳���ʱ�Ƚ��뿪ʼ״̬���������£�
        if (PlayerPrefs.HasKey("bestscore"))       //����з����浵���������
        {
            bestscore = PlayerPrefs.GetInt("bestscore");  //��߷�����Ϊ�������
            UIManager.instrance.StartGame();        //����UIM���п�ʼ��Ϸ��UI�趨
        }
    }

    public void Update()
    {
        if (!gameStart)       //�����Ϸû�п�ʼ������ִ��
        {
            return;
        }

        if (gameOver)      //�����Ϸ������־�Ѵ���
        {
            Time.timeScale = 0;      //����
            UIManager.instrance.GameOver();   //����UIM����������
        }
        currState.OnUpdate(this);      //ִ��״̬��update����
        CheckGravise();   //�ı���������
    }

    public void FixedUpdate()
    {
        if (!gameStart)   //�����Ϸû�п�ʼ������ִ��
        {
            return;
        }
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

        //�߽�tag��ԭ
        upFrontier.gameObject.tag = "Frame";
        downFrontier.gameObject.tag = "Frame";
        leftFrontier.gameObject.tag = "Frame";
        rightFrontier.gameObject.tag = "Frame";
    }

    public void DesCheckEnd()     //ɾ���жϽ�����ִ��
    {
        //�ر������жϵ�obj
        GameManager.instance.isDesGrid = false;
        TransState(tetris);//������Ϸ״̬
    }

    public void CheckGravise()       //�������巽��ı���������
    {
        switch (dir)
        {
            case directions.up:
                Physics2D.gravity = new Vector2(0, 9.81f);
                break;
            case directions.down:
                Physics2D.gravity = new Vector2(0, -9.81f);
                break;
            case directions.left:
                Physics2D.gravity = new Vector2(-9.81f, 0);
                break;
            case directions.right:
                Physics2D.gravity = new Vector2(9.81f, 0);
                break;
            default:
                break;
        }
    }

    public void GameOver()    //��Ϸ������Է����Ĵ���UI������UIM��
    {
        bgm.Stop();
        if (score > bestscore)    //�����Ϸ����ʱ���·�������߷�����
        {
            //�洢�·���
            PlayerPrefs.SetInt("bestscore", score);
            PlayerPrefs.Save();
        }
    }
}

public enum directions   //��Ϊ������巽���ö��
{
    up, down, left, right
}
