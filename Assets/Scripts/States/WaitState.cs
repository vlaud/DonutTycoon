using UnityEngine;

public class WaitState : iState
{
    private Customer customer;

    public WaitState(Customer customer)
    {
        this.customer = customer;
    }

    private Color meshColor = Color.green;
    public Color MeshColor { get => meshColor; set => meshColor = value; }

    public void Enter()
    {

    }

    public void Execute()
    {
        // Check if the customer has been clicked
        if (customer.IsClicked)
        {
            // Change state to Clicked
            customer.ChangeState(CustomerState.Clicked);
        }
    }

    public void Exit()
    {

    }

    public void OnMessage(Telegram telegram)
    {

    }
}
