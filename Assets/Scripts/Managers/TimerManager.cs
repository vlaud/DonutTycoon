using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TimerPool timerPool;
    [SerializeField] private float defaultWaitTime = 2f;
    [SerializeField] private float defaultDuration = 5f;
    private WaitForSeconds waitForSeconds;

    /// <summary>
    /// 타이머 대상과 코루틴을 연결하는 딕셔너리
    /// </summary>
    private Dictionary<iTimerObserver, Coroutine> timerDictionary = new Dictionary<iTimerObserver, Coroutine>();

    private void Awake()
    {
        waitForSeconds = new WaitForSeconds(defaultWaitTime);
    }
    /// <summary>
    /// 타이머 UI를 각 개체에 추가합니다
    /// </summary>
    /// <param name="target">연결될 개체</param>
    public void AddTimer(iTimerObserver target)
    {
        Debug.Log($"TimerUI spawn from: {target.GetTransform().name}");

        this.StartManagedCoroutine(ref timerDictionary, target, WaitAndSpawnTimer(target));
    }

    public void RemoveTimer(iTimerObserver target)
    {
        Debug.Log("time end");
        // 타이머가 존재하지 않으면 경고를 출력합니다.
        if (!timerDictionary.ContainsKey(target))
        {
            Debug.LogWarning($"No timer found for target: {target.GetTransform().name}");
            return;
        }
        // 타이머 UI를 풀에 반환합니다.
        timerPool.ReleaseTimer(target);

        // 코루틴을 중지합니다.
        this.StopManagedCoroutine(ref timerDictionary, target);
    }

    private IEnumerator WaitAndSpawnTimer(iTimerObserver target)
    {
        yield return waitForSeconds;
        timerPool.SpawnTimer(target, defaultDuration);
    }
}
