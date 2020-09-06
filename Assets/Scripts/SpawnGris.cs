using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGris : SerializedMonoBehaviour
{
    [Title("方块参数", HorizontalLine = true)]
    [LabelText("方块集合")]
    public List<GameObject> gridList = new List<GameObject>();
    [LabelText("颜色集合")]
    public List<Color> grisColors = new List<Color>();
    [LabelText("生成方块临时存储")]
    public GameObject grid;
    [LabelText("生成方块方向")]
    public int currGridDir;

    public void SpawnNewGrid()          //供外部调用的整体生成方块方法
    {
        if (GameManager.instance.grid == null || GameManager.instance.grid.GetComponent<Grid>().isGround)     //如果不存在生成的方块或已生成的方块已落地
        {
            currGridDir = CheckGridDir(GameManager.instance.dir);   //先通过全局方向定方块方向
            Spawn();         //生成方块 
            GameManager.instance.grid.GetComponent<Grid>().GridAnim();//放下方块的动画
        }

        if (GameManager.instance.grid != null && !GameManager.instance.grid.GetComponent<Grid>().IsMove)     //如果存在生成方块且可移动
        {
            GameManager.instance.grid.GetComponent<Grid>().GridAnim();//放下方块的动画
        }
    }

    public int CheckGridDir(directions directions)        //标识方块方向
    {
        var i = 0;
        switch (directions)
        {
            case directions.up:        //主盘上
                i = 1;                          //方块下
                break;
            case directions.down:   //主盘下
                i = 0;                         //方块上
                break;
            case directions.left:      //主盘左
                i = 3;                         //方块右
                break;
            case directions.right:   //主盘右
                i = 2;                         //方块左
                break;
            default:
                break;
        }
        return i;
    }

    public void Spawn()      //生成一个随机颜色的方块
    {
        //开始生成方块
        var currColor = grisColors[Random.Range(0, grisColors.Count)];       //定随机颜色

        grid = Instantiate(gridList[Random.Range(0, gridList.Count)],
                                        transform.GetChild(currGridDir).position,
                                        gridList[Random.Range(0, gridList.Count)].transform.rotation);      //生成新方块

        for (int i = 0; i < 4; i++)     //给每个格子上色
        {
            grid.transform.GetChild(i).GetComponent<SpriteRenderer>().color = currColor;
        }
        grid.GetComponent<Grid>().gdDir = (gridDirections)currGridDir;                  //给方块标记位置
        GameManager.instance.grid = grid;     //记录给GM
    }
}
