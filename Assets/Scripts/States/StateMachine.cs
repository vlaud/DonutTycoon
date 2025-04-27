using System;

public class StateMachine
{
    public iState currentState { get; private set; }
    public iState globalState { get; private set; }

    public event Action<iState> stateChanged;


    public void SetUp(iState newState)
    {
        currentState = null;
        globalState = null;
        ChangeState(newState);
    }

    public void SetGlobalState(iState state)
    {
        globalState = state;
    }

    public void ChangeState(iState newState)
    {
        if (newState == null) return;
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter();
        stateChanged?.Invoke(currentState);
    }

    public void Update()
    {
        if (globalState != null)
        {
            globalState.Execute();
        }

        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public void HandleMessage(Telegram telegram)
    {
        if (globalState != null)
        {
            globalState?.OnMessage(telegram);
        }
        if (currentState != null)
        {
            currentState?.OnMessage(telegram);
        }
    }
}
