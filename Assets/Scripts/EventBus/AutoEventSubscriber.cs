using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class AutoEventSubscriber : Attribute
{
    public Type EventEnumType { get; private set; }

    public AutoEventSubscriber(Type eventEnumType)
    {
        if (!eventEnumType.IsEnum)
        {
            throw new ArgumentException("Type must be an enum", nameof(eventEnumType));
        }
        EventEnumType = eventEnumType;
    }
} 