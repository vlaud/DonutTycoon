using System;

public interface IEventSubscriber
{
    void Subscribe();
    void Unsubscribe();
}

public interface IEventSubscriber<T> : IEventSubscriber where T : Enum
{
    void OnEventReceived(T eventType, EventData data);
} 