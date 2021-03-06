using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

public class MainPanController : SerializedMonoBehaviour
{
    /// <summary>
    ///  功能： 控制主盘移动及相关动画
    ///   
    /// 包括：
    ///        键盘读取
    ///        主盘移动
    ///        主盘移动动画
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

    [Title("组件", HorizontalLine = true)]
    [LabelText("主盘")]
    [PreviewField]
    public GameObject MainGird;
    [LabelText("摄像机")]
    public Camera MainCamera;
    [LabelText("生成Grid的组件")]
    public GameObject GridSpawn;

    [Title("判断参数", HorizontalLine = true)]
    [LabelText("主盘是否正在移动")]
    public bool PanIsMove;
    [LabelText("主盘是否正在向下撞击")]
    public bool PanIsCrash;

    [Title("主盘边界线", HorizontalLine = true)]
    [LabelText("上")]
    public Transform upColl;
    [LabelText("下")]
    public Transform downColl;
    [LabelText("左")]
    public Transform leftColl;
    [LabelText("右")]
    public Transform rightColl;

    [Title("背景边界线", HorizontalLine = true)]
    [LabelText("上")]
    public Transform backUp;
    [LabelText("下")]
    public Transform backDown;
    [LabelText("左")]
    public Transform backLeft;
    [LabelText("右")]
    public Transform backRight;

    [Title("屏幕抖动设置")]
    [LabelText("屏幕抖动时间")]
    public float cameraShakeTime;
    [LabelText("屏幕抖动方向")]
    public Vector2 cameraShakePos;

    AudioSource scm;

    void Start()
    {
        MainGird = this.gameObject;      //定义主盘
        scm = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.instance.gameStart)    //游戏没开始不能移动
        {
            return;
        }
        CheckInput();   //移动判断
    }

    #region  主盘移动相关

    #region 键盘操作主盘移动

    public void CheckInput()         //判断键盘输入方向键，以决定主盘的移动方向和方块的生成地点
    {
        //以修改GM的全局方向来决定方向及其他关于方向的判断

        if (!PanIsMove)     //首先确认主盘不在移动状态（用布尔值标识）且没有方块在移动状态
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))          //判断方向键
            {
                GameManager.instance.dir = directions.down;      //确定全局方向
                GameManager.instance.TransState(GameManager.instance.pan);      //告知GM主盘移动
                ConfirmedFrontier(leftColl, rightColl, downColl, upColl, KeyCode.S, Vector2.down);     //通告GM部分参数
                CheckPanMove(GameManager.instance.dir);        //进行移动动作的执行
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                GameManager.instance.dir = directions.up;
                GameManager.instance.TransState(GameManager.instance.pan);      //告知GM主盘移动
                ConfirmedFrontier(leftColl, rightColl, upColl, downColl, KeyCode.W, Vector2.up);
                CheckPanMove(GameManager.instance.dir);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GameManager.instance.dir = directions.left;
                GameManager.instance.TransState(GameManager.instance.pan);      //告知GM主盘移动
                ConfirmedFrontier(upColl, downColl, leftColl, rightColl, KeyCode.A, Vector2.left);
                CheckPanMove(GameManager.instance.dir);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GameManager.instance.dir = directions.right;
                GameManager.instance.TransState(GameManager.instance.pan);      //告知GM主盘移动
                ConfirmedFrontier(upColl, downColl, rightColl, leftColl, KeyCode.D, Vector2.right);
                CheckPanMove(GameManager.instance.dir);
            }
        }
    }
    #endregion

    #region 移动执行动作

    public void CheckPanMove(directions directions)          //移动动作的执行
    {
        PanIsMove = true;    //标识主盘正在移动

        GameManager.instance.ResetColl();      //一开始移动主盘就先复原四周的边界

        if (GameManager.instance.grid != null)         //如果已经存在生成好的方块
        {
            GameManager.instance.grid.GetComponent<Grid>().BackAnim();     //告知方块退出界面
            GameManager.instance.grid.GetComponent<Grid>().gdDir = (gridDirections)GridSpawn.GetComponent<SpawnGris>().CheckGridDir(GameManager.instance.dir);//修改方块的方向
        }
        Tweener tweener = MainGird.transform.DOMove(new Vector3(0, 0, 0), 1f);     //将主盘位置归零，准备移动

        switch (directions)               //判断移动方向
        {
            case directions.up:
                tweener.OnComplete(MainGridMoveUpAnim);       //播放移动动画
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

    #region 主盘移动动画

    public void MainGridMoveDownAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveY(backDown.position.y - downColl.position.y, 2f);      //主盘移动到对应位置（使用背景coll定位）
        tweener.SetEase(Ease.InOutElastic);      //确定缓动动画效果
        GameManager.instance.spawnGridObj.GetComponent<SpawnGris>().SpawnNewGrid();      //通过GM告知生成器生成方块
        PanIsCrash = true;     //标记主盘正在下落
        Tweener camera = MainCamera.transform.DOShakePosition(cameraShakeTime, cameraShakePos).SetDelay(1f);     //屏幕震动效果
        camera.OnComplete(IsPanMove);        //动画结束后进行主盘结束移动后的操作
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
        PanIsCrash = false;  //标识撞击结束
        PanIsMove = false;  //标识移动结束
        scm.Play();
        GameManager.instance.downFrontier.gameObject.tag = "Ground";     //将定为下方的边界打地面tag
        for (int i = 0; i < GameManager.instance.gridListObj.transform.childCount; i++)       //将所有已落地的方块轴向冻结防止移动
        {
            GameManager.instance.gridListObj.transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        GameManager.instance.upFrontier.GetComponent<collCheck>().topColl = true;       //将定为上方的边界的超界射线判断打开
                                                                                        //GameManager.instance.TransState(GameManager.instance.destroyState);        //移动完后做一次消除判断
        GameManager.instance.TransState(GameManager.instance.tetris);     //移动结束后进入正常游戏的准备 
    }
    #endregion
    #endregion

    public void ConfirmedFrontier(Transform leftFrontier, Transform rightFrontier, Transform downFrontier, Transform upFrontier, KeyCode keyCode, Vector2 gridDropDir)        //告知GM各个功能参数
    {
        //确定上下左右的边界
        GameManager.instance.leftFrontier = leftFrontier;
        GameManager.instance.rightFrontier = rightFrontier;
        GameManager.instance.downFrontier = downFrontier;
        GameManager.instance.upFrontier = upFrontier;

        //确定下落键和下落方向
        GameManager.instance.keyCode = keyCode;
        GameManager.instance.powerDir = gridDropDir;

        //确定只有上方边界判断代码打开
        GameManager.instance.upFrontier.GetComponent<collCheck>().enabled = true;
        GameManager.instance.downFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.leftFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.rightFrontier.GetComponent<collCheck>().enabled = false;

        //重置边界的超界判断
        GameManager.instance.upFrontier.GetComponent<collCheck>().topColl = false;
        GameManager.instance.downFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.leftFrontier.GetComponent<collCheck>().enabled = false;
        GameManager.instance.rightFrontier.GetComponent<collCheck>().enabled = false;

        //依据整体方向改变判断消除的组件
        var gm = GameManager.instance;
        if (gm.dir == directions.up || gm.dir == directions.down)
        {
            gm.leftRightGridDesCheck.SetActive(false);
            gm.upDownGridDesCheck.SetActive(true);
            gm.currGridDesCheck = gm.upDownGridDesCheck;     //把选中的组件告诉GM
        }
        else if (gm.dir == directions.left || gm.dir == directions.right)
        {
            gm.leftRightGridDesCheck.SetActive(true);
            gm.upDownGridDesCheck.SetActive(false);
            gm.currGridDesCheck = gm.leftRightGridDesCheck;
        }
    }

    public void FristMove()       //进入游戏时的第0次移动（一开始）
    {
        GameManager.instance.isPanMove = true;     //告知GM开始移动
        GameManager.instance.dir = directions.down;      //确定全局方向（向下）
        ConfirmedFrontier(leftColl, rightColl, downColl, upColl, KeyCode.S, Vector2.down);     //使用通告告知GM各个功能参数
        CheckPanMove(GameManager.instance.dir);        //进行移动动作的执行
        GameManager.instance.isPanMove = false;     //告知GM结束移动
    }
}
