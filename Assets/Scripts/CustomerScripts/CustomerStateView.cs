using UnityEngine;

public class CustomerStateView : MonoBehaviour
{
    // a user interface that responds to internal state changes
    private Customer customer;
    private StateMachine customerStateMachine;

    // mesh to change color
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        customer = GetComponent<Customer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // cache to save typing
        customerStateMachine = customer.CustomerStateMachine;
        // listen for any state changes
        customerStateMachine.stateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        // unregister the subscription if we destroy the object
        customerStateMachine.stateChanged -= OnStateChanged;
    }

    private void OnStateChanged(iState state)
    {
        Debug.Log("State changed to: " + state);
        ChangeMeshColor(state);
    }

    // set mesh material to the current state's associated color
    private void ChangeMeshColor(iState state)
    {
        Debug.Log("Changing mesh color to: " + state.MeshColor);
        if (spriteRenderer == null) return;

        spriteRenderer.color = state.MeshColor;
    }
}
