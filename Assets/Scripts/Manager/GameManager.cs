using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{

    /// <summary>
    /// 负责管理所有全局参数
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

    [Title("需记录的参数", HorizontalLine = true)]
    //方向的判断以主盘位置为准，例：整体方向的下=主盘在下，方块在上面生成，向下掉落
    [LabelText("整体方向")]
    public directions dir = directions.down;
    [LabelText("目前控制的方块")]
    public GameObject grid;
    [LabelText("左边界")]
    public Transform leftFrontier;
    [LabelText("右边界")]
    public Transform rightFrontier;
    [LabelText("下边界")]
    public Transform downFrontier;
    [LabelText("上边界")]
    public Transform upFrontier;
    [LabelText("下落按钮")]
    public KeyCode keyCode;
    [LabelText("方块移动方向")]
    public Vector2 powerDir;
    [LabelText("方块下落力度")]
    public float gridMovePower;
    [LabelText("当前负责清除判断的组件")]
    public GameObject currGridDesCheck;

    [Title("控制状态参数")]
    [LabelText("当前状态")]
    public EasePotralState currState;
    public StartGameState start = new StartGameState();     //开始状态
    public PanMoveState pan = new PanMoveState();     //移动状态
    public TetrisState tetris = new TetrisState();    //游戏状态
    public DestroyGridState destroyState = new DestroyGridState();    //消除方块状态
    [LabelText("游戏是否在开始状态")]
    public bool gameStart;
    [LabelText("游戏是否在结束状态")]
    public bool gameOver;
    [LabelText("主盘是否在移动")]
    public bool isPanMove;
    [LabelText("主盘是否在游戏状态")]
    public bool isTetris;
    [LabelText("主盘是否在消除状态")]
    public bool isDesGrid;

    [Title("组件")]
    [LabelText("主盘")]
    public GameObject mainPan;
    [LabelText("生成器")]
    public GameObject spawnGridObj;
    [LabelText("已落下方块的集合")]
    public List<GameObject> grids = new List<GameObject>();
    [LabelText("存放器")]
    public GameObject gridListObj;
    [LabelText("负责整体上下方向的清除判断")]
    public GameObject upDownGridDesCheck;
    [LabelText("负责整体左右方向的清除判断")]
    public GameObject leftRightGridDesCheck;


    public void Start()
    {
        TransState(start);        //开始此场景时先进入开始状态（主盘落下）
    }

    public void Update()
    {
        currState.OnUpdate(this);      //执行状态的update内容
    }

    public void FixedUpdate()
    {
        currState.OnFixedUpdate(this);     //执行状态的FixedUpdate内容
    }

    public void TransState(EasePotralState state)        //改变状态
    {
        currState = state;     //向GM登记现在的状态
        currState.EnterPotral(this);    //执行进入状态时先处理的东西
    }

    public void ResetColl()        //复原边界（移动时使用）
    {
        //将所有的边界重新开启
        upFrontier.gameObject.SetActive(true);
        downFrontier.gameObject.SetActive(true);
        leftFrontier.gameObject.SetActive(true);
        rightFrontier.gameObject.SetActive(true);
    }


    public void DesCheckEnd()
    {
        //关闭消除判断的obj
        GameManager.instance.isDesGrid = false;
        TransState(tetris);//继续游戏状态
    }

}

public enum directions   //作为标记整体方向的枚举
{
    up, down, left, right
}
