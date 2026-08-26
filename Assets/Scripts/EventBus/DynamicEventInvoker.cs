using Project.Tools.DictionaryHelp;
using ProjectTools;
using UnityEngine;
using UnityEngine.Events;

public class DynamicEventInvoker : MonoBehaviour
{
    [Header("이벤트 타입 - 서브타입 매핑")]
    [Tooltip("이벤트를 발행할 때 호출할 액션을 등록합니다. 이벤트 타입에 따라 다르게 설정할 수 있습니다.")]
    [SerializeField]
    private SerializableDictionary<GameEventEnum, UnityEvent<EventData>> actionsOnInvoke =
        new SerializableDictionary<GameEventEnum, UnityEvent<EventData>>();

    [Header("서브타입 - 이벤트 액션 매핑")]
    [SerializeField]
    private SerializableDictionary<UnityObjectWrapper<EventTypeSelector>, UnityEvent<EventData>> actions = new();
    /// <summary>
    /// 이벤트를 발행하는 메서드입니다.
    /// </summary>
    /// <param name="data">이벤트 데이터 클래스</param>
    public void Publish(EventData data)
    {
        // actionsOnInvoke 내부에서 data의 eventType을 사용하여 맞는 액션을 찾습니다.
        if (actionsOnInvoke.TryGetValue(data.eventType, out UnityEvent<EventData> events))
        {
            // 등록된 액션을 호출합니다.
            events.Invoke(data);

            // 그 후 이벤트버스에서 발행합니다.
            EventBusManager.Publish(data);
        }
        else
        {
            Debug.LogWarning($"No actions found for event type: {data.eventType}");
        }
    }
}
