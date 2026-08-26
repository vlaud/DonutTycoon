using Project.Tools.DictionaryHelp;
using UnityEngine;
using UnityEngine.Events;

public class DynamicEventListener : MonoBehaviour
{
    [SerializeField]
    private SerializableDictionary<GameEventEnum, UnityEvent<EventData>> actionsOnListen =
       new SerializableDictionary<GameEventEnum, UnityEvent<EventData>>();

    public SerializableDictionary<GameEventEnum, UnityEvent<EventData>> ActionsOnListen => actionsOnListen;

    [SerializeField] UnityEvent<EventData> action;

    public void OnEvent(EventData data)
    {
        if (actionsOnListen.TryGetValue(data.eventType, out UnityEvent<EventData> events))
        {
            events.Invoke(data);
        }
        else
        {
            Debug.LogWarning($"No actions found for event type: {data.eventType}");
        }
    }
}
