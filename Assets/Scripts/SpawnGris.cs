using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGris : SerializedMonoBehaviour
{
    [Title("�б�", HorizontalLine = true)]
    [LabelText("�����ɷ���")]
    public List<GameObject> gridList = new List<GameObject>();
    [LabelText("�����ɫ")]
    public List<Color> grisColors = new List<Color>();
    [LabelText("���ɵķ���")]
    public GameObject grid;
    //���ɷ����������
    public int currGridDir;

    public void SpawnNewGrid()
    {
        currGridDir = CheckGridDir(GameManager.instance.dir);   //��ͨ��ȫ�ַ��򶨷��鷽��
        Spawn();         //���ɷ��� 
        GridAnim();    //���·���Ķ���
    }

    public int CheckGridDir(directions directions)        //��ʶ���鷽��
    {
        var i = 0;
        switch (directions)
        {
            case directions.up:        //������
                i = 1;                          //������
                break;
            case directions.down:   //������
                i = 0;                         //������
                break;
            case directions.left:      //������
                i = 3;                         //������
                break;
            case directions.right:   //������
                i = 2;                         //������
                break;
            default:
                break;
        }
        return i;
    }

    public void Spawn()      //����һ�������ɫ�ķ���
    {
        if (GameManager.instance.grid == null || GameManager.instance.grid.GetComponent<Grid>().isGround)     //������������ɵķ���������ɵķ��������
        {
            //��ʼ���ɷ���
            var currColor = grisColors[Random.Range(0, grisColors.Count)];       //�������ɫ

            grid = Instantiate(gridList[Random.Range(0, gridList.Count)],
                                            transform.GetChild(currGridDir).position,
                                            gridList[Random.Range(0, gridList.Count)].transform.rotation);      //�����·���

            for (int i = 0; i < 4; i++)     //��ÿ��������ɫ
            {
                grid.transform.GetChild(i).GetComponent<SpriteRenderer>().color = currColor;
            }
            grid.GetComponent<Grid>().gdDir = (gridDirections)currGridDir;                  //��������λ��
            GameManager.instance.grid = grid;     //��¼��GM
        }
    }

    public void GridAnim()     //��������
    {
        grid.transform.position = transform.GetChild(currGridDir).position;     //ȷ������λ��

        switch (grid.GetComponent<Grid>().gdDir)       //�շ��鷽�򲥶���
        {
            case gridDirections.gdUp:
                Tweener tweener1 = grid.transform.DOMoveY(12f, 1f).From(true).SetDelay(0.5f);//��������
                tweener1.OnComplete(GridIsMove);
                break;
            case gridDirections.gdDown:
                Tweener tweener2 = grid.transform.DOMoveY(-17f, 1f).From(true).SetDelay(0.5f);
                tweener2.OnComplete(GridIsMove); //����������������˶�
                break;
            case gridDirections.gdLeft:
                Tweener tweener3 = grid.transform.DOMoveX(-7.1f, 1f).From(true).SetDelay(0.5f);
                tweener3.OnComplete(GridIsMove);

                break;
            case gridDirections.gdRight:
                Tweener tweener = grid.transform.DOMoveX(7.6f, 1f).From(true).SetDelay(0.5f);
                tweener.OnComplete(GridIsMove);
                break;
            default:
                break;
        }
    }

    void GridIsMove()      //����ִ�к�ִ��
    {
        grid.GetComponent<Grid>().IsMove = true;     //�ƶ���ѷ�����ƶ������
    }

}
