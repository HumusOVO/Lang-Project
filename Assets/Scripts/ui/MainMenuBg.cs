using System.Collections.Generic;
using UnityEngine;

public class MainMenuBg : MonoBehaviour
{
    /// <summary>
    /// 负责处理主界面背景特效
    /// </summary>

    public List<Color> grisColors = new List<Color>();     //方块颜色
    public List<GameObject> gridList = new List<GameObject>();     //方块集合
    float currTime;    //方块下落时间间隔

    void Start()
    {
        currTime = Time.time;         //记录时间
        if (PlayerPrefs.HasKey("bestscore"))    //如果有存储最高分
        {
            UIManager.instrance.bestscoreAdd();      //告知UIM更新分数
        }
    }

    void Update()
    {
        if (Time.time > currTime)      //如果现在的时间大于记录的时间
        {
            Spawn();     //生成方块
            currTime = Time.time + Random.Range(0.1f, 2f);     //记录新的间隔时间
        }
    }

    public void Spawn()
    {
        //开始生成方块
        var currColor = grisColors[Random.Range(0, grisColors.Count)];       //定随机颜色

        var grid = Instantiate(gridList[Random.Range(0, gridList.Count)],
                                        new Vector2(Random.Range(-9f, 9f), 30f),
                                        gridList[Random.Range(0, gridList.Count)].transform.rotation);      //随机位置生成新方块

        for (int i = 0; i < 4; i++)     //给每个格子上色
        {
            grid.transform.GetChild(i).GetComponent<SpriteRenderer>().color = currColor;
        }
        grid.GetComponent<Grid>().enabled = false;   //关掉代码
        grid.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1, 4);     //设置范围内重力
    }
    public void OnTriggerExit2D(Collider2D collision)         //判断如果有方块掉出范围则消除方块
    {
        Destroy(collision.gameObject.transform.parent.gameObject);
    }
}
