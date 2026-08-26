using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventTypeSelector", menuName = "Event System/EventTypeSelector")]
public class EventTypeSelector : ScriptableObject
{
    [SerializeField] private string enumFullTypeName;

    public Type EnumType => Type.GetType(enumFullTypeName);

    public void SetType<T>() where T : Enum
    {
        enumFullTypeName = typeof(T).AssemblyQualifiedName;
    }

#if UNITY_EDITOR
    [ContextMenu("Set as TimerEventEnum")]
    private void SetToTimer() => SetType<TimerEventEnum>();
    [ContextMenu("Set as CustomerEventEnum")]
    private void SetToCustomer() => SetType<CustomerEventEnum>();
    [ContextMenu("Set as ObjectPoolEventEnum")]
    private void SetToPool() => SetType<ObjectPoolEventEnum>();

# endif
}
