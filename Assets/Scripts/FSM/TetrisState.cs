
public class TetrisState : EasePotralState
{
    public override void EnterPotral(GameManager gm)
    {
        gm.upFrontier.gameObject.SetActive(true);     //关掉上面的边界令方块能够下落
    }

    public override void OnFixedUpdate(GameManager gm)
    {
        var grid = gm.grid;      //登记临时变量(避免语句过长) 

        if (gm.currState == gm.pan || gm.isPanMove)       //如果主盘正在移动状态/移动标识为真则跳过监测
        {
            return;
        }
    }

    public override void OnUpdate(GameManager gm)
    {

    }
}
