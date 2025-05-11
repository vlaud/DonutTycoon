using Project.Tools.DictionaryHelp;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DynamicEventListener : MonoBehaviour
{
    [SerializeField]
    private SerializableDictionary<GameEventEnum, List<UnityEvent<EventData>>> actionsOnListen =
       new SerializableDictionary<GameEventEnum, List<UnityEvent<EventData>>>();

    public SerializableDictionary<GameEventEnum, List<UnityEvent<EventData>>> ActionsOnListen => actionsOnListen;

    public void OnEvent(EventData data)
    {
        if (actionsOnListen.TryGetValue(data.eventType, out List<UnityEvent<EventData>> events))
        {
            foreach (var action in events)
            {
                action.Invoke(data);
            }
        }
        else
        {
            Debug.LogWarning($"No actions found for event type: {data.eventType}");
        }
    }
}
