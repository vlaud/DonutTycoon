using UnityEngine;

/// <summary>
/// 이것은 전역 상태이다. 고객이 이동하는 상태를 나타낸다.
/// </summary>
public class MoveState : iState
{
    private Customer customer;

    public MoveState(Customer customer)
    {
        this.customer = customer;
    }
    // 달리기 색상 추천
    private Color meshColor = Color.red;
    public Color MeshColor { get => meshColor; set => meshColor = value; }

    private bool isMovingToTarget = false;

    public void Enter()
    {

    }

    public void Execute()
    {
        isMovingToTarget = customer.IsMovingToTarget();
        if (isMovingToTarget) customer.MoveToTarget();
    }

    public void Exit()
    {

    }

    public void OnMessage(Telegram telegram)
    {
        customer.ChangeState(telegram.state);
    }
}
