public abstract class EasePotralState
{
    public abstract void EnterPotral(GameManager gm);      //开始时执行的方法
    public abstract void OnUpdate(GameManager gm);        //update时执行的方法
    public abstract void OnFixedUpdate(GameManager gm);     //fixedUpdate时执行的方法
}
