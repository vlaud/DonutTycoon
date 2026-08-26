using UnityEngine;

[AutoEventSubscriber(typeof(TimerEventEnum))]
public class TimerEventHandler : MonoBehaviour, IEventSubscriber<TimerEventEnum>
{
    public void Subscribe()
    {
        // 모든 타이머 이벤트 타입에 대해 구독
        foreach (TimerEventEnum eventType in System.Enum.GetValues(typeof(TimerEventEnum)))
        {
            EventBusManager.Subscribe(eventType, (data) => OnEventReceived(eventType, data));
        }
    }

    public void Unsubscribe()
    {
        // 모든 타이머 이벤트 타입에 대해 구독 해제
        foreach (TimerEventEnum eventType in System.Enum.GetValues(typeof(TimerEventEnum)))
        {
            EventBusManager.Unsubscribe(eventType, (data) => OnEventReceived(eventType, data));
        }
    }

    public void OnEventReceived(TimerEventEnum eventType, EventData data)
    {
        switch (eventType)
        {
            case TimerEventEnum.TimeStart:
                Debug.Log("Timer Started");
                break;
            case TimerEventEnum.TimeStop:
                Debug.Log("Timer Stopped");
                break;
            case TimerEventEnum.TimePause:
                Debug.Log("Timer Paused");
                break;
            case TimerEventEnum.TimeResume:
                Debug.Log("Timer Resumed");
                break;
        }
    }
} 