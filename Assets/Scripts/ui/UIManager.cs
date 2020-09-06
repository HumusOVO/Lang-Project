using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SerializedMonoBehaviour
{
    /// <summary>
    /// 负责UI整体调用
    /// </summary>

    #region Simgle
    public static UIManager instrance;


    public void Awake()
    {
        if (instrance != null)
        {
            Destroy(this);
        }

        instrance = this;
    }
    #endregion

    [Title("菜单组件", HorizontalLine = true)]
    [LabelText("帮助菜单")]
    public GameObject helpMenu;
    [LabelText("暂停菜单")]
    public GameObject pauseMenu;
    [LabelText("结束菜单")]
    public GameObject overMenu;

    [Title("按钮组件", HorizontalLine = true)]
    [LabelText("开始游戏按钮")]
    public GameObject startGameButton;
    [LabelText("帮助界面关闭按钮")]
    public GameObject helpCloceButton;

    [Title("文字组件", HorizontalLine = true)]
    [LabelText("分数")]
    public TextMeshProUGUI score;
    [LabelText("最高分数")]
    public TextMeshProUGUI bestscore;

    public void scoreAdd()        //更新分数
    {
        score.text = GameManager.instance.score.ToString();
    }
    public void bestscoreAdd()   //更新最高分数
    {
        if (PlayerPrefs.HasKey("bestscore"))     //如果存在存储的最高分
        {
            var t = PlayerPrefs.GetInt("bestscore");       //取出存储的数
            bestscore.text = t.ToString();    //更新到UI上
        }
    }

    public void LoadGameScene()     //进入游戏界面
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void StartGame()       //开始游戏
    {
        startGameButton.SetActive(false);   //关闭帮助界面的开始按钮
        helpCloceButton.SetActive(true);      //打开帮助界面的关闭按钮
        Time.timeScale = 1;      //游戏时间复原
        helpMenu.SetActive(false);      //关闭帮助界面
        GameManager.instance.gameStart = true;      //告知GM游戏开始
        GameManager.instance.bgm.Play();
        GameManager.instance.TransState(GameManager.instance.start);        //开始此场景时先进入开始状态（主盘落下）
    }

    public void HelpMenu()    //帮助界面
    {
        if (!GameManager.instance.gameStart)     //如果游戏没有运行则不显示
        {
            return;
        }
        Time.timeScale = 0;     //冻结时间
        helpMenu.SetActive(true);      //打开界面
    }
    public void PauseMenu()        //如果游戏没运行则不显示
    {
        if (!GameManager.instance.gameStart)
        {
            return;
        }
        Time.timeScale = 0;    //冻结时间
        pauseMenu.SetActive(true);     //打开界面
    }

    public void RestartMenu()     //重开游戏
    {
        Time.timeScale = 1;    //时间复原
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      //重新加载场景
        StartGame();     //执行开始游戏的方法
    }

    public void ResumeMenu()     //返回游戏
    {
        Time.timeScale = 1;    //时间复原
        pauseMenu.SetActive(false);   //关闭界面
    }

    public void MainMenu()    //回到主界面
    {
        SceneManager.LoadScene(0);     //加载主界面场景
    }

    public void CloseHelp()      //关闭帮助界面
    {
        Time.timeScale = 1;   //时间复原
        helpMenu.SetActive(false);    //关闭界面
    }

    public void GameOver()      //游戏结束（UI处理）
    {
        Time.timeScale = 0;     //冻结时间
        overMenu.SetActive(true);     //打开结束界面
        GameManager.instance.GameOver();    //告知GM处理结束
    }

    public void QuitMenu()     //退出游戏
    {
        Application.Quit();
    }
}
