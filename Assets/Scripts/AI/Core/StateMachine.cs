using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
  public IState CurrentState {  get; private set; }

    public void ChangeState(IState newState)
    {
        if (CurrentState == newState) return;

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Tick()
    {
        CurrentState?.Tick();
    }
}
