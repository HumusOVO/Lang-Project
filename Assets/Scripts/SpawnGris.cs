using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGris : SerializedMonoBehaviour
{
    [Title("�������", HorizontalLine = true)]
    [LabelText("���鼯��")]
    public List<GameObject> gridList = new List<GameObject>();
    [LabelText("��ɫ����")]
    public List<Color> grisColors = new List<Color>();
    [LabelText("���ɷ�����ʱ�洢")]
    public GameObject grid;
    [LabelText("���ɷ��鷽��")]
    public int currGridDir;

    public void SpawnNewGrid()          //���ⲿ���õ��������ɷ��鷽��
    {
        if (GameManager.instance.grid == null || GameManager.instance.grid.GetComponent<Grid>().isGround)     //������������ɵķ���������ɵķ��������
        {
            currGridDir = CheckGridDir(GameManager.instance.dir);   //��ͨ��ȫ�ַ��򶨷��鷽��
            Spawn();         //���ɷ��� 
            GameManager.instance.grid.GetComponent<Grid>().GridAnim();//���·���Ķ���
        }

        if (GameManager.instance.grid != null && !GameManager.instance.grid.GetComponent<Grid>().IsMove)     //����������ɷ����ҿ��ƶ�
        {
            GameManager.instance.grid.GetComponent<Grid>().GridAnim();//���·���Ķ���
        }
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
