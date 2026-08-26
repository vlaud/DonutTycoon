using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TimerPool : MonoBehaviour
{
    // 타이머 프리팹
    [SerializeField] private TimerUI timerPrefab;
    private ObjectPool<TimerUI> timerPool;
    [SerializeField] private int timerCount = 0;
    private Dictionary<iTimerObserver, TimerUI> timerDictionary = new Dictionary<iTimerObserver, TimerUI>();

    private void Awake()
    {
        // Initialize the timer pool
        timerPool = new ObjectPool<TimerUI>(CreateTimer, OnGetTimer, OnReleaseTimer, OnDestroyTimer, false, 10, 200);
    }

    private TimerUI CreateTimer()
    {
        // Create a new timer
        TimerUI timer = Instantiate(timerPrefab, Gamemanager.GetUICanvas.transform);
        timer.gameObject.SetActive(false);
        return timer;
    }

    private void OnGetTimer(TimerUI uI)
    {
        // Activate the timer UI
        uI.gameObject.SetActive(true);
        // 부모를 캔버스로 설정
        uI.transform.SetParent(Gamemanager.GetUICanvas.transform);
        // 타이머 초기 설정
        uI.SetUpTimerUI();
    }

    private void OnReleaseTimer(TimerUI uI)
    {
        // Deactivate the timer UI
        uI.gameObject.SetActive(false);

        uI.SetIsReleased(true);
        // Release the timer back to the pool
        uI.transform.SetParent(transform);
        uI.transform.position = Vector3.zero;
    }

    private void OnDestroyTimer(TimerUI uI)
    {
        // Destroy the timer UI
        Destroy(uI.gameObject);
    }

    /// <summary>
    /// 타이머를 풀에서 가져옵니다.
    /// </summary>
    /// <param name="target">연결될 개체</param>
    /// <param name="duration">활성화 기간</param>
    public void SpawnTimer(iTimerObserver target, float duration)
    {
        // 타이머가 이미 존재하는지 확인합니다.
        if (timerDictionary.ContainsKey(target))
        {
            Debug.LogWarning($"Timer already exists for target: {target.GetTransform().name}");
            return;
        }

        // 타이머를 풀에서 가져옵니다.
        TimerUI timer = timerPool.Get();

        // 이름 설정
        string timerName = $"Timer_{timerCount} with {target.GetTransform().name}";
        timer.gameObject.name = timer.IsReleased ? "Reused: " + timerName : timerName;

        // 타이머 개수 증가
        timerCount++;

        // 타이머에 target과 timer을 연결합니다.
        timerDictionary.Add(target, timer);

        // 타이머와 대상 연결
        timer.AttachTo(target, duration);
        target.SetTimerUI(timer);

        // Activate the timer
        timer.OnActive();
    }

    public void ReleaseTimer(TimerUI uI)
    {
        uI.OnRelease();
        // 타이머를 풀에 반환합니다.
        timerPool.Release(uI);
    }

    public void ReleaseTimer(iTimerObserver target)
    {
        // 타이머를 풀에 반환합니다.
        if (timerDictionary.TryGetValue(target, out TimerUI timer))
        {
            timerDictionary.Remove(target);
            ReleaseTimer(timer);
        }
    }
}
