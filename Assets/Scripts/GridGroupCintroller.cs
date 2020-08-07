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



    [Title("���", HorizontalLine = true)]
    [LabelText("����")]
    [PreviewField]
    public GameObject MainGird;
    [LabelText("�����")]
    public Camera MainCamera;
    [LabelText("����Grid���")]
    public GameObject GridSpawn;

    [Title("�жϲ���", HorizontalLine = true)]
    [LabelText("�����Ƿ������ƶ�")]
    public bool PanIsMove;

    [Title("�߽���", HorizontalLine = true)]
    [LabelText("��")]
    public Collider2D upColl;
    [LabelText("��")]
    public Collider2D downColl;
    [LabelText("��")]
    public Collider2D leftColl;
    [LabelText("��")]
    public Collider2D rightColl;

    // Start is called before the first frame update
    void Start()
    {
        MainGird = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();   //�ƶ��ж�
    }

    #region  �����ƶ����

    #region ���̲��������ƶ�

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

    #region �ƶ�ִ�ж���

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

    #region �����ƶ�����

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
