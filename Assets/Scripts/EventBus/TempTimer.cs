using UnityEngine;

[RequireComponent(typeof(DynamicEventInvoker))]
public class TempTimer : MonoBehaviour
{
    [SerializeField] private DynamicEventInvoker dynamicEventInvoker;
    private EventData TimerEventData { get; set; } = new EventData(GameEventEnum.TimerEvent, TimerEventEnum.TimeStop);

    // 인스펙터에서 TempTimer를 설정할 때, DynamicEventInvoker가 자동으로 연결되도록 합니다.
    private void OnValidate()
    {
        if (dynamicEventInvoker == null)
        {
            dynamicEventInvoker = GetComponent<DynamicEventInvoker>();
        }
    }

    private void Awake()
    {
        if (dynamicEventInvoker == null)
        {
            dynamicEventInvoker = GetComponent<DynamicEventInvoker>();
        }
    }

    public void TimeStop()
    {
        Debug.Log("Key Pressed");
        Debug.Log(TimerEventData.subValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dynamicEventInvoker.Publish(TimerEventData);
            TimerEventData.SetSubType(TimerEventEnum.TimeStart);
        }
    }
}

public enum TimerEventEnum
{
    None,
    TimeStop,
    TimeStart,
    TimePause,
    TimeResume
}