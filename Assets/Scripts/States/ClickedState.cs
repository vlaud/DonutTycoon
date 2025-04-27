using UnityEngine;

public class ClickedState : iState
{
    private Customer customer;

    public ClickedState(Customer customer)
    {
        this.customer = customer;
    }

    private Color meshColor = Color.yellow;
    public Color MeshColor { get => meshColor; set => meshColor = value; }

    public void Enter()
    {
        Gamemanager.GetUIController.ShowOrderMenu(customer.CustomerInfo);
    }

    public void Execute()
    {
        // Check if the customer has been clicked again
        if (!customer.IsClicked)
        {
            // Change state to Leave
            customer.ChangeState(CustomerState.Wait);
        }
    }

    public void Exit()
    {
        Debug.Log("ClickedState Exit: " + customer.CustomerInfo);
        Gamemanager.GetClickManager.NotifyClickMissed();
        Gamemanager.GetUIController.HideOrderMenu();
    }

    public void OnMessage(Telegram telegram)
    {

    }
}
