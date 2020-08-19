using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : SerializedMonoBehaviour
{
    [Title("���", HorizontalLine = true)]
    [LabelText("���ڷ�λ")]
    public gridDirections gdDir = gridDirections.gdUp;
    [LabelText("�Ƿ�����ƶ�")]
    public bool IsMove;
    [LabelText("�Ƿ������ƶ�")]
    public bool IsMoving;
    [LabelText("�Ƿ������")]
    public bool isGround;
    [LabelText("�����")]
    public Transform groundPoint;
    [LabelText("�Ƿ���ײ������")]
    public bool hasColl;

    //gridÿ���ƶ��ľ���
    float gridMovePosX, gridMovePosY;

    //��ʼ������
    Rigidbody2D rb;

    void Start()
    {
        //��ʼ������
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        groundSurface(GameManager.instance.dir);       //�����������׼��֮����ж�

        if (isGround && IsMoving)       //������/�������䣬�����ж��˶�
        {
            return;
        }

        if (IsMove && !IsMoving)     //��������˶���û�������ƶ�
        {
            IsMoveRange();     //�жϷ����Ƿ����ƶ���Χ�ڣ�����GM�����ұ߽磩
            StartCoroutine(Move());   //�ƶ�
        }

        //�����ʱ�ƶ�����
        gridMovePosX = 0;
        gridMovePosY = 0;
    }

    public void FixedUpdate()
    {

    }

    IEnumerator Move()
    {

        if (this.gdDir == gridDirections.gdUp || this.gdDir == gridDirections.gdDown)    //�������������
        {
            //��AD���Ʒ���

            if (Input.GetKeyDown(KeyCode.A))
            {
                gridMovePosX = -2;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                gridMovePosX = 2;
            }
            transform.position = new Vector2(transform.position.x + gridMovePosX, transform.position.y);     //�����ƶ�
        }
        else if (this.gdDir == gridDirections.gdLeft || this.gdDir == gridDirections.gdRight)     //�������������
        {
            //��WS���Ʒ���
            if (Input.GetKeyDown(KeyCode.W))
            {
                gridMovePosY = 2f;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                gridMovePosY = -2f;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + gridMovePosY);   //�����ƶ�
        }

        yield return null;
    }

    public void BackAnim()    //�˵�����Ķ���
    {
        switch (gdDir)        //�жϷ��鷽��
        {
            case gridDirections.gdUp:
                transform.DOMoveY(28f, 1f);     //ִ���˵�����Ķ���
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

    public void IsMoveRange()      //�ж�grid�Ƿ����ƶ���Χ��
    {
    }

    public void groundSurface(directions dir)        //�����ѡ��
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

    public void Force()        //����ٶ�ѡ��
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

public enum gridDirections      //���鷽��
{
    gdUp, gdDown, gdLeft, gdRight
}