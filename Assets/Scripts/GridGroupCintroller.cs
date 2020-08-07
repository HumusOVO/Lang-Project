using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridGroupCintroller : SerializedMonoBehaviour
{
    #region Simgle
    public static GridGroupCintroller instrance;

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
    [LabelText("生成Grid组件")]
    public GameObject GridSpawn;

    [Title("判断参数", HorizontalLine = true)]
    [LabelText("主盘是否正在移动")]
    public bool PanIsMove;

    [Title("边界线", HorizontalLine = true)]
    [LabelText("上")]
    public Collider2D upColl;
    [LabelText("下")]
    public Collider2D downColl;
    [LabelText("左")]
    public Collider2D leftColl;
    [LabelText("右")]
    public Collider2D rightColl;

    // Start is called before the first frame update
    void Start()
    {
        MainGird = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();   //移动判断
    }

    #region  主盘移动相关

    #region 键盘操作主盘移动

    public void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow) && !PanIsMove)
        {
            CheckInputDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !PanIsMove)
        {
            CheckInputUp();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !PanIsMove)
        {
            CheckInputLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !PanIsMove)
        {
            CheckInputRight();
        }
    }

    #endregion

    #region 移动执行动作

    public void CheckInputDown()
    {
        if (GridSpawn.GetComponent<SpawnGris>().grid != null)
        {
            GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().BackAnim();
        }
        Tweener tweener = MainGird.transform.DOMove(new Vector3(0, 0, 0), 1f);
        tweener.OnComplete(MainGridMoveDownAnim);

    }
    public void CheckInputUp()
    {
        if (GridSpawn.GetComponent<SpawnGris>().grid != null)
        {
            GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().BackAnim();
        }
        Tweener tweener = MainGird.transform.DOMove(new Vector3(0, 0, 0), 1f);
        tweener.OnComplete(MainGridMoveUpAnim);
    }
    public void CheckInputLeft()
    {
        if (GridSpawn.GetComponent<SpawnGris>().grid != null)
        {
            GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().BackAnim();
        }
        Tweener tweener = MainGird.transform.DOMove(new Vector3(0, 0, 0), 1f);
        tweener.OnComplete(MainGridMoveLeftAnim);
    }
    public void CheckInputRight()
    {
        if (GridSpawn.GetComponent<SpawnGris>().grid != null)
        {
            GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().BackAnim();
        }
        Tweener tweener = MainGird.transform.DOMove(new Vector3(0, 0, 0), 1f);
        tweener.OnComplete(MainGridMoveRightAnim);
    }

    #endregion

    #region 主盘移动动画

    public void MainGridMoveDownAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveY(-1.12f, 2f);
        tweener.SetEase(Ease.InOutElastic);

        Tweener camera = MainCamera.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0)).SetDelay(1f);
        camera.OnComplete(IsPanMove);

        GridSpawn.GetComponent<SpawnGris>().Spawn(1);
        GridSpawn.GetComponent<SpawnGris>().GridAnim(1);
        GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().dir = Grid.directions.up;

    }
    public void MainGridMoveUpAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveY(1.08f, 2f);
        tweener.SetEase(Ease.InOutElastic);

        Tweener camera = MainCamera.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0)).SetDelay(1f);
        camera.OnComplete(IsPanMove);
        GridSpawn.GetComponent<SpawnGris>().Spawn(2);
        GridSpawn.GetComponent<SpawnGris>().GridAnim(2);
        GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().dir = Grid.directions.down;

    }
    public void MainGridMoveLeftAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveX(-0.44f, 2f);
        tweener.SetEase(Ease.InOutElastic);

        Tweener camera = MainCamera.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0)).SetDelay(1f);
        camera.OnComplete(IsPanMove);
        GridSpawn.GetComponent<SpawnGris>().Spawn(4);
        GridSpawn.GetComponent<SpawnGris>().GridAnim(4);
        GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().dir = Grid.directions.right;

    }
    public void MainGridMoveRightAnim()
    {
        Tweener tweener = MainGird.transform.DOMoveX(0.45f, 2f);
        tweener.SetEase(Ease.InOutElastic);

        Tweener camera = MainCamera.transform.DOShakePosition(1f, new Vector3(0.2f, 0.2f, 0)).SetDelay(1f);
        camera.OnComplete(IsPanMove);
        GridSpawn.GetComponent<SpawnGris>().Spawn(3);
        GridSpawn.GetComponent<SpawnGris>().GridAnim(3);
        GridSpawn.GetComponent<SpawnGris>().grid.GetComponent<Grid>().dir = Grid.directions.left;

    }

    void IsPanMove()
    {
        PanIsMove = false;
    }


    #endregion
    #endregion




}
