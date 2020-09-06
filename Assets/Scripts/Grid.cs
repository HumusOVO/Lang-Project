using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Grid : SerializedMonoBehaviour
{
    /// <summary>
    /// ��������
    /// </summary>

    [Title("���", HorizontalLine = true)]
    [LabelText("���ڷ�λ")]
    public gridDirections gdDir = gridDirections.gdUp;
    [LabelText("�Ƿ�����ƶ�")]
    public bool IsMove;
    [LabelText("�Ƿ������ƶ�")]
    public bool IsMoving;
    [LabelText("�Ƿ���½�")]
    public bool isDownMove;
    [LabelText("�Ƿ������")]
    public bool isGround;
    [LabelText("�����")]
    public Transform groundPoint;
    [LabelText("�Ƿ���ײ������")]
    [SuffixLabel("�����жϷ����ֹͣ")]
    public bool hasColl;

    Vector2 currDir;    //gridÿ���ƶ��ľ���
    Rigidbody2D rb;    //��ʼ������
    AudioSource scm;   //��ײ��

    void Start()
    {
        //��ʼ������
        rb = GetComponent<Rigidbody2D>();
        scm = GetComponent<AudioSource>();
    }

    void Update()
    {
        groundPoint = GameManager.instance.downFrontier;       //�����������׼��֮����ж�
        if (hasColl || IsMoving)       //������/�������䣬�����ж��˶�
        {
            return;
        }
        CheckInput();    //�ж�����
    }

    public void FixedUpdate()
    {
        Move();   //�ƶ�
        DownMove();      //�����ж�
        CheckDownMove();   //������ش���
    }

    public void CheckInput()     //�ƶ���ť�ж�
    {
        if (this.gdDir == gridDirections.gdUp || this.gdDir == gridDirections.gdDown)    //�������������
        {
            //��AD���Ʒ���
            if (Input.GetKeyDown(KeyCode.A))
            {
                currDir = Vector2.left * 2;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currDir = Vector2.right * 2;
            }
        }
        else if (this.gdDir == gridDirections.gdLeft || this.gdDir == gridDirections.gdRight)     //�������������
        {
            //��WS���Ʒ���
            if (Input.GetKeyDown(KeyCode.W))
            {
                currDir = Vector2.up * 2;

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currDir = Vector2.down * 2;
            }
        }

        if (Input.GetKeyDown(GameManager.instance.keyCode))     //������������
        {
            isDownMove = true;     //���½���־
        }
    }

    private void CheckDownMove()        //��ش���
    {
        if (hasColl && !isGround && rb.velocity == Vector2.zero)        //�������������û���ڵ��������ٶ�Ϊ0����һ����أ�
        {
            var gm = GameManager.instance;
            IsMoving = false;    //ȥ�����������ʶ
            IsMove = false;    //ȥ���ƶ���ʶ�����գ�
            isDownMove = false;     //ȥ�����������ʶ
            scm.Play();
            this.gameObject.transform.SetParent(GameManager.instance.mainPan.transform.GetChild(0).transform);     //����list������
            GameManager.instance.score++;    //һ��������ط���+1
            UIManager.instrance.scoreAdd();     //��֪UIM���·���
            rb.constraints = RigidbodyConstraints2D.FreezeAll;    //���Ľ�������ȫ���򶳽�
            gm.grids.Add(this.gameObject);//��������ط��鼯�ϣ��ȴ��ж�
            GameManager.instance.grid = null;         //GM�еĿ��Ʒ������
            gm.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();     //��֪������������һ������
            gm.TransState(gm.destroyState);//�����������״̬
            isGround = true;   //����ر�־
        }
    }

    private void DownMove()        //���䴦��
    {
        if (!isDownMove)       //�������������������
        {
            return;
        }
        var gm = GameManager.instance;
        gm.upFrontier.gameObject.SetActive(false);     //�ص�����ı߽�����ܹ�����
        IsMove = false;    //�����ر������ƶ�
        IsMoving = true;    //��������ƶ�
        rb.gravityScale = 2;      //������Ϊ2
    }

    public void Move()       //�ƶ�����
    {
        if (!IsMove)    //����������ƶ�������
        {
            return;
        }
        rb.MovePosition(transform.position + (Vector3)currDir);      //ʹ��MP�Ը����ƶ�
        currDir = Vector2.zero;      //�ƶ��������
    }

    public void GridAnim()     //��������
    {
        transform.position = GameManager.instance.spawnGridPoint.transform.position;   //ȷ������λ��

        switch (gdDir)       //�շ��鷽�򲥶���
        {
            case gridDirections.gdUp:
                Tweener tweener1 = transform.DOMoveY(40f, 1f).From().SetDelay(1f);//��������
                break;
            case gridDirections.gdDown:
                Tweener tweener2 = transform.DOMoveY(-48f, 1f).From().SetDelay(1f);
                break;
            case gridDirections.gdLeft:
                Tweener tweener3 = transform.DOMoveX(-40f, 1f).From().SetDelay(1f);
                break;
            case gridDirections.gdRight:
                Tweener tweener = transform.DOMoveX(40f, 1f).From().SetDelay(1f);
                break;
            default:
                break;
        }
        IsMove = true;      //�����ƶ�
    }

    public void BackAnim()    //�˵�����Ķ���
    {
        switch (gdDir)        //�жϷ��鷽��
        {
            case gridDirections.gdUp:
                transform.DOMoveY(40f, 1f);     //ִ���˵�����Ķ���
                break;
            case gridDirections.gdDown:
                transform.DOMoveY(-48f, 1f);
                break;
            case gridDirections.gdLeft:
                transform.DOMoveX(-40f, 1f);
                break;
            case gridDirections.gdRight:
                transform.DOMoveX(40f, 1f);
                break;
            default:
                break;
        }
        IsMove = false;     //�ر������ƶ�
    }

    public void OnCollisionEnter2D(Collision2D collision)         //��ײ�ж�
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Grid"))    //�����ײ������򷽿�
        {
            hasColl = true;       //��־��ײ�����壬��ͣ��
        }
    }
}

public enum gridDirections      //���鷽��
{
    gdUp, gdDown, gdLeft, gdRight
}