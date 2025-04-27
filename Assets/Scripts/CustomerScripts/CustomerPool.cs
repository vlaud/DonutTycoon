using UnityEngine;
using UnityEngine.Pool;

public class CustomerPool : MonoBehaviour
{
    // The prefab for the customer
    [SerializeField] private Customer customerPrefab;
    // The pool of customers
    public ObjectPool<Customer> customerPool;

    /// <summary>
    /// 손님 대기열 관리자
    /// </summary>
    [SerializeField] private GetInLineManager getInLineManager;
    public GetInLineManager GetInLineManager => getInLineManager;

    private void Awake()
    {
        // Initialize the customer pool
        customerPool = new ObjectPool<Customer>(CreateCustomer, OnGetCustomer, OnReleaseCustomer, OnDestroyCustomer, false, 10, 200);
    }

    private Customer CreateCustomer()
    {
        // Create a new customer
        Customer customer = Instantiate(customerPrefab);
        customer.gameObject.SetActive(false);
        return customer;
    }

    /// <summary>
    /// 함수 설명: 고객을 가져올 때 호출됩니다.
    /// </summary>
    /// <param name="customer"></param>
    private void OnGetCustomer(Customer customer)
    {
        // 손님 게임 오브젝트를 활성화합니다.
        customer.gameObject.SetActive(true);
        // 손님을 대기열에 추가합니다.
        getInLineManager.AddCustomer(customer);
        // 손님의 대기열 위치를 설정합니다. GetIndexOfLastQueue는 대기열의 마지막 인덱스를 나타냅니다.
        int index = getInLineManager.GetIndexOfLastQueue();
        customer.gameObject.name = "Customer_" + index;
        customer.SetMoveTargetAndIndex(getInLineManager.CustomerSpots[index], index);
    }

    /// <summary>
    /// 함수 설명: 고객을 반환할 때 호출됩니다.
    /// </summary>
    /// <param name="customer"></param>
    private void OnReleaseCustomer(Customer customer)
    {
        // Reset the customer
        customer.gameObject.SetActive(false);

        // Reset the customer's position
        customer.transform.SetParent(transform);
        customer.transform.position = Vector3.zero;
    }

    /// <summary>
    /// 함수 설명: 고객을 파괴할 때 호출됩니다.
    /// </summary>
    /// <param name="customer"></param>
    private void OnDestroyCustomer(Customer customer)
    {
        // Destroy the customer
        Destroy(customer.gameObject);
    }

    /// <summary>
    /// 함수 설명: 고객을 스폰합니다.
    /// </summary>
    public void SpawnCustomer(Transform target)
    {
        // 풀에서 고객을 가져옵니다.
        Customer customer = customerPool.Get();
        // 손님의 스폰 위치를 설정합니다.
        customer.transform.position = target.position;
        customer.SetCustomerInfo($"{getInLineManager.GetIndexOfLastQueue()}번 손님입니다.");
        customer.ChangeState(CustomerState.Enter);
    }

    /// <summary>
    /// 함수 설명: 고객을 반환합니다.
    /// </summary>
    /// <param name="customer"></param>
    public void RemoveCustomer(Customer customer)
    {
        // Return the customer to the pool
        customerPool.Release(customer);
    }

    public bool IsCustomerFull()
    {
        // Check if the customer pool is full
        return getInLineManager.CustomerQueue.Count >= getInLineManager.CustomerSpots.Length;
    }
}
