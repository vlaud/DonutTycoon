using System.Collections.Generic;
using UnityEngine;

public enum CustomerState
{
    None,
    Enter,
    Wait,
    Clicked,
    Leave,
    Move,
}

public class Customer : MonoBehaviour
{
    // The state machine for the customer
    private StateMachine stateMachine;
    public StateMachine CustomerStateMachine => stateMachine;

    [Header("손님 상태")]
    private Dictionary<CustomerState, iState> stateDictionary = new Dictionary<CustomerState, iState>();
    private Dictionary<CustomerState, CustomerState> stateTransitionDictionary = new Dictionary<CustomerState, CustomerState>();
    [Tooltip("손님 상태")]
    [SerializeField] private CustomerState currentState;
    public CustomerState CurrentState => currentState;
    [SerializeField] private CustomerState globalState;
    public CustomerState GlobalState => globalState;

    [Tooltip("손님 속도")]
    public float speed = 5f;

    [Tooltip("손님 이동 목표")]
    [SerializeField] private Transform moveTarget;
    public Transform MoveTarget => moveTarget;
    [SerializeField] private int targetIndex = 0;
    public int TargetIndex => targetIndex;

    [Header("이벤트 관리")]
    [Tooltip("손님 이벤트")]
    [SerializeField] private EventBinding eventBinding;

    [SerializeField] private string customerInfo = "손님 정보";
    public string CustomerInfo => customerInfo;

    private bool isClicked;
    /// <summary>
    /// 손님 클릭 여부
    /// </summary>
    public bool IsClicked => isClicked;

    private void OnEnable()
    {
        // eventBinding안의 모든 EventsOnListen들을 등록
        eventBinding.EventsOnListen[GameEventEnum.TimerEvent]?.Register(OnTimerEvent);
    }
    private void OnDisable()
    {
        // eventBinding안의 모든 EventsOnListen들을 등록
        eventBinding.EventsOnListen[GameEventEnum.TimerEvent]?.Unregister(OnTimerEvent);
    }

    private void Awake()
    {
        stateDictionary.Add(CustomerState.None, new NoneState(this));
        stateDictionary.Add(CustomerState.Enter, new EnterState(this));
        stateDictionary.Add(CustomerState.Wait, new WaitState(this));
        stateDictionary.Add(CustomerState.Clicked, new ClickedState(this));
        stateDictionary.Add(CustomerState.Leave, new LeaveState(this));
        stateDictionary.Add(CustomerState.Move, new MoveState(this));

        stateTransitionDictionary.Add(CustomerState.Enter, CustomerState.Wait);
        stateTransitionDictionary.Add(CustomerState.Leave, CustomerState.None);

        // Initialize the state machine
        stateMachine = new StateMachine();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the initial state to Enter
        stateMachine.SetUp(stateDictionary[CustomerState.Enter]);
        stateMachine.SetGlobalState(stateDictionary[CustomerState.Move]);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the state machine
        stateMachine.Update();
    }

    public void ChangeState(CustomerState newState)
    {
        // Change the state of the customer
        currentState = newState;
        stateMachine.ChangeState(stateDictionary[currentState]);
    }

    // if customer is clicked, set isClicked to true
    public void OnCustomerClicked()
    {
        if (currentState == CustomerState.Wait && targetIndex == 0)
        {
            isClicked = true;
        }
    }

    // if other position is clicked, set isClicked to false
    public void OnClickMissed()
    {
        isClicked = false;
        Debug.Log("OnClickMissed");
    }

    public void TriggerCustomerEvent()
    {
        // Trigger the customer event
        eventBinding.EventOnInvoke[GameEventEnum.CustomerEvent]?.Raise();
    }

    private void OnTimerEvent()
    {
        // Handle the timer event here
        Debug.Log("Timer event triggered!");
    }

    public void SetCustomerInfo(string info)
    {
        customerInfo = info;
    }

    public void SetMoveTargetAndIndex(Transform transform, int index)
    {
        SetIndex(index);
        SetMoveTarget(transform);
    }

    public void SetIndex(int index)
    {
        // Set the index for the customer
        targetIndex = index;
    }

    public void SetMoveTarget(Transform transform)
    {
        // Set the move target for the customer
        moveTarget = transform;
    }

    public void MoveToTarget()
    {
        // Move the customer to the target position
        if (moveTarget != null)
        {
            Vector3 direction = (moveTarget.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public bool IsMovingToTarget()
    {
        if (currentState == CustomerState.Wait 
            || currentState == CustomerState.None
            || currentState == CustomerState.Clicked) 
            return false;

        bool isMoving = Vector3.Distance(transform.position, moveTarget.position) >= 0.1f;
        if (!isMoving)
        {
            Debug.Log("sending message to state machine");
            var telegram = new Telegram();
            telegram.SetNextState(stateTransitionDictionary[currentState]);
            Debug.Log($"Customer change State to: {stateTransitionDictionary[currentState]}");
            HandleMessage(telegram);
        }
        return isMoving;
    }

    public void HandleMessage(Telegram telegram)
    {
        stateMachine.HandleMessage(telegram);
    }
}
