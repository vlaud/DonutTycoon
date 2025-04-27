using UnityEngine;

public class EnterState : iState
{
    private Customer customer;

    public EnterState(Customer customer)
    {
        this.customer = customer;
    }

    private Color meshColor = Color.blue;
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
