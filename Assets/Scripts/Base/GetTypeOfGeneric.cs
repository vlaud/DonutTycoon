using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GetTypeofGeneric<T> where T : class
{
    /// <summary>
    /// 트랜스폼에서 T를 상속받은 타입을 찾습니다.
    /// </summary>
    /// <param name="transform">트랜스폼</param>
    /// <returns></returns>
    public static Type GetType(Transform transform)
    {
        // T 타입을 상속받은 MonoBehaviour를 transform 내에서 찾습니다.
        var components = transform.GetComponents<T>();

        if (components == null || components.Length == 0)
            return null;

        // 가장 구체적인 타입을 찾기 위해 OrderByDescending로 깊이 정렬
        return components
            .Select(c => c.GetType())
            .OrderByDescending(type => GetInheritanceDepth(type))
            .FirstOrDefault();
    }

    /// <summary>
    /// 타입의 상속 깊이를 계산합니다. T를 상속받은 타입의 깊이를 계산합니다.
    /// </summary>
    /// <param name="type">상속받은 클래스 타입 </param>
    /// <returns></returns>
    private static int GetInheritanceDepth(Type type)
    {
        // 깊이는 0부터 시작합니다.
        int depth = 0;

        // type이 null이 아니고, T 타입이 아니고, object 타입이 아닐 때까지 반복합니다.
        while (type != null && type != typeof(T) && type != typeof(object))
        {
            depth++;
            type = type.BaseType;
        }
        return depth;
    }

    /// <summary>
    /// 모든 MonoBehaviour 오브젝트를 검색하여 특정 인터페이스를 구현하는 오브젝트를 찾습니다.
    /// </summary>
    /// <typeparam name="T">인터페이스</typeparam>
    /// <returns></returns>
    public static List<T> FindAllImplementing()
    {
        List<T> results = new List<T>();

        // 모든 MonoBehaviour 오브젝트 검색
        MonoBehaviour[] allBehaviours = GameObject.FindObjectsByType<MonoBehaviour>();

        foreach (var behaviour in allBehaviours)
        {
            if (behaviour is T t)
            {
                results.Add(t);
            }
        }

        return results;
    }
}
