using Sirenix.OdinInspector;
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

    AudioSource scm;

    void Start()
    {
        MainGird = this.gameObject;      //��������
        scm = GetComponent<AudioSource>();
    }

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
        //���޸�GM��ȫ�ַ��������������������ڷ�����ж�

        if (!PanIsMove)     //����ȷ�����̲����ƶ�״̬���ò���ֵ��ʶ����û�з������ƶ�״̬
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))          //�жϷ����
            {
                GameManager.instance.dir = directions.down;      //ȷ��ȫ�ַ���
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                ConfirmedFrontier(leftColl, rightColl, downColl, upColl, KeyCode.S, Vector2.down);     //ͨ��GM���ֲ���
                CheckPanMove(GameManager.instance.dir);        //�����ƶ�������ִ��
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameManager.instance.dir = directions.up;
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                ConfirmedFrontier(leftColl, rightColl, upColl, downColl, KeyCode.W, Vector2.up);
                CheckPanMove(GameManager.instance.dir);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameManager.instance.dir = directions.left;
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
                ConfirmedFrontier(upColl, downColl, leftColl, rightColl, KeyCode.A, Vector2.left);
                CheckPanMove(GameManager.instance.dir);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GameManager.instance.dir = directions.right;
                GameManager.instance.TransState(GameManager.instance.pan);      //��֪GM�����ƶ�
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

        GameManager.instance.ResetColl();      //һ��ʼ�ƶ����̾��ȸ�ԭ���ܵı߽�

        if (GameManager.instance.grid != null)         //����Ѿ��������ɺõķ���
        {
            GameManager.instance.grid.GetComponent<Grid>().BackAnim();     //��֪�����˳�����
            GameManager.instance.grid.GetComponent<Grid>().gdDir = (gridDirections)GridSpawn.GetComponent<SpawnGris>().CheckGridDir(GameManager.instance.dir);//�޸ķ���ķ���
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
        GameManager.instance.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();      //ͨ��GM��֪���������ɷ���
        PanIsCrash = true;     //���������������
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);     //��Ļ��Ч��
        camera.OnComplete(IsPanMove);        //����������������̽����ƶ���Ĳ���
    }
    public void MainGridMoveUpAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveY(backUp.position.y - upColl.position.y, 2f);
        tweener.SetEase(Ease.InOutElastic);

        GameManager.instance.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);
        camera.OnComplete(IsPanMove);
    }
    public void MainGridMoveLeftAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveX(backLeft.position.x - leftColl.position.x, 2f);
        tweener.SetEase(Ease.InOutElastic);

        GameManager.instance.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);
        camera.OnComplete(IsPanMove);
    }
    public void MainGridMoveRightAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveX(backRight.position.x - rightColl.position.x, 2f);
        tweener.SetEase(Ease.InOutElastic);

        GameManager.instance.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();
        PanIsCrash = true;
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);
        camera.OnComplete(IsPanMove);
    }

    void IsPanMove()
    {
        PanIsCrash = false;  //��ʶײ������
        PanIsMove = false;  //��ʶ�ƶ�����
        scm.Play();
        GameManager.instance.downFrontier.gameObject.tag = "Ground";     //����Ϊ�·��ı߽�����tag
        for (int i = 0; i < GameManager.instance.gridListObj.transform.childCount; i++)       //����������صķ������򶳽��ֹ�ƶ�
        {
            GameManager.instance.gridListObj.transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        GameManager.instance.upFrontier.GetComponent<collCheck>().topColl = true;       //����Ϊ�Ϸ��ı߽�ĳ��������жϴ�
                                                                                        //GameManager.instance.TransState(GameManager.instance.destroyState);        //�ƶ������һ�������ж�
        GameManager.instance.TransState(GameManager.instance.tetris);     //�ƶ����������������Ϸ��׼�� 
    }
    #endregion
    #endregion

    public void ConfirmedFrontier(Transform leftFrontier, Transform rightFrontier, Transform downFrontier, Transform upFrontier, KeyCode keyCode, Vector2 gridDropDir)        //��֪GM�������ܲ���
    {
        //ȷ���������ҵı߽�
        GameManager.instance.leftFrontier = leftFrontier;
        GameManager.instance.rightFrontier = rightFrontier;
        GameManager.instance.downFrontier = downFrontier;
        GameManager.instance.upFrontier = upFrontier;

        //ȷ������������䷽��
        GameManager.instance.keyCode = keyCode;
        GameManager.instance.powerDir = gridDropDir;

        //ȷ��ֻ���Ϸ��߽��жϴ����
        GameManager.instance.upFrontier.GetComponent<collCheck>().enabled = true;
        GameManager.instance.downFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.leftFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.rightFrontier.GetComponent<collCheck>().enabled = false;

        //���ñ߽�ĳ����ж�
        GameManager.instance.upFrontier.GetComponent<collCheck>().topColl = false;
        GameManager.instance.downFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.leftFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.rightFrontier.GetComponent<collCheck>().enabled = false;

        //�������巽��ı��ж����������
        var gm = GameManager.instance;
        if (gm.dir == directions.up || gm.dir == directions.down)
        {
            gm.leftRightGridDesCheck.SetActive(false);
            gm.upDownGridDesCheck.SetActive(true);
            gm.currGridDesCheck = gm.upDownGridDesCheck;     //��ѡ�е��������GM
        }
        else if (gm.dir == directions.left || gm.dir == directions.right)
        {
            gm.leftRightGridDesCheck.SetActive(true);
            gm.upDownGridDesCheck.SetActive(false);
            gm.currGridDesCheck = gm.leftRightGridDesCheck;
        }
    }

    public void FristMove()       //������Ϸʱ�ĵ�0���ƶ���һ��ʼ��
    {
        GameManager.instance.isPanMove = true;     //��֪GM��ʼ�ƶ�
        GameManager.instance.dir = directions.down;      //ȷ��ȫ�ַ������£�
        ConfirmedFrontier(leftColl, rightColl, downColl, upColl, KeyCode.S, Vector2.down);     //ʹ��ͨ���֪GM�������ܲ���
        CheckPanMove(GameManager.instance.dir);        //�����ƶ�������ִ��
        GameManager.instance.isPanMove = false;     //��֪GM�����ƶ�
    }
}
