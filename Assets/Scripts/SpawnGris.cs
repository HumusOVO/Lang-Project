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
    //生成方块待定方向
    public int currGridDir;

    public void SpawnNewGrid()
    {
        currGridDir = CheckGridDir(GameManager.instance.dir);   //先通过全局方向定方块方向
        Spawn();         //生成方块 
        GridAnim();    //放下方块的动画
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
        if (GameManager.instance.grid == null || GameManager.instance.grid.GetComponent<Grid>().isGround)     //如果不存在生成的方块或已生成的方块已落地
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

    public void GridAnim()     //进场动画
    {
        grid.transform.position = transform.GetChild(currGridDir).position;     //确定方块位置

        switch (grid.GetComponent<Grid>().gdDir)       //照方块方向播动画
        {
            case gridDirections.gdUp:
                Tweener tweener1 = grid.transform.DOMoveY(12f, 1f).From(true).SetDelay(0.5f);//进场动画
                tweener1.OnComplete(GridIsMove);
                break;
            case gridDirections.gdDown:
                Tweener tweener2 = grid.transform.DOMoveY(-17f, 1f).From(true).SetDelay(0.5f);
                tweener2.OnComplete(GridIsMove); //动画播完后允许方块运动
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

    void GridIsMove()      //动画执行后执行
    {
        grid.GetComponent<Grid>().IsMove = true;     //移动完把方块的移动允许打开
    }

}
