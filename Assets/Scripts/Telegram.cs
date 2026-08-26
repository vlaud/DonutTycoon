public struct Telegram
{
    public float dispatchTime;
    public string sender;
    public string receiver;
    public string message;
    public CustomerState state;

    public void SetTelegram(float time, string sender, string receiver, string message)
    {
        this.dispatchTime = time;
        this.sender = sender;
        this.receiver = receiver;
        this.message = message;
    }

    public void SetNextState(CustomerState state)
    {
        this.state = state;
    }
}