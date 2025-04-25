using UnityEngine;
using UnityEngine.Events;

public class ClickController : MonoBehaviour
{
    [System.Serializable]
    public class OnSingleClick : UnityEvent { };
    public OnSingleClick onSingleClick;
    [System.Serializable]
    public class OnDoubleClick : UnityEvent { };
    public OnDoubleClick onDoubleClick;
    [System.Serializable]
    public class OnClickMissed : UnityEvent { };
    public OnClickMissed onClickMissed;
}
