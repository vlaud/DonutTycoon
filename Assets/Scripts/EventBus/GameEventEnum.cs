using System;

/// <summary>
/// 게임 이벤트의 열거형
/// </summary>
public enum GameEventEnum
{
    None, // 기본값
    CustomerEvent, // 고객 이벤트
    TimerEvent, // 타이머 이벤트
    ObjectPoolEvent, // 오브젝트 풀 이벤트
    ClickEvent, // 클릭 이벤트
}

/// <summary>
/// 이벤트 데이터 클래스
/// </summary>
public class EventData
{
    public GameEventEnum eventType; // 게임 이벤트의 열거형
    public Type subType; // 서브 이벤트 열거형 타입
    public Enum subValue; // 서브 이벤트 열거형 값

    /// <summary>
    /// 이벤트 데이터 생성자 : 서브 이벤트 열거형 타입이 없는 경우
    /// </summary>
    /// <param name="eventType"></param>
    public EventData(GameEventEnum eventType)
    {
        this.eventType = eventType;
        this.subType = null;
        this.subValue = null;
    }

    /// <summary>
    /// 이벤트 데이터 생성자 : 서브 이벤트 열거형 타입이 있는 경우
    /// </summary>
    /// <param name="eventType">게임 이벤트의 열거형</param>
    /// <param name="subValue">서브 이벤트 열거형 값</param>
    public EventData(GameEventEnum eventType, Enum subValue)
    {
        this.eventType = eventType;
        this.subType = subValue.GetType();
        this.subValue = subValue;
    }

    /// <summary>
    /// 서브 이벤트 열거형 값을 설정하는 메서드
    /// </summary>
    /// <param name="subValue"></param>
    public void SetSubType(Enum subValue)
    {
        this.subValue = subValue;
    }
}
