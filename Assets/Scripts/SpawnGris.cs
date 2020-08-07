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

    float currMoveIndex;

    public void Update()
    {
        if (grid != null && grid.GetComponent<Grid>().isDown)
        {
            grid = null;
        }
    }

    // Start is called before the first frame update
    public void Spawn(float moveIndex)      //����һ�������ɫ�ķ���
    {
        currMoveIndex = moveIndex;

        if (grid != null)     //����������ɵķ���
        {
            return;   //�������·���
        }

        var currColor = grisColors[Random.Range(0, grisColors.Count)];

        grid = Instantiate(gridList[Random.Range(0, gridList.Count)],
                                        transform.GetChild((int)moveIndex - 1).position,
                                        gridList[Random.Range(0, gridList.Count)].transform.rotation);

        for (int i = 0; i < 4; i++)
        {
            grid.transform.GetChild(1).GetChild(i).GetComponent<SpriteRenderer>().color = currColor;
        }

        //GridAnim(moveIndex);
    }

    public void GridAnim(float index)     //��������
    {
        grid.transform.position = transform.GetChild((int)index - 1).position;
        switch (index)
        {
            case 1:
                grid.transform.DOMoveY(3f, 1f).From(true).SetDelay(0.5f);
                grid.GetComponent<Grid>().dir = Grid.directions.up;
                break;
            case 2:
                grid.transform.DOMoveY(-3f, 1f).From(true).SetDelay(0.5f);
                grid.GetComponent<Grid>().dir = Grid.directions.down;
                break;
            case 3:
                grid.transform.DOMoveX(-2f, 1f).From(true).SetDelay(0.5f);
                grid.GetComponent<Grid>().dir = Grid.directions.right;
                break;
            case 4:
                grid.transform.DOMoveX(2f, 1f).From(true).SetDelay(0.5f);
                grid.GetComponent<Grid>().dir = Grid.directions.left;
                break;

            default:
                break;
        }

        if (grid != null)
        {
            grid.GetComponent<Grid>().IsMove = true;
        }
    }

}
