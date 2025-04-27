using UnityEngine;

public class LeaveState : iState
{
    private Customer customer;

    public LeaveState(Customer customer)
    {
        this.customer = customer;
    }

    private Color meshColor = Color.gray;
    public Color MeshColor { get => meshColor; set => meshColor = value; }

    public void Enter()
    {
        Gamemanager.GetInLineManager.RemoveCustomer(customer);
        customer.SetMoveTarget(Gamemanager.GetCustomerSpawner.Dismisspos);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
        Gamemanager.GetCustomerSpawner.RemoveCustomer(customer);
    }

    public void OnMessage(Telegram telegram)
    {

    }
}
