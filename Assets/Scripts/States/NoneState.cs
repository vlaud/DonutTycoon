using UnityEngine;

public class NoneState : iState
{
    private Customer customer;

    public NoneState(Customer customer)
    {
        this.customer = customer;
    }

    private Color meshColor = Color.white;
    public Color MeshColor { get => meshColor; set => meshColor = value; }

    public void Enter()
    {

    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }

    public void OnMessage(Telegram telegram)
    {

    }
}
