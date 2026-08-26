using UnityEngine;

public class TempObject : MonoBehaviour
{
    public void OnTimer(EventData data)
    {
        if (data.subType == typeof(TimerEventEnum))
        {
            TimerEventEnum timerEvent = (TimerEventEnum)data.subValue;
            switch (timerEvent)
            {
                case TimerEventEnum.TimeStop:
                    Debug.Log("Time stopped in TempObject");
                    break;
                case TimerEventEnum.TimePause:
                    Debug.Log("Time paused in TempObject");
                    break;
                case TimerEventEnum.TimeResume:
                    Debug.Log("Time resumed in TempObject");
                    break;
            }
        }
    }

    public void OnCustomer(EventData data)
    {

    }

    public void OnClick(EventData data)
    {

    }
}

public enum ObjectPoolEventEnum
{
    None,
    Get,
    Release,
}