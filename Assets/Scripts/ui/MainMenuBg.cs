using System.Collections.Generic;
using UnityEngine;

public class MainMenuBg : MonoBehaviour
{
    /// <summary>
    /// �����������汳����Ч
    /// </summary>

    public List<Color> grisColors = new List<Color>();     //������ɫ
    public List<GameObject> gridList = new List<GameObject>();     //���鼯��
    float currTime;    //��������ʱ����

    void Start()
    {
        currTime = Time.time;         //��¼ʱ��
        if (PlayerPrefs.HasKey("bestscore"))    //����д洢��߷�
        {
            UIManager.instrance.bestscoreAdd();      //��֪UIM���·���
        }
    }

    void Update()
    {
        if (Time.time > currTime)      //������ڵ�ʱ����ڼ�¼��ʱ��
        {
            Spawn();     //���ɷ���
            currTime = Time.time + Random.Range(0.1f, 2f);     //��¼�µļ��ʱ��
        }
    }

    public void Spawn()
    {
        //��ʼ���ɷ���
        var currColor = grisColors[Random.Range(0, grisColors.Count)];       //�������ɫ

        var grid = Instantiate(gridList[Random.Range(0, gridList.Count)],
                                        new Vector2(Random.Range(-9f, 9f), 30f),
                                        gridList[Random.Range(0, gridList.Count)].transform.rotation);      //���λ�������·���

        for (int i = 0; i < 4; i++)     //��ÿ��������ɫ
        {
            grid.transform.GetChild(i).GetComponent<SpriteRenderer>().color = currColor;
        }
        grid.GetComponent<Grid>().enabled = false;   //�ص�����
        grid.GetComponent<Rigidbody2D>().gravityScale = Random.Range(1, 4);     //���÷�Χ������
    }
    public void OnTriggerExit2D(Collider2D collision)         //�ж�����з��������Χ����������
    {
        Destroy(collision.gameObject.transform.parent.gameObject);
    }
}
