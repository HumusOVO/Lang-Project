using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SerializedMonoBehaviour
{
    /// <summary>
    /// ����UI�������
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

    [Title("�˵����", HorizontalLine = true)]
    [LabelText("�����˵�")]
    public GameObject helpMenu;
    [LabelText("��ͣ�˵�")]
    public GameObject pauseMenu;
    [LabelText("�����˵�")]
    public GameObject overMenu;

    [Title("��ť���", HorizontalLine = true)]
    [LabelText("��ʼ��Ϸ��ť")]
    public GameObject startGameButton;
    [LabelText("��������رհ�ť")]
    public GameObject helpCloceButton;

    [Title("�������", HorizontalLine = true)]
    [LabelText("����")]
    public TextMeshProUGUI score;
    [LabelText("��߷���")]
    public TextMeshProUGUI bestscore;

    public void scoreAdd()        //���·���
    {
        score.text = GameManager.instance.score.ToString();
    }
    public void bestscoreAdd()   //������߷���
    {
        if (PlayerPrefs.HasKey("bestscore"))     //������ڴ洢����߷�
        {
            var t = PlayerPrefs.GetInt("bestscore");       //ȡ���洢����
            bestscore.text = t.ToString();    //���µ�UI��
        }
    }

    public void LoadGameScene()     //������Ϸ����
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void StartGame()       //��ʼ��Ϸ
    {
        startGameButton.SetActive(false);   //�رհ�������Ŀ�ʼ��ť
        helpCloceButton.SetActive(true);      //�򿪰�������Ĺرհ�ť
        Time.timeScale = 1;      //��Ϸʱ�临ԭ
        helpMenu.SetActive(false);      //�رհ�������
        GameManager.instance.gameStart = true;      //��֪GM��Ϸ��ʼ
        GameManager.instance.bgm.Play();
        GameManager.instance.TransState(GameManager.instance.start);        //��ʼ�˳���ʱ�Ƚ��뿪ʼ״̬���������£�
    }

    public void HelpMenu()    //��������
    {
        if (!GameManager.instance.gameStart)     //�����Ϸû����������ʾ
        {
            return;
        }
        Time.timeScale = 0;     //����ʱ��
        helpMenu.SetActive(true);      //�򿪽���
    }
    public void PauseMenu()        //�����Ϸû��������ʾ
    {
        if (!GameManager.instance.gameStart)
        {
            return;
        }
        Time.timeScale = 0;    //����ʱ��
        pauseMenu.SetActive(true);     //�򿪽���
    }

    public void RestartMenu()     //�ؿ���Ϸ
    {
        Time.timeScale = 1;    //ʱ�临ԭ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      //���¼��س���
        StartGame();     //ִ�п�ʼ��Ϸ�ķ���
    }

    public void ResumeMenu()     //������Ϸ
    {
        Time.timeScale = 1;    //ʱ�临ԭ
        pauseMenu.SetActive(false);   //�رս���
    }

    public void MainMenu()    //�ص�������
    {
        SceneManager.LoadScene(0);     //���������泡��
    }

    public void CloseHelp()      //�رհ�������
    {
        Time.timeScale = 1;   //ʱ�临ԭ
        helpMenu.SetActive(false);    //�رս���
    }

    public void GameOver()      //��Ϸ������UI����
    {
        Time.timeScale = 0;     //����ʱ��
        overMenu.SetActive(true);     //�򿪽�������
        GameManager.instance.GameOver();    //��֪GM�������
    }

    public void QuitMenu()     //�˳���Ϸ
    {
        Application.Quit();
    }
}
