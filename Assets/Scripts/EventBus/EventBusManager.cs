using Project.Tools.DictionaryHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class EventBusManager : MonoBehaviour
{
    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    private static EventBusManager _instance;

    /// <summary>
    /// 싱글톤 인스턴스를 가져오는 프로퍼티
    /// </summary>
    public static EventBusManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("EventBusManager");
                _instance = go.AddComponent<EventBusManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [Tooltip("이벤트 버스에 등록할 DynamicEventListener를 GameEventEnum 타입별로 리스트로 관리합니다.")]
    [SerializeField]
    private SerializableDictionary<GameEventEnum, List<DynamicEventListener>> _subscribersList
       = new SerializableDictionary<GameEventEnum, List<DynamicEventListener>>();

    private Dictionary<Type, Dictionary<Enum, List<Action<EventData>>>> eventHandlers = new();
    private List<IEventSubscriber> subscribers = new();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        RegisterAllSubscribers();
    }

    private void RegisterAllSubscribers()
    {
        var subscribers = GetTypeofGeneric<IEventSubscriber>.FindAllImplementing();
        
        foreach (var subscriber in subscribers)
        {
            var type = subscriber.GetType();
            var attribute = type.GetCustomAttribute<AutoEventSubscriber>();
            if (attribute != null)
            {
                RegisterSubscriber(subscriber, attribute.EventEnumType);
            }
        }
    }

    private void RegisterSubscriber(IEventSubscriber subscriber, Type eventEnumType)
    {
        subscribers.Add(subscriber);
        subscriber.Subscribe();
    }

    private void OnDestroy()
    {
        foreach (var subscriber in subscribers)
        {
            subscriber.Unsubscribe();
        }
        subscribers.Clear();
        eventHandlers.Clear();
        UnsubscribeAll();
    }

    /// <summary>
    /// 모든 DynamicEventListener들을 찾아서 Actions의 이벤트 타입으로 그룹화하는 메서드
    /// </summary>
    private static void FindAllAndSubscribe()
    {
        // 모든 DynamicEventListener들을 찾아서 Actions의 이벤트 타입으로 그룹화
        var allListeners = GetTypeofGeneric<DynamicEventListener>.FindAllImplementing();

        // allListeners 리스트를 순회하며 각 리스너들을 불러옴
        foreach (var listener in allListeners)
        {
            // 각 리스너의 ActionsOnListen의 키를 가져와서 리스너를 구독함
            foreach (var action in listener.ActionsOnListen)
            {
                Subscribe(action.Key, listener);
            }
        }
    }

    /// <summary>
    /// 딕셔너리의 모든 리스너를 구독 취소하는 메서드
    /// </summary>
    private static void UnsubscribeAll()
    {
        // _subscribersList 내부의 pairs를 순회하며 각 리스너들을 구독 취소함
        foreach (var kvp in Instance._subscribersList)
        {
            kvp.Value.Clear();
        }

        Instance._subscribersList.Clear();
    }

    /// <summary>
    /// 이벤트를 구독하는 메서드
    /// </summary>
    /// <param name="eventType">이벤트 타입</param>
    /// <param name="listener">리스너</param>
    public static void Subscribe(GameEventEnum eventType, DynamicEventListener listener)
    {
        // _subscribersList에 해당 이벤트 타입이 없으면 추가
        if (!Instance._subscribersList.ContainsKey(eventType))
        {
            Instance._subscribersList.Add(eventType, new List<DynamicEventListener>());
        }

        // 리스너가 구독되어 있지 않으면 추가
        if (!Instance._subscribersList[eventType].Contains(listener))
        {
            Instance._subscribersList[eventType].Add(listener);
        }
    }

    /// <summary>
    /// 이벤트 구독 취소 메서드
    /// </summary>
    /// <param name="eventType">이벤트 타입</param>
    /// <param name="listener">리스너</param>
    public static void Unsubscribe(GameEventEnum eventType, DynamicEventListener listener)
    {
        // _subscribersList에 해당 이벤트 타입이 없으면 리턴
        if (!Instance._subscribersList.ContainsKey(eventType)) return;

        // 해당 이벤트 타입의 리스너 리스트를 가져옴
        List<DynamicEventListener> subscribers = Instance._subscribersList[eventType];

        // 역순으로 반복하여 리스너를 제거
        for (int i = subscribers.Count - 1; i >= 0; i--)
        {
            if (subscribers[i] == listener)
            {
                subscribers.RemoveAt(i);

                if (subscribers.Count == 0)
                {
                    Instance._subscribersList.Remove(eventType);
                }
                return;
            }
        }
    }

    /// <summary>
    /// 이벤트를 발행하는 메서드
    /// </summary>
    /// <param name="data">이벤트의 타입과 서브 타입이 저장되어있는 클래스</param>
    public static void Publish(EventData data)
    {
        if (Instance.eventHandlers.TryGetValue(data.eventType.GetType(), out var handlers))
        {
            if (handlers.TryGetValue(data.eventType, out var eventList))
            {
                foreach (var handler in eventList.ToList())
                {
                    handler?.Invoke(data);
                }
            }
        }
        else
        {
            Debug.LogWarning($"No subscribers found for event type: {data.eventType}");
        }
    }

    public static void Subscribe<T>(T eventType, Action<EventData> handler) where T : Enum
    {
        var type = typeof(T);
        if (!Instance.eventHandlers.ContainsKey(type))
        {
            Instance.eventHandlers[type] = new Dictionary<Enum, List<Action<EventData>>>();
        }

        if (!Instance.eventHandlers[type].ContainsKey(eventType))
        {
            Instance.eventHandlers[type][eventType] = new List<Action<EventData>>();
        }

        Instance.eventHandlers[type][eventType].Add(handler);
    }

    public static void Unsubscribe<T>(T eventType, Action<EventData> handler) where T : Enum
    {
        var type = typeof(T);
        if (Instance.eventHandlers.TryGetValue(type, out var handlers))
        {
            if (handlers.TryGetValue(eventType, out var eventList))
            {
                eventList.Remove(handler);
            }
        }
    }
}
