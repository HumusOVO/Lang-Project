using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Grid : SerializedMonoBehaviour
{
    /// <summary>
    /// 方块属性
    /// </summary>

    [Title("标记", HorizontalLine = true)]
    [LabelText("所在方位")]
    public gridDirections gdDir = gridDirections.gdUp;
    [LabelText("是否可以移动")]
    public bool IsMove;
    [LabelText("是否正在移动")]
    public bool IsMoving;
    [LabelText("是否可下降")]
    public bool isDownMove;
    [LabelText("是否已落地")]
    public bool isGround;
    [LabelText("落地面")]
    public Transform groundPoint;
    [LabelText("是否碰撞到物体")]
    [SuffixLabel("用来判断方块的停止")]
    public bool hasColl;

    Vector2 currDir;    //grid每次移动的距离
    Rigidbody2D rb;    //初始化参数
    AudioSource scm;   //碰撞声

    void Start()
    {
        //初始化参数
        rb = GetComponent<Rigidbody2D>();
        scm = GetComponent<AudioSource>();
    }

    void Update()
    {
        groundPoint = GameManager.instance.downFrontier;       //决定落地面以准备之后的判断
        if (hasColl || IsMoving)       //如果落地/正在下落，则不再判断运动
        {
            return;
        }
        CheckInput();    //判断输入
    }

    public void FixedUpdate()
    {
        Move();   //移动
        DownMove();      //下落判断
        CheckDownMove();   //进行落地处理
    }

    public void CheckInput()     //移动按钮判断
    {
        if (this.gdDir == gridDirections.gdUp || this.gdDir == gridDirections.gdDown)    //如果方向是上下
        {
            //用AD控制方向
            if (Input.GetKeyDown(KeyCode.A))
            {
                currDir = Vector2.left * 2;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currDir = Vector2.right * 2;
            }
        }
        else if (this.gdDir == gridDirections.gdLeft || this.gdDir == gridDirections.gdRight)     //如果方向是左右
        {
            //用WS控制方向
            if (Input.GetKeyDown(KeyCode.W))
            {
                currDir = Vector2.up * 2;

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                currDir = Vector2.down * 2;
            }
        }

        if (Input.GetKeyDown(GameManager.instance.keyCode))     //如果按下下落键
        {
            isDownMove = true;     //打开下降标志
        }
    }

    private void CheckDownMove()        //落地处理
    {
        if (hasColl && !isGround && rb.velocity == Vector2.zero)        //如果碰到物体且没有在地面上且速度为0（第一次落地）
        {
            var gm = GameManager.instance;
            IsMoving = false;    //去除正在下落标识
            IsMove = false;    //去除移动标识（保险）
            isDownMove = false;     //去除允许下落标识
            scm.Play();
            this.gameObject.transform.SetParent(GameManager.instance.mainPan.transform.GetChild(0).transform);     //整理到list物体里
            GameManager.instance.score++;    //一个方块落地分数+1
            UIManager.instrance.scoreAdd();     //告知UIM更新分数
            rb.constraints = RigidbodyConstraints2D.FreezeAll;    //（改进方法）全轴向冻结
            gm.grids.Add(this.gameObject);//加入已落地方块集合，等待判断
            GameManager.instance.grid = null;         //GM中的控制方块清空
            gm.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();     //告知生成器新生成一个方块
            gm.TransState(gm.destroyState);//进入清除方块状态
            isGround = true;   //打落地标志
        }
    }

    private void DownMove()        //下落处理
    {
        if (!isDownMove)       //如果不允许下落则跳过
        {
            return;
        }
        var gm = GameManager.instance;
        gm.upFrontier.gameObject.SetActive(false);     //关掉上面的边界令方块能够下落
        IsMove = false;    //下落后关闭左右移动
        IsMoving = true;    //标记正在移动
        rb.gravityScale = 2;      //重力设为2
    }

    public void Move()       //移动处理
    {
        if (!IsMove)    //如果不允许移动则跳过
        {
            return;
        }
        rb.MovePosition(transform.position + (Vector3)currDir);      //使用MP对刚体移动
        currDir = Vector2.zero;      //移动距离归零
    }

    public void GridAnim()     //进场动画
    {
        transform.position = GameManager.instance.spawnGridPoint.transform.position;   //确定方块位置

        switch (gdDir)       //照方块方向播动画
        {
            case gridDirections.gdUp:
                Tweener tweener1 = transform.DOMoveY(40f, 1f).From().SetDelay(1f);//进场动画
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
        IsMove = true;      //允许移动
    }

    public void BackAnim()    //退到外面的动画
    {
        switch (gdDir)        //判断方块方向
        {
            case gridDirections.gdUp:
                transform.DOMoveY(40f, 1f);     //执行退到外面的动画
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
        IsMove = false;     //关闭允许移动
    }

    public void OnCollisionEnter2D(Collision2D collision)         //碰撞判断
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Grid"))    //如果碰撞到地面或方块
        {
            hasColl = true;       //标志碰撞到物体，可停下
        }
    }
}

public enum gridDirections      //方块方向
{
    gdUp, gdDown, gdLeft, gdRight
}