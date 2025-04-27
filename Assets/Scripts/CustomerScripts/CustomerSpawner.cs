using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerLoop
{
    None,
    SpwanCustomerLoop,
}

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] private CustomerPool customerPool;
    [SerializeField] private float remaining = 20f;
    [SerializeField] private Vector2 spawnRange = new Vector2(2f, 5f);
    CoroutineRunner coroutineRunner;
    private Dictionary<SpawnerLoop, Coroutine> coroutineDictionary = new Dictionary<SpawnerLoop, Coroutine>();
    [SerializeField] private Transform dismissPos;
    public Transform Dismisspos => dismissPos;

    private void Awake()
    {
        coroutineRunner = new CoroutineRunner(this);
    }

    private void Start()
    {
        // Initialize the coroutine dictionary
        coroutineDictionary[SpawnerLoop.SpwanCustomerLoop] = null;
        // Start the customer spawn loop
        coroutineRunner.StartCurrentCoroutine(coroutineDictionary[SpawnerLoop.SpwanCustomerLoop], out Coroutine currentCoroutine, SpwanCustomerLoop());
        coroutineDictionary[SpawnerLoop.SpwanCustomerLoop] = currentCoroutine;
    }

    IEnumerator SpwanCustomerLoop()
    {
        while (true)
        {
            // 손님이 꽉 차있으면 스폰 일시정지
            while (customerPool.IsCustomerFull())
            {
                yield return null;
            }

            // 스폰 대기시간을 랜덤으로 설정
            remaining = Random.Range(spawnRange.x, spawnRange.y);

            while (remaining > Mathf.Epsilon)
            {
                remaining -= Time.deltaTime;
                yield return null;
            }

            // 손님 스폰
            customerPool.SpawnCustomer(transform);
        }
    }

    public void RemoveCustomer(Customer customer)
    {
        customerPool.RemoveCustomer(customer);
    }
}
