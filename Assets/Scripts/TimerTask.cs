using System;
using UnityEngine;

public class TimerTask
{
    public string id;
    /// <summary>
    /// 총 시간
    /// </summary>
    public float duration;
    /// <summary>
    /// 남은 시간
    /// </summary>
    public float remaining;
    /// <summary>
    /// 타이머가 종료되었을 때 호출되는 델리게이트
    /// </summary>
    public iTimerObserver target;
    /// <summary>
    /// 타이머가 남은 시간에 따라 호출되는 델리게이트. TimerUI의 UpdateProgress가 등록된다. TimerTask의 Tick(float deltaTime)에서 호출됩니다.
    /// </summary>
    public Action<float> onTick;

    public TimerTask(string id, float duration, iTimerObserver target)
    {
        this.id = id;
        this.duration = duration;
        this.remaining = duration;
        this.target = target;
    }

    /// <summary>
    /// TimeManager에서 매 업데이트 시 호출되는 함수입니다.
    /// </summary>
    /// <param name="deltaTime">Time.deltaTime </param>
    /// <returns></returns>
    public bool Tick(float deltaTime)
    {
        remaining -= deltaTime;
        if (remaining <= Mathf.Epsilon)
        {
            //target.OnTimerFinished(id);
            return true; // Timer finished
        }

        // 타이머가 종료되지 않았을 때 남은 시간을 비율로 변환하여 델리게이트 호출
        onTick?.Invoke(remaining);
        return false; // Timer still running
    }
}
