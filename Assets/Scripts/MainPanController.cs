using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainPanController : SerializedMonoBehaviour
{
    /// <summary>
    ///  ���ܣ� ���������ƶ�����ض���
    ///   
    /// ������
    ///        ���̶�ȡ
    ///        �����ƶ�
    ///        �����ƶ�����
    /// </summary>

    #region Simgle
    public static MainPanController instrance;

    public void Awake()
    {
        if (instrance != null)
        {
            Destroy(this);
        }
        instrance = this;
    }
    #endregion


    [Title("���", HorizontalLine = true)]
    [LabelText("����")]
    [PreviewField]
    public GameObject MainGird;
    [LabelText("�����")]
    public Camera MainCamera;
    [LabelText("����Grid�����")]
    public GameObject GridSpawn;

    [Title("�жϲ���", HorizontalLine = true)]
    [LabelText("�����Ƿ������ƶ�")]
    public bool PanIsMove;
    [LabelText("�����Ƿ���������ײ��")]
    public bool PanIsCrash;

    [Title("���̱߽���", HorizontalLine = true)]
    [LabelText("��")]
    public Transform upColl;
    [LabelText("��")]
    public Transform downColl;
    [LabelText("��")]
    public Transform leftColl;
    [LabelText("��")]
    public Transform rightColl;

    [Title("�����߽���", HorizontalLine = true)]
    [LabelText("��")]
    public Transform backUp;
    [LabelText("��")]
    public Transform backDown;
    [LabelText("��")]
    public Transform backLeft;
    [LabelText("��")]
    public Transform backRight;

    [Title("��Ļ��������")]
    [LabelText("��Ļ����ʱ��")]
    public float cameraShakeTime;
    [LabelText("��Ļ��������")]
    public Vector2 cameraShakePos;

    // Start is called before the first frame update
    void Start()
    {
        MainGird = this.gameObject;      //��������
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameStart)    //��Ϸû��ʼ�����ƶ�
        {
            return;
        }
        CheckInput();   //�ƶ��ж�
    }

    #region  �����ƶ����

    #region ���̲��������ƶ�

    public void CheckInput()         //�жϼ������뷽������Ծ������̵��ƶ�����ͷ�������ɵص�
    {
        //���޸�GM��ȫ�ַ�������������

        if (!PanIsMove && !GameManager.instance.grid.GetComponent<Grid>().IsMoving)     //����ȷ�����̲����ƶ�״̬���ò���ֵ��ʶ����û�з������ƶ�״̬
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))          //�жϷ����
            {
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                GameManager.instance.dir = directions.down;      //ȷ��ȫ�ַ���
                ConfirmedFrontier(leftColl, rightColl, downColl, upColl, KeyCode.S, Vector2.down);     //ͨ��
                CheckPanMove(GameManager.instance.dir);        //�����ƶ�������ִ��
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                GameManager.instance.dir = directions.up;
                ConfirmedFrontier(leftColl, rightColl, upColl, downColl, KeyCode.W, Vector2.up);
                CheckPanMove(GameManager.instance.dir);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                GameManager.instance.dir = directions.left;
                ConfirmedFrontier(upColl, downColl, leftColl, rightColl, KeyCode.A, Vector2.left);
                CheckPanMove(GameManager.instance.dir);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                GameManager.instance.dir = directions.right;
                ConfirmedFrontier(upColl, downColl, rightColl, leftColl, KeyCode.D, Vector2.right);
                CheckPanMove(GameManager.instance.dir);
            }

        }

    }
    #endregion

    #region �ƶ�ִ�ж���

    public void CheckPanMove(directions directions)          //�ƶ�������ִ��
    {
        PanIsMove = true;    //��ʶ���������ƶ�

        if (GameManager.instance.grid != null)         //����Ѿ��������ɺõķ���
        {
            GameManager.instance.grid.GetComponent<Grid>().BackAnim();     //���Ƚ����ɺõķ����˳�����
            GameManager.instance.grid.GetComponent<Grid>().gdDir = (gridDirections)GridSpawn.GetComponent<SpawnGris>().CheckGridDir(GameManager.instance.dir);//�޸ķ���
        }
        Tweener tweener = MainGird.transform.DOMove(new Vector3(0, 0, 0), 1f);     //������λ�ù��㣬׼���ƶ�

        switch (directions)               //�ж��ƶ�����
        {
            case directions.up:
                tweener.OnComplete(MainGridMoveUpAnim);       //�����ƶ�����
                break;
            case directions.down:
                tweener.OnComplete(MainGridMoveDownAnim);
                break;
            case directions.left:
                tweener.OnComplete(MainGridMoveLeftAnim);
                break;
            case directions.right:
                tweener.OnComplete(MainGridMoveRightAnim);
                break;
            default:
                break;
        }
    }
    #endregion

    #region �����ƶ�����

    public void MainGridMoveDownAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveY(backDown.position.y - downColl.position.y, 2f);      //�����ƶ�����Ӧλ�ã�ʹ�ñ���coll��λ��
        tweener.SetEase(Ease.InOutElastic);      //ȷ����������Ч��
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);     //��Ļ��Ч��
        camera.OnComplete(IsPanMove);        //�����������ʶ���̲����ƶ�

        GridSpawn.GetComponent<SpawnGris>().SpawnNewGrid();     //�������������ɷ���

    }
    public void MainGridMoveUpAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveY(backUp.position.y - upColl.position.y, 2f);
        tweener.SetEase(Ease.InOutElastic);
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);
        camera.OnComplete(IsPanMove);
        GridSpawn.GetComponent<SpawnGris>().SpawnNewGrid();

    }
    public void MainGridMoveLeftAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveX(backLeft.position.x - leftColl.position.x, 2f);
        tweener.SetEase(Ease.InOutElastic);
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);
        camera.OnComplete(IsPanMove);
        GridSpawn.GetComponent<SpawnGris>().SpawnNewGrid();


    }
    public void MainGridMoveRightAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveX(backRight.position.x - rightColl.position.x, 2f);
        tweener.SetEase(Ease.InOutElastic);
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);
        camera.OnComplete(IsPanMove);
        GridSpawn.GetComponent<SpawnGris>().SpawnNewGrid();


    }

    void IsPanMove()
    {
        PanIsCrash = false;  //��ʶײ������
        PanIsMove = false;  //��ʶ�ƶ�����
        for (int i = 0; i < GameManager.instance.gridListObj.transform.childCount; i++)       //����������صķ������򶳽ᣬ�ٶ����㣨��Ľ���
        {
            GameManager.instance.gridListObj.transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            //GameManager.instance.gridListObj.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        GameManager.instance.TransState(GameManager.instance.tetris);     //�ƶ����������������Ϸ��׼�� 
    }
    #endregion
    #endregion


    public void ConfirmedFrontier(Transform leftFrontier, Transform rightFrontier, Transform downFrontier, Transform upFrontier, KeyCode keyCode, Vector2 gridDropDir)        //��֪GM�������ܲ���
    {
        GameManager.instance.leftFrontier = leftFrontier;
        GameManager.instance.rightFrontier = rightFrontier;
        GameManager.instance.downFrontier = downFrontier;
        GameManager.instance.downFrontier.gameObject.layer = LayerMask.NameToLayer("Ground");
        GameManager.instance.upFrontier = upFrontier;
        GameManager.instance.keyCode = keyCode;
        GameManager.instance.powerDir = gridDropDir;
    }

    public void FristMove()       //������Ϸʱ�ĵ�0���ƶ���׼����
    {
        GameManager.instance.isPanMove = true;     //��֪GM��ʼ�ƶ�
        GameManager.instance.dir = directions.down;      //ȷ��ȫ�ַ������£�
        ConfirmedFrontier(leftColl, rightColl, downColl, upColl, KeyCode.S, Vector2.down);     //ʹ��ͨ���֪GM�������ܲ���
        CheckPanMove(GameManager.instance.dir);        //�����ƶ�������ִ��
        GameManager.instance.isPanMove = false;     //��֪GM�����ƶ�
    }
}
