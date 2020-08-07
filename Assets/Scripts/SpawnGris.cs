using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGris : SerializedMonoBehaviour
{
    [Title("列表", HorizontalLine = true)]
    [LabelText("待生成方块")]
    public List<GameObject> gridList = new List<GameObject>();
    [LabelText("随机颜色")]
    public List<Color> grisColors = new List<Color>();
    [LabelText("生成的方块")]
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
    public void Spawn(float moveIndex)      //生成一个随机颜色的方块
    {
        currMoveIndex = moveIndex;

        if (grid != null)     //如果存在生成的方块
        {
            return;   //不生成新方块
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

    public void GridAnim(float index)     //进场动画
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
