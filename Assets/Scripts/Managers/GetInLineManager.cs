using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 손님의 대기열을 관리하는 클래스입니다.
/// </summary>
public class GetInLineManager : MonoBehaviour
{
    /// <summary>
    /// 손님이 대기하는 위치를 갖고있는 부모 트랜스폼입니다.
    /// </summary>
    [SerializeField] private Transform customerLineParent;
    /// <summary>
    /// 손님이 대기하는 위치를 나타내는 배열입니다.
    /// </summary>
    [SerializeField] private Transform[] customerSpots;
    /// <summary>
    /// 손님이 대기하는 위치를 가져오는 속성입니다.
    /// </summary>
    public Transform[] CustomerSpots => customerSpots;
    /// <summary>
    /// 손님의 큐를 나타내는 큐입니다.
    /// </summary>
    private Queue<Customer> customerQueue = new Queue<Customer>();
    public Queue<Customer> CustomerQueue { get => customerQueue; private set => customerQueue = value; }

    private void Awake()
    {
        // customerLineParent가 가진 모든 자식 트랜스폼을 가져옵니다. Where(t => t != customerLineParent)는 customerLineParent를 제외한 모든 자식 트랜스폼을 필터링합니다.
        customerSpots = customerLineParent.GetComponentsInChildren<Transform>().Where(t => t != customerLineParent).ToArray();
    }

    public int GetIndexOfLastQueue()
    {
        // 대기열의 마지막 인덱스를 반환합니다.
        return customerQueue.Count - 1;
    }

    public void AddCustomer(Customer customer)
    {
        // 손님을 큐에 추가합니다.
        customerQueue.Enqueue(customer);
    }

    /// <summary>
    /// 손님을 큐에서 제거합니다.
    /// </summary>
    /// <param name="customer"></param>
    public void RemoveCustomer(Customer customer)
    {
        // 손님을 큐에서 제거합니다.
        customerQueue.Dequeue();
        // 손님의 위치를 업데이트합니다.
        UpdateCustomerPositions();
    }

    /// <summary>
    /// 모든 손님의 위치를 비어있는 앞쪽으로 이동시킵니다.
    /// </summary>
    void UpdateCustomerPositions()
    {
        int i = 0;
        foreach (Customer customer in customerQueue)
        {
            // 손님의 위치를 업데이트합니다.
            customer.SetMoveTargetAndIndex(customerSpots[i], i);

            var telegram = new Telegram();
            telegram.SetNextState(CustomerState.Enter);
            customer.HandleMessage(telegram);
            i++;
        }
    }
}
