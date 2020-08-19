using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : SerializedMonoBehaviour
{
    [Title("标记", HorizontalLine = true)]
    [LabelText("所在方位")]
    public gridDirections gdDir = gridDirections.gdUp;
    [LabelText("是否可以移动")]
    public bool IsMove;
    [LabelText("是否正在移动")]
    public bool IsMoving;
    [LabelText("是否已落地")]
    public bool isGround;
    [LabelText("落地面")]
    public Transform groundPoint;
    [LabelText("是否碰撞到物体")]
    public bool hasColl;

    //grid每次移动的距离
    float gridMovePosX, gridMovePosY;

    //初始化参数
    Rigidbody2D rb;

    void Start()
    {
        //初始化参数
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        groundSurface(GameManager.instance.dir);       //决定落地面以准备之后的判断

        if (isGround && IsMoving)       //如果落地/正在下落，则不再判断运动
        {
            return;
        }

        if (IsMove && !IsMoving)     //如果允许运动且没有正在移动
        {
            IsMoveRange();     //判断方块是否在移动范围内（存在GM的左右边界）
            StartCoroutine(Move());   //移动
        }

        //清除临时移动参数
        gridMovePosX = 0;
        gridMovePosY = 0;
    }

    public void FixedUpdate()
    {

    }

    IEnumerator Move()
    {

        if (this.gdDir == gridDirections.gdUp || this.gdDir == gridDirections.gdDown)    //如果方向是上下
        {
            //用AD控制方向

            if (Input.GetKeyDown(KeyCode.A))
            {
                gridMovePosX = -2;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                gridMovePosX = 2;
            }
            transform.position = new Vector2(transform.position.x + gridMovePosX, transform.position.y);     //方块移动
        }
        else if (this.gdDir == gridDirections.gdLeft || this.gdDir == gridDirections.gdRight)     //如果方向是左右
        {
            //用WS控制方向
            if (Input.GetKeyDown(KeyCode.W))
            {
                gridMovePosY = 2f;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                gridMovePosY = -2f;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + gridMovePosY);   //方块移动
        }

        yield return null;
    }

    public void BackAnim()    //退到外面的动画
    {
        switch (gdDir)        //判断方块方向
        {
            case gridDirections.gdUp:
                transform.DOMoveY(28f, 1f);     //执行退到外面的动画
                break;
            case gridDirections.gdDown:
                transform.DOMoveY(-31f, 0.5f);
                break;
            case gridDirections.gdLeft:
                transform.DOMoveX(-16.2f, 0.5f);
                break;
            case gridDirections.gdRight:
                transform.DOMoveX(14.8f, 0.5f);
                break;
            default:
                break;
        }
        IsMove = false;
    }

    public void IsMoveRange()      //判断grid是否在移动范围内
    {
    }

    public void groundSurface(directions dir)        //落地面选定
    {
        switch (dir)
        {
            case directions.up:
                groundPoint = GameManager.instance.upFrontier;
                break;
            case directions.down:
                groundPoint = GameManager.instance.downFrontier;
                break;
            case directions.left:
                groundPoint = GameManager.instance.leftFrontier;
                break;
            case directions.right:
                groundPoint = GameManager.instance.rightFrontier;
                break;
            default:
                break;
        }
    }

    public void Force()        //落地速度选定
    {
        switch (gdDir)
        {
            case gridDirections.gdUp:
                rb.AddForce(Vector2.down * 400f);
                break;
            case gridDirections.gdDown:
                rb.AddForce(Vector2.up * 400f);
                break;
            case gridDirections.gdLeft:
                rb.AddForce(Vector2.right * 400f);
                break;
            case gridDirections.gdRight:
                rb.AddForce(Vector2.left * 400f);
                break;
            default:
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector3.zero;
        hasColl = true;
    }
}

public enum gridDirections      //方块方向
{
    gdUp, gdDown, gdLeft, gdRight
}