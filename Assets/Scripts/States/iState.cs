public interface iState : IColorable
{
    void Enter();
    void Execute();
    void Exit();
    void OnMessage(Telegram telegram);
}