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
    public directions dir = directions.up;
    [LabelText("是否可以移动")]
    public bool IsMove;
    [LabelText("是否已落地")]
    public bool isDown;


    [Title("方向指定")]
    [LabelText("上")]
    public Transform upPoint;
    [LabelText("下")]
    public Transform downPoint;
    [LabelText("左")]
    public Transform leftPoint;
    [LabelText("右")]
    public Transform rightPoint;

    //grid每次移动的距离
    float gridMovePosX, gridMovePosY;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDown)
        {
            return;
        }

        if (IsMove)
        {
            IsMoveRange();
            StartCoroutine(Move());
        }

        gridMovePosX = 0;
        gridMovePosY = 0;


        checkInput();
    }

    private void checkInput()
    {
        switch (dir)
        {
            case directions.up:
                if (Input.GetKeyDown(KeyCode.S))
                {
                    GridGroupCintroller.instrance.upColl.enabled = false;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.AddForce(Vector2.down, ForceMode2D.Impulse);
                    isDown = true;
                }
                break;
            case directions.down:
                transform.DOLocalMoveY(-6f, 0.5f);
                break;
            case directions.left:
                transform.DOLocalMoveX(-3f, 0.5f);
                break;
            case directions.right:
                transform.DOLocalMoveX(3f, 0.5f);
                break;
            default:
                break;
        }
    }

    IEnumerator Move()
    {

        if (this.dir == directions.up || this.dir == directions.down)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                gridMovePosX = -0.214f;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                gridMovePosX = 0.214f;
            }
            transform.position = new Vector2(transform.position.x + gridMovePosX, transform.position.y);
        }
        else if (this.dir == directions.left || this.dir == directions.right)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                gridMovePosY = 0.214f;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                gridMovePosY = -0.214f;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + gridMovePosY);
        }

        yield return null;
    }

    public void BackAnim()    //退到外面的动画
    {
        switch (dir)
        {
            case directions.up:
                transform.DOLocalMoveY(6f, 0.5f);
                break;
            case directions.down:
                transform.DOLocalMoveY(-6f, 0.5f);
                break;
            case directions.left:
                transform.DOLocalMoveX(-3f, 0.5f);
                break;
            case directions.right:
                transform.DOLocalMoveX(3f, 0.5f);
                break;
            default:
                break;
        }
        IsMove = false;
    }


    public void IsMoveRange()      //判断grid是否在移动范围内
    {
        if (this.dir == directions.up || this.dir == directions.down)
        {
            if (leftPoint.position.x < GridGroupCintroller.instrance.leftColl.transform.position.x && rightPoint.position.x > GridGroupCintroller.instrance.rightColl.transform.position.x)
            {
                IsMove = false;
            }
        }
        else if (this.dir == directions.left || this.dir == directions.right)
        {
        }
    }

    public enum directions
    {
        up, down, left, right
    }

}
