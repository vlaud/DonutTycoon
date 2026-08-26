using UnityEngine;

public class TempCustomer : MonoBehaviour
{
    public void OnTimer(EventData data)
    {
        if (data.subType == typeof(TimerEventEnum))
        {
            TimerEventEnum timerEvent = (TimerEventEnum)data.subValue;
            switch (timerEvent)
            {
                case TimerEventEnum.TimeStop:
                    Debug.Log("Time stopped in TempCustomer");
                    break;
                case TimerEventEnum.TimeStart:
                    Debug.Log("Time started in TempCustomer");
                    break;
                case TimerEventEnum.TimePause:
                    Debug.Log("Time paused in TempCustomer");
                    break;
                case TimerEventEnum.TimeResume:
                    Debug.Log("Time resumed in TempCustomer");
                    break;
            }
        }
    }

    public void OnObject(EventData data)
    {

    }

    public void OnClick(EventData data)
    {

    }
}

public enum CustomerEventEnum
{
    None,
    Spawn,
    Release,
}