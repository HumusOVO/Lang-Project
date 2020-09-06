
public class DestroyGridState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.isDesGrid = true;    //告知GM开始进行删除
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        for (int i = 0; i < gm.currGridDesCheck.transform.childCount; i++)          //循环消除组件下的子物体
        {
            gm.currGridDesCheck.transform.GetChild(i).GetComponent<CheckPhyCountRay>().CheckPointRay();      //告知生成射线判断
        }
    }

    public override void OnUpdate(GameManager gm)
    {
        for (int i = 0; i < gm.currGridDesCheck.transform.childCount; i++)      //循环消除组件下的子物体
        {
            gm.currGridDesCheck.transform.GetChild(i).GetComponent<CheckPhyCountRay>().DrawCheckLink();        //告知进行消除判断
        }
    }

}
