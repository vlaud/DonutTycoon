using UnityEngine;

public interface iTimerObserver
{
    void OnTimerFinished();
    Transform GetTransform();

    void SetTimerUI(TimerUI timerUI);
}
